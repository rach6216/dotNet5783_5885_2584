

using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class Order
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of the customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// email of the customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// address of the customer
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// status of the order
    /// </summary>
    public OrderStatus? Status { get; set; } = BO.OrderStatus.OrderIsConfirmed;

    /// <summary>
    /// date of the order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// shipping date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// list of items in the order
    /// </summary>
    public List<BO.OrderItem?>? Items { get; set; }
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
        Order ID: {ID}: 
        Customer name: {CustomerName}, 
        Customer Email: {CustomerEmail},
        Customer Address: {CustomerAddress},
        status: {Status},
        OrderDate: {OrderDate},
        ShipDate: {ShipDate},
        DeliveryDate: {DeliveryDate},
        Items: {orderItem},
        TotalPrice: {TotalPrice}

";
    }

}
