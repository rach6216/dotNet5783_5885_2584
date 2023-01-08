using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for actions on orders entities
/// </summary>
internal struct DalOrder : IOrder
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
    public Order Read(Func<Order?, bool>? f)
    {
        try
        {
            return s_orders.First(f ?? throw new Exception()) ?? throw new Exception();
        }
        catch
        {
            throw new ExceptionEntityNotFound("Order is not found");
        }
    }

    /// <summary>
    /// get all the orders
    /// </summary>
    /// <returns>array with all the orders</returns>
    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? f = null)
    {
        List<Order?> ol = s_orders;
        if (f != null)
        {
            ol = s_orders.Where(f).ToList();
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
        if (1 > s_orders.RemoveAll(x => o.ID == x?.ID))
            throw new ExceptionEntityNotFound("the order entity not found");
        s_orders.Add(o);
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete order from the orders by id
    /// </summary>
    /// <param name="id">id of order to delete</param>
    public void Delete(int id)
    {
        if (1 > s_orders.RemoveAll(x => x?.ID == id))
            throw new ExceptionEntityNotFound("Order for delete is not found");
    }


    #endregion
}
