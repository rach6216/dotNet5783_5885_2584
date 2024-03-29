﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;




public class OrderItem : DalApi.IOrderItem
{
    string OrderItemFile = @"OrderItem.xml";
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(DO.OrderItem entity)
    {
        List<DO.OrderItem> list = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItem.xml");
        var identify = XMLTools.LoadListFromXMLElement("config.xml");
        int id = int.Parse(identify.Elements().ToList()[2].Value);
        identify.Elements().ToList()[2].Value = (id+1).ToString();
        entity.ID = id;
        list.Add(entity);
        XMLTools.SaveListToXMLElement(identify, "config.xml");
        XMLTools.SaveListToXMLSerializer(list, "OrderItem.xml");
        return id;

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.OrderItem?> orderList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemFile);
        orderList.RemoveAll(x => x == null || x?.ID == id);
        XMLTools.SaveListToXMLSerializer(orderList, OrderItemFile);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Read(Func<DO.OrderItem?, bool>? f)
    {
        try
        {
            List<DO.OrderItem?> orderList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemFile);
            DO.OrderItem? orderItem = orderList.FirstOrDefault(x => x != null && f(x));
            if (orderItem != null)
                return (DO.OrderItem)orderItem;
            throw new DO.ExceptionEntityNotFound("orderItem not found");
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw;
        }

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? f = null)
    {
        List<DO.OrderItem?> orderList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemFile);
        return orderList.Where(x => f == null || f(x));
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
        List<DO.OrderItem?> orderList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemFile);
        orderList.RemoveAll(x => x == null || x?.ID == entity.ID);
        orderList.Add(entity);
        XMLTools.SaveListToXMLSerializer(orderList, OrderItemFile);
    }
    #endregion

}
