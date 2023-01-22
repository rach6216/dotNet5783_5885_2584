using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Order:IOrder
{
    #region Fields and auto properties
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

    public int Create(DO.Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Read(Func<DO.Order?, bool>? f)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? f = null)
    {
        throw new NotImplementedException();
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

    public void Update(DO.Order entity)
    {
        throw new NotImplementedException();
    }
    #endregion

}
