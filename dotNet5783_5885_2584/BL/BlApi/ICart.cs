

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
    public BO.Cart AddProduct(BO.Cart cart, int id);
    /// <summary>
    /// update product amoumt in cart
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="id">product id</param>
    /// <param name="amount">new amount to update</param>
    /// <returns>the update cart after the change</returns>
    public BO.Cart UpdatePAmount(BO.Cart cart, int id, int amount);
    /// <summary>
    /// confirm order and create it
    /// </summary>
    /// <param name="cart">cart object</param>
    /// <param name="customerName">name of the customer</param>
    /// <param name="customerEmail">email of the customer</param>
    /// <param name="customerAdress">adress of the customer</param>
    public BO.Order ConfirmOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress);
    /// <param name="id">product id</param>
    /// <returns>amount of certain product in the cart</returns>
}
