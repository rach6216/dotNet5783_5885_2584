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
        List<OrderItem?> list =f!=null? s_orderItems.Where(f).ToList(): s_orderItems;
        return list;
    }

    /// <summary>
    /// read order item by condition
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    /// <exception cref="ExceptionEntityNotFound"></exception>
    public OrderItem Read(Func<OrderItem?, bool>? f)
    {
        if(f == null)
            throw new ExceptionEntityNotFound("Order item is not found");
        try
        {
            return s_orderItems.First(f)??throw new Exception();
        }
        catch
        {
            throw new ExceptionEntityNotFound("Order item is not found");
        }
    }
    #endregion

    #region Update 
    /// <summary>
    /// update details of order-item
    /// </summary>
    /// <param name="oi">order-item id to update</param>
    public void Update(OrderItem oi)
    {
        if (1 > s_orderItems.RemoveAll(x => oi.ID == x?.ID))
            throw new ExceptionEntityNotFound("order-item not found");
        s_orderItems.Add(oi);
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete order-item 
    /// </summary>
    /// <param name="id">order-item id to delete</param>
    public void Delete(int id)
    {
      if(1> s_orderItems.RemoveAll(x=>x?.ID==id))
            throw new ExceptionEntityNotFound("the order-item to delete is not found");
    }

   
    #endregion

}
