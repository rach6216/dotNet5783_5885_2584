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
    /// get all the order-items
    /// </summary>
    /// <returns>array with all the order-items</returns>
    public IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? f = null)
    {
        List<OrderItem?> list = s_orderItems;
        if (f != null)
        {
            list = s_orderItems.FindAll(x => f(x));
        }
        return list;
    }

    /// <summary>
    /// read order item by condition
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    /// <exception cref="ExceptionEntityNotFound"></exception>
    public OrderItem Read(Func<OrderItem?, bool> f)
    {
        OrderItem? oi = s_orderItems.Find(x => f(x));
        if (oi.HasValue&& oi?.ID != 0 )
            return new OrderItem() { ID = oi.Value.ID, Amount = oi.Value.Amount, OrderID = oi.Value.OrderID, Price = oi.Value.Price, ProductID = oi.Value.ProductID };
        throw new ExceptionEntityNotFound("Order item is not found");
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
            if (s_orderItems[i]?.ID == oi.ID)
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
      bool flag= s_orderItems.Remove(Read(x=>x.Value.ID==id));
        if (!flag)
            throw new ExceptionEntityNotFound("the order-item to delete is not found");
    }

   
    #endregion

}
