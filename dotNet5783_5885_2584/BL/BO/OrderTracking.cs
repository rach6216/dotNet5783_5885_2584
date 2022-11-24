

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// status of the order
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// list of pairs that show the order track
    /// </summary>
    public List<(DateTime d, string s)> Tracking;
}
