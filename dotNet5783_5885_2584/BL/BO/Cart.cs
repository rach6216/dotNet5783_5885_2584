

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
    public List<OrderItem> Items { get; set; }
    /// <summary>
    /// the total price of the order
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string orderItem = "";
        foreach (var item in Items)
        {
            orderItem += item;
        }
        return $@"
        Customer name: {CustomerName}, 
        Customer Email: {CustomerEmail},
        Customer Address: {CustomerAddress},
        Items: 
        {orderItem},

        Total price of cart: {TotalPrice}

";
    }
}
