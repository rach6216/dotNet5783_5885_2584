using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class OrderItem:IOrderItem
{
    #region Fields& properties  of order-item
    /// <summary>
    /// id of the order item
    /// </summary>
    public int ID;
    /// <summary>
    /// product id
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// order id
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// price of order
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of products in order
    /// </summary>
    public int Amount { get; set; }

    public int Create(DO.OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Read(Func<DO.OrderItem?, bool>? f)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? f = null)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Constructor for order-item
    /// <summary>
    /// ctor for orderItems
    /// </summary>
    /// <param name="myPID">product id</param>
    /// <param name="myOID">order id</param>
    /// <param name="myPrice">price of order</param>
    /// <param name="myAmount"> amount of products in order</param>
    /// <param name="myID" default="000000">id of the order item</param>

    #endregion

    #region To string
    /// <summary>
    /// description of the order+item object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString()
    {
        return $@"
        Id: {ID}
        product id: {ProductID}
        order id: {OrderID}
        price: {Price}
        amount: {Amount}
";
    }

    public void Update(DO.OrderItem entity)
    {
        throw new NotImplementedException();
    }
    #endregion

}
