
using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>List of all orders</returns>
    public IEnumerable<OrderForList> ReadAll();
    /// <summary>
    /// get order details
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>constructed order object</returns>
    public Order Read(int id);
    /// <summary>
    /// update the order ship date
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>the update order with the ship date</returns>
    public Order ShipOrder(int id);
    /// <summary>
    /// update the order delivery date
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>the update order with the delivery date</returns>
    public Order DeliveryOrder(int id);
    /// <summary>
    /// method that give details about the track of the order
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>orderTracking that reflect the track of the order</returns>
    public OrderTracking OrderTracking(int id); 
    
    /// <summary>
    /// update order by id
    /// </summary>
    /// <param name="id">order id </param>
    /// <returns>the updated logic order</returns>
    public BO.Order  UpdateOrder(int orderID,BO.OrderItem orderItem);
}
