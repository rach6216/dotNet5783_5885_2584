using DO;
using static Dal.DataSource;

namespace Dal;

public struct DalOrder
{
    public int Create(Order o)
    {
        o.ID = Config.OrderID;
        _orders[Config._orderIndex++] = o;
        return o.ID;
    }
    public Order Read(int id)
    {
        for (int i = 0; i < Config._orderIndex; i++)
        {
            if (_orders[i].ID == id)
                return _orders[i];
        }
        throw new Exception("Order is not found");
    }
    public Order[] Read()
    {
        Order[] orders = new Order[Config._orderIndex];
        for (int i = 0; i < Config._orderIndex; i++)
        {
            orders[i] = _orders[i];
        }
        return _orders;
    }
    public void Update(Order o)
    {
        for (int i = 0; i < Config._orderIndex; i++)
        {
            if (_orders[i].ID == o.ID)
                _orders[i] = o;
        }
    }
    public void Delete(int id)
    {
        int i = 0;
        while (_orders[i].ID != id) { i++; }
        while (i < Config._orderIndex - 1)
        {
            _orders[i] = _orders[i + 1];
        }
        Config._orderIndex--;
    }
}
