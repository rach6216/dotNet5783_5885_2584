
namespace Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
sealed public class DalXml : DalApi.IDal
{
    private static readonly Lazy<DalApi.IDal> _instance = new(() => new Dal.DalXml());
    public static DalApi.IDal Instance { get { return _instance.Value; } }

    public DalApi.IProduct Product { get; } = new Dal.Product();

    public DalApi.IOrderItem OrderItem { get; } = new Dal.OrderItem();

    public DalApi.IOrder Order { get; } = new Dal.Order();

    public DalApi.IUser User { get; } = new Dal.User();
    private DalXml()
    {
    }
}
