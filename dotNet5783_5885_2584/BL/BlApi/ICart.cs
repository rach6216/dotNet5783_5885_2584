

using BO;

namespace BlApi;
/// <summary>
/// intrface of cart
/// </summary>
public interface ICart
{
    /// <summary>
    /// add product to the cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">produt id</param>
    /// <returns>the update cart with the new product</returns>
    public Cart AddProduct(Cart cart, int id);
    /// <summary>
    /// update product amoumt in cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">product id</param>
    /// <param name="amount">new amount to update</param>
    /// <returns>the update cart after the change</returns>
    public Cart UpdatePAmount(Cart cart, int id, int amount);
    /// <summary>
    /// confirm order and create it
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="customerName">name of the customer</param>
    /// <param name="customerEmail">email of the customer</param>
    /// <param name="customerAdress">adress of the customer</param>
    public void ConfirmOrder(Cart cart, string customerName, string customerEmail, string customerAdress); public void ConfirmOrder(Cart cart,string customerName,string email,string adress);
}
