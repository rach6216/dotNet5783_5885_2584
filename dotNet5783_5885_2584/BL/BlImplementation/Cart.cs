using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : ICart
{
    private IDal Dal = new DalList();
    /// <summary>
    /// add product to the cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">produt id</param>
    /// <returns>the update cart with the new product</returns>
    public BO.Cart AddProduct(BO.Cart cart, int id)
    {
        bool exist = false;
        ProductItem product=new ProductItem();
        foreach (var item in cart.Items)
            if (item.ID == id) { product = item; exist = true; break; }
        if (exist)
            if (product.InStock > 0) {
                product.Amount += 1;
                product.InStock -= 1;
                //update price of item
                cart.TotalPrice+=product.Price;
            }
        else
        {   ProductForList products=new ProductForList();
            foreach (var p in products)
            {
                 Console.WriteLine(p);
            }
            }

        return new BO.Cart();

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
        return new BO.Cart();
    }
    /// <summary>
    /// confirm order and create it
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="customerName">name of the customer</param>
    /// <param name="customerEmail">email of the customer</param>
    /// <param name="customerAdress">adress of the customer</param>
    public void ConfirmOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress) { }
}
