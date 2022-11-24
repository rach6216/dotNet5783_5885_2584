
using System.CodeDom.Compiler;

namespace DO;

/// <summary>
/// structure for orders
/// </summary>
public struct Order
{
    #region Fields and auto properties
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

    #endregion

    #region Order constructors
    /// <summary>
    /// Order constructor 1
    /// </summary>
    /// <param name="cName">customer name</param>
    /// <param name="cEmail">cuatomer email</param>
    /// <param name="cAddress">customer address</param>
    /// <param name="orderD">order Date</param>
    /// <param name="id">optional order id</param>
    public Order(string cName, string cEmail, string cAddress, DateTime orderD, int id = 0)
    {
        ID = id;
        CustomerName = cName;
        CustomerEmail = cEmail;
        CustomerAddress = cAddress;
        OrderDate = orderD;
        ShipDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
        Random trnd = new Random();
        OrderDate = orderD;
    }
    /// <summary>
    /// Order constructor 2
    /// </summary>
    /// <param name="cName">customer name</param>
    /// <param name="cEmail">cuatomer email</param>
    /// <param name="cAddress">customer address</param>
    /// <param name="orderD">order Date</param>
    /// <param name="shipD">ship date</param>
    /// <param name="id">optional order id</param>
    public Order(string cName, string cEmail, string cAddress, DateTime orderD, DateTime shipD, int id = 0)
    {
        ID = id;
        CustomerName = cName;
        CustomerEmail = cEmail;
        CustomerAddress = cAddress;
        OrderDate = orderD;
        ShipDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
        Random trnd = new Random();
        OrderDate = orderD;
        ShipDate = shipD;
    }
    /// <summary>
    /// order constructor 3
    /// </summary>
    /// <param name="cName">customer name</param>
    /// <param name="cEmail">cuatomer email</param>
    /// <param name="cAddress">customer address</param>
    /// <param name="orderD">order Date</param>
    /// <param name="shipD">ship date</param>
    /// <param name="deliveryD">delivery date</param>
    /// <param name="id">optional order id</param>
    public Order(string cName, string cEmail, string cAddress, DateTime orderD, DateTime shipD, DateTime deliveryD, int id = 0)
    {
        ID = id;
        CustomerName = cName;
        CustomerEmail = cEmail;
        CustomerAddress = cAddress;
        OrderDate = orderD;
        ShipDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
        OrderDate = orderD;
        ShipDate = shipD;
        DeliveryDate = deliveryD;
    }
    #endregion

    #region To string
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
    #endregion


}
