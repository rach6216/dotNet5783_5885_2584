using DO;

namespace DalApi;

public interface IOrderItem:ICrud<OrderItem>
{
    public OrderItem Read(int oID, int pID);
    public IEnumerable<OrderItem> ReadByOrder(int oID);
}
