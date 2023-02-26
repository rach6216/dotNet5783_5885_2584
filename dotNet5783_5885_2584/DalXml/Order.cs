using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;



public class Order : DalApi.IOrder
{
    string OrderFile = @"Order.xml";
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
        List<DO.Order> list = XMLTools.LoadListFromXMLSerializer<DO.Order>(OrderFile);
        var identify = XMLTools.LoadListFromXMLElement("config.xml");
        int id = int.Parse(identify.Elements().ToList()[1].Value);
        identify.Elements().ToList()[1].Value = (id + 1).ToString();
        entity.ID = id;
        list.Add(entity);
        XMLTools.SaveListToXMLElement(identify, "config.xml");
        XMLTools.SaveListToXMLSerializer(list, OrderFile);
        return id;
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFile);
            orderList.RemoveAll(x => x == null || x?.ID == id);
            XMLTools.SaveListToXMLSerializer(orderList, OrderFile);
        }
        catch (DO.XMLFileLoadCreateException e)
        {
            throw new(e.Message);
        }
    }

    public DO.Order Read(Func<DO.Order?, bool>? f)
    {
        if (f != null)
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFile);
            DO.Order? order = orderList.FirstOrDefault(x => f(x));
            if (order == null)
                throw new DO.ExceptionEntityNotFound("order not found");
            return (DO.Order)order;
        }
        else

            throw new DO.ExceptionEntityNotFound("order not found");
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? f = null)
    {
        List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFile);
        return orderList.Where(x => f == null || f(x));
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
        List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFile);
        orderList.RemoveAll(x => x == null || x?.ID == entity.ID);
        orderList.Add(entity);
        XMLTools.SaveListToXMLSerializer(orderList, OrderFile);
    }
    #endregion

}
