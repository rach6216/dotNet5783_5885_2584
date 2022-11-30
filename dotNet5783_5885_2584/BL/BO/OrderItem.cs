
using System.Xml.Linq;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// id of the order item
    /// </summary>
    public int ID;
    /// <summary>
    /// product id
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// name of product
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// price of order
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of products in order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the total price of the order
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        ID: {ID},
        Product ID: {ProductID}, 
        Product name: {ProductName}, 
    	Price: {Price},
    	Amount: {Amount},
        Total price: {TotalPrice}

";


}
