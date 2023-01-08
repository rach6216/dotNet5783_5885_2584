
using DalApi;
using DO;
using System.ComponentModel;
using System.Security.Principal;

namespace Dal;

sealed internal class DalList : IDal
{
    //Lazy<T> - a built C# class that provide Thread Safe Initialization & fully Lazy, this class is Theared Safe in default,
    //the value property is initalize only in the first access to it
    private static readonly Lazy<IDal> _instance = new Lazy<IDal>(() => new DalList());
    public static IDal Instance { get { return _instance.Value; } }

    public IProduct Product { get; } = new DalProduct();
    public IOrder Order { get; }= new DalOrder();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
    //return the value property
    private DalList()
    {
        //Product = p;
        //Order = new DalOrder();
        //OrderItem = new DalOrderItem();
        //Instance { get; } = new DalList();
    }
}

