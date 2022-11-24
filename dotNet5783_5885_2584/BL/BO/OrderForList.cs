
using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of the customer
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// status of the order
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// amount of items in the order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the total price of the order
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        order ID: {ID}: 
        Customer name: {CustomerName}, 
        order Status: {Status}
    	Amount : {Amount}
        TotalPrice: {TotalPrice}

";

}
