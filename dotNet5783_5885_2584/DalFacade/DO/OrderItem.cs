﻿namespace DO;

/// <summary>
/// structure for linking the product with the order
/// </summary>
public struct OrderItem
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
    public OrderItem(int myPID, int myOID, double myPrice, int myAmount, int myID = 000000)
    {
        ID = myID;
        ProductID = myPID;
        OrderID = myOID;
        Price = myPrice;
        Amount = myAmount;
    }
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
    #endregion

}
