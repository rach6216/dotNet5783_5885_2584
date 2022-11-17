using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for actions on orders entities
/// </summary>
internal struct DalOrder:IOrder
{
    #region Create
    /// <summary>
    /// create new order and enter it to the orders array
    /// </summary>
    /// <param name="o">order without id</param>
    /// <returns>the id of the new order</returns>
    public int Create(Order o)
    {
        o.ID = Config.OrderID;
        s_orders.Add(o);
        return o.ID;
    }
    #endregion

    #region Read
    /// <summary>
    /// get the order by the id
    /// </summary>
    /// <param name="id">the id of the requested order</param>
    /// <returns>the requested order</returns>
    /// <exception cref="Exception">if the order doesn't exist throw string: "Order is not found" </exception>
    public Order Read(int id)
    {
        Order o=s_orders.Find(x => x.ID == id);
        if (o.ID != 0)
            return o;
        throw new ExceptionEntityNotFound("Order is not found");
    }

    /// <summary>
    /// get all the orders
    /// </summary>
    /// <returns>array with all the orders</returns>
    public IEnumerable<Order> Read()
    {
        List<Order> ol=s_orders;       
        return ol;
    }
    #endregion

    #region Update
    /// <summary>
    /// update a specific order
    /// </summary>
    /// <param name="o">order to update</param>
    public void Update(Order o)
    {
        for (int i = 0; i < s_orders.Count; i++)
        {
            if (s_orders[i].ID == o.ID)
            {
                s_orders[i] = o;
                return;
            } 
        }
        throw new ExceptionEntityNotFound("the order entity not found");
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete order from the orders by id
    /// </summary>
    /// <param name="id">id of order to delete</param>
    public void Delete(int id)
    {
       bool flag= s_orders.Remove(Read(id));
        if (!flag)
        {
            throw new ExceptionEntityNotFound("Order for delete is not found");
        }
    }
    #endregion
}
