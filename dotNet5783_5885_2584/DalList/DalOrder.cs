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
/// read order by condition
/// </summary>
/// <param name="f">lambda function bool</param>
/// <returns>the first order that true in the condition</returns>
/// <exception cref="ExceptionEntityNotFound"></exception>
    public Order Read(Func<Order?, bool> f)
    {
        Order? o = s_orders.Find(x => f(x));
        if (o.HasValue&&o?.ID != 0 )
            return new Order() { CustomerAddress = o.Value.CustomerAddress, CustomerEmail = o.Value.CustomerEmail, CustomerName = o.Value.CustomerName, DeliveryDate = o.Value.DeliveryDate, ID = o.Value.ID, OrderDate = o.Value.OrderDate, ShipDate = o.Value.ShipDate };
        throw new ExceptionEntityNotFound("Order is not found");
    }

    /// <summary>
    /// get all the orders
    /// </summary>
    /// <returns>array with all the orders</returns>
    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? f = null)
    {

        List<Order?> ol=s_orders;
        if (f != null)
        {
            ol = s_orders.FindAll(x=>f(x));
        }
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
            if (s_orders[i]?.ID == o.ID)
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
       bool flag= s_orders.Remove(Read(x=>x.Value.ID==id));
        if (!flag)
        {
            throw new ExceptionEntityNotFound("Order for delete is not found");
        }
    }

   


    #endregion
}
