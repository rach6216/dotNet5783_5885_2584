using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for octions on order-items 
/// </summary>
internal struct DalOrderItem:IOrderItem
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
        s_orderItems.Add(oi);
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
       OrderItem oi= s_orderItems.Find(x=>x.ID==id);
       if(oi.ID!=0)
            return oi;
        throw new Exception("Order item is not found");
    }

    /// <summary>
    /// get all the order-items
    /// </summary>
    /// <returns>array with all the order-items</returns>
    public IEnumerable<OrderItem> Read()
    {
        List<OrderItem> list = s_orderItems;
        return list;
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
        OrderItem oi = s_orderItems.Find(x => x.OrderID == oID&&x.ProductID==pID);
        if (oi.ID != 0)
            return oi;
        throw new Exception("Order item is not found");
    }
    /// <summary>
    /// get all the order-items of specific order
    /// </summary>
    /// <param name="oID">order id</param>
    /// <returns>array with order-items</returns>
    public IEnumerable<OrderItem> ReadByOrder(int oID)
    {
        List<OrderItem> list = new();
        for (int i = 0; i < s_orderItems.Count; i++)
        {
            if (s_orderItems[i].OrderID == oID)
                list.Add(s_orderItems[i]);
        }
        return list;
    }
    #endregion

    #region Update 
    /// <summary>
    /// update details of order-item
    /// </summary>
    /// <param name="oi">order-item id to update</param>
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < s_orderItems.Count; i++)
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
      bool flag= s_orderItems.Remove(Read(id));
        if (!flag)
            throw new ExceptionEntityNotFound("the order-item to delete is not found");
    }
    #endregion

}
