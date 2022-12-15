using BlApi;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal _dal = new Dal.DalList();
    /// <summary>
    /// add product to the cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">produt id</param>
    /// <returns>the update cart with the new product</returns>
    public BO.Cart AddProduct(BO.Cart cart, int id)
    {

        cart ??= new BO.Cart();
        cart.Items ??= new List<BO.OrderItem?>() { };
        DO.Product product;
        try
        {
            product = _dal.Product.Read(x => x?.ID == id);
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionInvalidInput("can't get product,it doesn't exist", exp);
        }
        int oiIndex = cart.Items.FindIndex(x => x?.ProductID == id);
        if (oiIndex != -1)
        {
            if (product.InStock > 0 && cart.Items != null && cart.Items[oiIndex] != null)
            {
                cart.Items[oiIndex]!.Amount += 1;
                product.InStock -= 1;
                cart.Items[oiIndex]!.TotalPrice = cart.Items[oiIndex]!.Amount * cart.Items[oiIndex]!.Price;
                //update price of item
                cart.TotalPrice += product.Price;
            }
        }
        else
        {
            if (product.InStock > 0)
            {
                cart.Items.Add(new BO.OrderItem() { Amount = 1, Price = product.Price, ProductID = id, ProductName = product.Name, TotalPrice = product.Price });
                cart.TotalPrice += product.Price;
            }
        }
        return cart;

    }
    /// <summary>
    /// update product amoumt in cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">product id</param>
    /// <param name="amount">new amount to update</param>
    /// <returns>the update cart after the change</returns>
    public BO.Cart UpdatePAmount(BO.Cart cart, int id, int amount)
    {
        cart ??= new BO.Cart();
        cart.Items ??= new List<BO.OrderItem?>() { };
        int oiIndex = cart.Items.FindIndex(x => x?.ProductID == id);
        if (cart.Items[oiIndex] != null && cart.Items[oiIndex]!.Amount != amount)
        {
            int oldAmount = cart.Items[oiIndex]!.Amount;
            if (0 == amount)
            {
                cart.TotalPrice -= cart.Items[oiIndex]!.TotalPrice;
                cart.Items.RemoveAt(oiIndex);
            }
            else if (oldAmount > amount)
            {
                cart.Items[oiIndex]!.Amount = amount;
                cart.Items[oiIndex]!.TotalPrice -= (oldAmount - amount) * cart.Items[oiIndex]!.Price;
                cart.TotalPrice -= (oldAmount - amount) * cart.Items[oiIndex]!.Price;
            }
            else
            {
                for (int i = 0; i < amount - oldAmount; i++)
                    AddProduct(cart, id);

            }
        }

        return cart;
    }
    /// <summary>
    /// confirm order and create it
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="customerName">name of the customer</param>
    /// <param name="customerEmail">email of the customer</param>
    /// <param name="customerAdress">adress of the customer</param>
    public BO.Order ConfirmOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        cart ??= new BO.Cart();
        if (cart.Items == null || cart.Items.Count == 0)
            throw new BO.ExceptionCannotCreateItem("cart is empty, can't confirm order");
        //integrity check
        if (customerAdress == null)
            throw new BO.ExceptionInvalidInput("invalid customer address ");
        if (customerName == null)
            throw new BO.ExceptionInvalidInput("invalid customer name ");
        bool isEmail = Regex.IsMatch(customerEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (customerEmail == null || !IsValid(customerEmail))
            throw new BO.ExceptionInvalidInput("invalid customer email ");
        //create order
        DO.Order order = new(customerName, customerEmail, customerAdress, DateTime.Now);
        int orderID = _dal.Order.Create(order);
        BO.Order order2 = new()
        {
            ID = orderID,
            CustomerAddress = customerAdress,
            CustomerEmail = customerEmail
            ,
            CustomerName = customerName,
            Status = 0,
            TotalPrice = 0,
            Items = new List<BO.OrderItem?>() { },
            OrderDate = DateTime.Now
        };
        string messageOfMissingProducts = "";
        //create order-items
        foreach (var o in cart.Items)
        {
            if (o != null)
                try
                {
                    DO.Product p = _dal.Product.Read(x => (x?.ID == o.ProductID));
                    if (p.InStock == 0)
                    {
                        messageOfMissingProducts += " ," + p.Name + " is out of stock";
                    }
                    else if (p.InStock < o.Amount)
                    {
                        int orderItemID = _dal.OrderItem.Create(new DO.OrderItem(p.ID, orderID, p.Price, o.Amount));
                        order2.Items.Add(new BO.OrderItem()
                        {
                            ProductID = p.ID,
                            Amount = o.Amount,
                            ID = orderItemID,
                            Price = p.Price,
                            ProductName = p.Name,
                            TotalPrice = p.Price * o.Amount
                        });
                        p.InStock = 0;
                        _dal.Product.Update(p);
                        messageOfMissingProducts += " ," + p.Name + " out of stock " + (o.Amount - p.InStock);
                    }
                    else
                    {
                        int orderItemID = _dal.OrderItem.Create(new DO.OrderItem(p.ID, orderID, p.Price, o.Amount));
                        order2.Items.Add(new BO.OrderItem()
                        {
                            ProductID = p.ID,
                            Amount = o.Amount,
                            ID = orderItemID,
                            Price = p.Price,
                            ProductName = p.Name,
                            TotalPrice = p.Price * o.Amount
                        });
                        p.InStock -= o.Amount;
                        _dal.Product.Update(p);
                    }

                }
                catch (DO.ExceptionEntityNotFound exp)
                {
                    throw new BO.ExceptionInvalidInput("product of order item is not exist", exp);
                }



        }
        //if there was some items out of stock

        if (messageOfMissingProducts != "")
        {
            throw new BO.ExceptionProductOutOfStock("order #" + orderID + "complited, some items is out of stock " + messageOfMissingProducts);
        }
        return order2;
    }
    /// <summary>
    /// validation of email
    /// </summary>
    /// <param name="email">email address</param>
    /// <returns>valid or not</returns>
    private static bool IsValid(string email)
    {
        bool isValid = true;
        try { MailAddress m = new(email); }
        catch { isValid = false; }
        return isValid;
    }
}
