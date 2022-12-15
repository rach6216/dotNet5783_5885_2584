
using DalApi;
using DO;
using System.ComponentModel;
using System.Security.Principal;

namespace Dal;

sealed internal class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get; } = new DalList();
    private DalList()
    {
        //Product = p;
        //Order = new DalOrder();
        //OrderItem = new DalOrderItem();
        //Instance { get; } = new DalList();
    }
}

