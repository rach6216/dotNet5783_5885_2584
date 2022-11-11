
namespace DO;

/// <summary>
/// structure for orders
/// </summary>
public struct Order
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
    /// email of the customer
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// address of the customer
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// date of the order
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// shipping date
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    /// <summary>
    /// ctor for orders
    /// </summary>
    /// <param name="cName">name of the customer</param>
    /// <param name="cEmail">email of the customer</param>
    /// <param name="cAddress">address of the customer</param>
    /// <param name="orderD">date of the order</param>
    /// <param name="shipD">shipping date</param>
    /// <param name="deliveryD">delivery date</param>
    /// <param name="myOID" default="000000">order id</param>
    public Order(string cName, string cEmail, string cAddress, DateTime orderD, int myOID = 000000)
    {
        ID = myOID;
        CustomerName = cName;
        CustomerEmail = cEmail;
        CustomerAddress = cAddress;
        OrderDate = orderD;
        Random trnd = new Random();
        if (orderD == DateTime.MinValue)
        {
            OrderDate = new DateTime(trnd.Next(1, DateTime.Now.Year), trnd.Next(1, 12), trnd.Next(1, 30));

        }
        ShipDate = OrderDate + new TimeSpan(trnd.Next(10), trnd.Next(24), trnd.Next(60), trnd.Next(60), trnd.Next(1000));
        DeliveryDate = ShipDate + new TimeSpan(trnd.Next(15), trnd.Next(24), trnd.Next(60), trnd.Next(60), trnd.Next(1000));
    }

    /// <summary>
    /// description of the order object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString() => $@"
    order ID: {ID}
    customer name: {CustomerName}
    customer email: {CustomerEmail}
    customer address: {CustomerAddress}
    order date: {OrderDate}
    ship date: {ShipDate}
    delivery date: {DeliveryDate}
";

}
