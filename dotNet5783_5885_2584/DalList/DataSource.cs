

namespace Dal;
using DO;
internal static class DataSource
{
    static readonly Random rnd = new Random();
    static internal Order[] _orders = new Order[100];
    static internal Product[] _products = new Product[50];
    static internal OrderItem[] _orderItems = new OrderItem[200];
    static DataSource()
    {
        s_Initialize();
    }
    static private void addOrder(Order newOrder)
    {
        _orders[Config._orderIndex++]=newOrder;
    }
    static private void addProduct(Product newProduct)
    {
        newProduct.ID = Config.ProductID;
        _products[Config._productIndex++]=newProduct;
    }
    static private void addOrderItem(OrderItem newOrderItem)
    {
        _orderItems[Config._orderItemIndex++]=newOrderItem;
    }
    static private void s_Initialize()
    {
        Product[] tProduct = new Product[10];
        Order[] tOrder = new Order[20];
        OrderItem[] tOrderItem = new OrderItem[40];
        for(int i = 0; i < 10; i++)
        {
            addProduct(tProduct[i]);
        }
        for (int i = 0; i < 20; i++)
        {
            addOrder(tOrder[i]);
        }
        for (int i = 0; i < 40; i++)
        {
            addOrderItem(tOrderItem[i]);
        }
    }
    static internal class Config
    {
        static internal int _orderIndex = 0;
        static internal int _productIndex = 0;
        static internal int _orderItemIndex = 0;
        static private int _orderID=100000;
        static private int _productID=100000;
        static private int _orderItemID = 100000;

        public static int ProductID
        {
            get { return _productID++; }
        }
        public static int OrderID
        {
            get { return _orderID++; }
        }
        public static int OrderItemID
        {
            get { return _orderItemID++; }
        }

    }

}
