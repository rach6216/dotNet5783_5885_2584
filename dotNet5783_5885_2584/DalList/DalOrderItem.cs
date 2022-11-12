
using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for octions on order-items 
/// </summary>
public struct DalOrderItem
{
    #region Create 
    /// <summary>
    /// generate id for the order-item and put it in the order-items array
    /// </summary>
    /// <param name="oi">order-item without id</param>
    /// <returns>the order-item id</returns>
    public int Create(OrderItem oi)
    {
        oi.ID = Config.OrderItemID;
        s_orderItems[Config.s_orderItemIndex++] = oi;
        return oi.ID;
    }
    #endregion

    #region Read
    /// <summary>
    /// get order-item by id
    /// </summary>
    /// <param name="id">id of the order-item request</param>
    /// <returns>order-item with the requested id</returns>
    /// <exception cref="Exception">if there are no order-item with the id throw string: "Order item is not found"</exception>
    public OrderItem Read(int id)
    {
        for (int i = 0; i < Config.s_orderItemIndex; i++)
        {
            if (s_orderItems[i].ID == id)
                return s_orderItems[i];
        }
        throw new Exception("Order item is not found");
    }

    /// <summary>
    /// get all the order-items
    /// </summary>
    /// <returns>array with all the order-items</returns>
    public OrderItem[] Read()
    {
        Console.WriteLine("reading don't disturb");
        OrderItem[] items = new OrderItem[Config.s_orderItemIndex];
        for (int i = 0; i < Config.s_orderItemIndex; i++)
        {
            items[i] = s_orderItems[i];
        }
        return items;
    }


    /// <summary>
    /// get order item by two id's:order & product
    /// </summary>
    /// <param name="oID">order id</param>
    /// <param name="pID">product id</param>
    /// <returns>order-item with these two id's</returns>
    /// <exception cref="Exception">when there are no such order-item throw string: Order item is not found </exception>
    public OrderItem Read(int oID, int pID)
    {
        for (int i = 0; i < Config.s_orderItemIndex; i++)
        {
            if (s_orderItems[i].OrderID == oID && s_orderItems[i].ProductID == pID)
                return s_orderItems[i];
        }
        throw new Exception("Order item is not found");
    }
    /// <summary>
    /// get all the order-items of specific order
    /// </summary>
    /// <param name="oID">order id</param>
    /// <returns>array with order-items</returns>
    public OrderItem[] ReadByOrder(int oID)
    {
        int j = 0;
        OrderItem[] items = new OrderItem[Config.s_orderItemIndex];
        for (int i = 0; i < Config.s_orderItemIndex; i++)
        {
            if (s_orderItems[i].OrderID == oID)
                items[j++] = s_orderItems[i];
        }
        return items;
    }
    #endregion

    #region Update 
    /// <summary>
    /// update details of order-item
    /// </summary>
    /// <param name="oi">order-item id to update</param>
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < Config.s_orderItemIndex; i++)
        {
            if (s_orderItems[i].ID == oi.ID)
                s_orderItems[i] = oi;
        }
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete order-item 
    /// </summary>
    /// <param name="id">order-item id to delete</param>
    public void Delete(int id)
    {
        int i = 0;
        while (s_orderItems[i].ID != id) { i++; }
        s_orderItems[i] = s_orderItems[--Config.s_orderItemIndex];

    }
    #endregion

}
