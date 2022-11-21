

using DO;

namespace BO;

public class Cart
{
    /// <summary>
    /// name of the customer
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// email of the customer
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// address of the customer
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// items in the cart
    /// </summary>
    List<OrderItem> Items { get; set; }
    /// <summary>
    /// the total price of the items in cart
    /// </summary>
    public double TotalPrice { get; set; }
}
