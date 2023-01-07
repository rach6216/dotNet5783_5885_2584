using BlApi;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();
    /// <summary>
    /// add product to the cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">produt id</param>
    /// <returns>the update cart with the new product</returns>
    public BO.Cart AddProduct(BO.Cart cart, int id)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        cart ??= new BO.Cart();
        cart.Items ??= new List<BO.OrderItem?>() { };
        try
        {
            int productAmount = (from orderItem in cart.Items
                                 where orderItem.ID == id
                                 select orderItem).FirstOrDefault()?.Amount ?? default;
            cart = UpdatePAmount(cart, id, productAmount + 1);
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionInvalidInput("can't get product,it doesn't exist", exp);
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
        DO.Product product = new() { };
        try
        {
            product = _dal?.Product.Read(x => x?.ID == id) ?? throw new BO.ExceptionNullDal();
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionInvalidInput("can't get product,it doesn't exist", exp);
        }
        if (product.InStock < amount)
            throw new BO.ExceptionProductOutOfStock("can't update product amount, there is only " + product.InStock + " items in stock");
        BO.OrderItem item = cart.Items.FirstOrDefault(x => x?.ID == id) ?? new BO.OrderItem() { Price = product.Price, ProductID = id, ProductName = product.Name };
        product.InStock += item.Amount;
        cart.TotalPrice -= item.TotalPrice;
        cart.Items.RemoveAll(x => x?.ID == id);
        item.Amount = amount;
        product.InStock -= amount;
        item.TotalPrice = item.Amount * item.Price;
        if (amount > 0)
            cart.Items.Add(item);
        cart.TotalPrice += item.TotalPrice;
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
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        cart ??= new BO.Cart();
        if (cart.Items == null || cart.Items.Count == 0)
            throw new BO.ExceptionCannotCreateItem("cart is empty, can't confirm order");
        //integrity check
        bool isEmail = Regex.IsMatch(customerEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (customerEmail == null || !IsValid(customerEmail))
            throw new BO.ExceptionInvalidInput("invalid customer email ");
        //create order
        DO.Order order = new(customerName ?? throw new BO.ExceptionInvalidInput("invalid customer name "), customerEmail, customerAdress ?? throw new BO.ExceptionInvalidInput("invalid customer address "), DateTime.Now);
        int orderID = _dal.Order.Create(order);
        BO.Order order2 = new()
        {
            ID = orderID,
            CustomerAddress = customerAdress,
            CustomerEmail = customerEmail,
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
                    DO.Product p = _dal.Product.Read(x => x?.ID == o.ProductID);
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
