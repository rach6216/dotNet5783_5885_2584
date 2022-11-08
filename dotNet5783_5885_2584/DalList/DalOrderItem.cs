
using DO;
using static Dal.DataSource;

namespace Dal;

public struct DalOrderItem
{
    public int Create(OrderItem oi)
    {
        oi.ID = Config.OrderItemID;
        _orderItems[Config._orderItemIndex++] = oi;
        return oi.ID;
    }
    public OrderItem Read(int id)
    {
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if (_orderItems[i].ID == id)
                return _orderItems[i];
        }
        throw new Exception("Order item is not found");
    }
    public OrderItem[] Read()
    {
       
        OrderItem[] items = new OrderItem[Config._orderItemIndex];
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            items[i] = _orderItems[i];
        }
        return items;
    }
    public OrderItem Read(int oID,int pID)
    {
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if (_orderItems[i].OrderID == oID && _orderItems[i].ProductID==pID)
                return _orderItems[i];
        }
        throw new Exception("Order item is not found");
    }
    public OrderItem[] ReadByOrder(int oID)
    {
        int j = 0;
        OrderItem[] items = new OrderItem[Config._orderItemIndex];
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if(_orderItems[i].OrderID == oID)
                items[j++]=_orderItems[i];
        }
        return items;
    }
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if (_orderItems[i].ID == oi.ID)
                _orderItems[i] = oi;
        }
    }
    public void Delete(int id)
    {
        int i = 0;
        while (_orderItems[i].ID != id) { i++; }
        while (i < Config._orderItemIndex - 1)
        {
            _orderItems[i] = _orderItems[i + 1];
        }
        Config._orderItemIndex--;
    }

}
