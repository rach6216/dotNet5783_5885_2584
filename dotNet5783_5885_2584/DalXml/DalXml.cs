using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalXml : IDal
    {
        private static readonly Lazy<IDal> _instance = new Lazy<IDal>(() => new DalXml());
        public static IDal Instance { get { return _instance.Value; } }

        public IProduct Product { get; } = new Dal.Product();

        public IOrderItem OrderItem { get; } = new Dal.OrderItem();

        public IOrder Order { get; } = new Dal.Order();

        public IUser User { get; } = new Dal.User();
    }
}
