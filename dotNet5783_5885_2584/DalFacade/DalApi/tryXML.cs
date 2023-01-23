//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace DalApi;
//sealed public class TryXml : IDal
//{
//    private static readonly Lazy<IDal> _instance = new(() => new TryXml());
//    public static IDal Instance { get { return _instance.Value; } }

//    public IProduct Product { get; } = new Dal.Product();

//    public IOrderItem OrderItem { get; } = new Dal.OrderItem();

//    public IOrder Order { get; } = new Dal.Order();

//    public IUser User { get; } = new Dal.User();
//    private TryXml()
//    {
//        //Product = p;
//        //Order = new DalOrder();
//        //OrderItem = new DalOrderItem();
//        //Instance { get; } = new DalList();
//    }
//}
