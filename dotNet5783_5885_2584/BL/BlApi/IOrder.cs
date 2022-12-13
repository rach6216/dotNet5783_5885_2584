
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>List of all orders</returns>
    public IEnumerable<BO.OrderForList?> ReadAll(Func<DO.Order?,bool>? f=null);
    /// <summary>
    /// get order details
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>constructed order object</returns>
    public BO.Order Read(Func<DO.Order?,bool>? f);
    /// <summary>
    /// update the order ship date
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>the update order with the ship date</returns>
    public BO.Order ShipOrder(int id);
    /// <summary>
    /// update the order delivery date
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>the update order with the delivery date</returns>
    public BO.Order DeliveryOrder(int id);
    /// <summary>
    /// method that give details about the track of the order
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>orderTracking that reflect the track of the order</returns>
    public BO.OrderTracking OrderTracking(int id); 
    
    /// <summary>
    /// update order by id
    /// </summary>
    /// <param name="id">order id </param>
    /// <returns>the updated logic order</returns>
    public BO.Order UpdateOrder(int orderID,BO.OrderItem orderItem);
}
