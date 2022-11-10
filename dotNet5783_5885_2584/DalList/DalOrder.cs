using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for actions on orders entities
/// </summary>
public struct DalOrder
{
    /// <summary>
    /// create new order and enter it to the orders array
    /// </summary>
    /// <param name="o">order without id</param>
    /// <returns>the id of the new order</returns>
    public int Create(Order o)
    {
        o.ID = Config.OrderID;
        s_orders[Config.s_orderIndex++] = o;
        return o.ID;
    }
    /// <summary>
    /// get the order by the id
    /// </summary>
    /// <param name="id">the id of the requested order</param>
    /// <returns>the requested order</returns>
    /// <exception cref="Exception">if the order doesn't exist throw string: "Order is not found" </exception>
    public Order Read(int id)
    {
        for (int i = 0; i < Config.s_orderIndex; i++)
        {
            if (s_orders[i].ID == id)
                return s_orders[i];
        }
        throw new Exception("Order is not found");
    }
    /// <summary>
    /// get all the orders
    /// </summary>
    /// <returns>array with all the orders</returns>
    public Order[] Read()
    {
        Order[] orders = new Order[Config.s_orderIndex];
        for (int i = 0; i < Config.s_orderIndex; i++)
        {
            orders[i] = s_orders[i];
        }
        return s_orders;
    }
    /// <summary>
    /// update a specific order
    /// </summary>
    /// <param name="o">order to update</param>
    public void Update(Order o)
    {
        for (int i = 0; i < Config.s_orderIndex; i++)
        {
            if (s_orders[i].ID == o.ID)
                s_orders[i] = o;
        }
    }
    /// <summary>
    /// delete order from the orders by id
    /// </summary>
    /// <param name="id">id of order to delete</param>
    public void Delete(int id)
    {
        int i = 0;
        while (s_orders[i].ID != id) { i++; }
        s_orders[i] = s_orders[--Config.s_orderIndex];

    }
}
