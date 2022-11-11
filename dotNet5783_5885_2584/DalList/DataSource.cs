namespace Dal;
using DO;

/// <summary>
/// static structure for storing the data
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// random var for random actions
    /// </summary>
    static readonly Random rnd = new Random();
    /// <summary>
    /// static arrays for storing orders,products and order-items
    /// </summary>
    static internal Order[] s_orders = new Order[100];
    static internal Product[] s_products = new Product[50];
    static internal OrderItem[] s_orderItems = new OrderItem[200];
    /// <summary>
    /// static ctor-init the data array
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// adding order to the orders array
    /// </summary>
    /// <param name="newOrder">order to add</param>
    static private void addOrder(Order newOrder)
    {
        s_orders[Config.s_orderIndex++] = newOrder;
    }
    /// <summary>
    /// adding product to the products array,generating new random id
    /// </summary>
    /// <param name="newProduct">product to add</param>
    static private void addProduct(Product newProduct)
    {
        // p.ID = Config.ProductID;
        Random r = new Random();
        int tID;
        //generate random id
        do
        {
            tID = r.Next(10000000, 99999999);
            foreach (Product item in s_products)
            {
                if (item.ID == tID)
                {
                    tID = 0;
                    break;
                }
            }
        } while (tID == 0);
        newProduct.ID = tID;
        s_products[Config.s_productIndex] = newProduct;
        //newProduct.ID = Config.ProductID;
        //s_products[Config.s_productIndex++] = newProduct;
    }
    /// <summary>
    /// adding order-item to the order-items array
    /// </summary>
    /// <param name="newOrderItem">order-item to add</param>
    static private void addOrderItem(OrderItem newOrderItem)
    {
        newOrderItem.ID = Config.OrderItemID;
        s_orderItems[Config.s_orderItemIndex++] = newOrderItem;
    }
    /// <summary>
    /// init the data arrays
    /// </summary>
    static private void s_Initialize()
    {
        Product[] tProduct = new Product[10];
        Order[] tOrder = new Order[20];
        OrderItem[] tOrderItem = new OrderItem[40];
        for (int i = 0; i < 10; i++)
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
    /// <summary>
    /// inner class for configurations of the data array structures
    /// </summary>
    static internal class Config
    {
        /// <summary>
        /// next places in the orders array
        /// </summary>
        static internal int s_orderIndex = 0;
        /// <summary>
        /// next places in the products array
        /// </summary>
        static internal int s_productIndex = 0;
        /// <summary>
        /// next places in the order-items array
        /// </summary>
        static internal int s_orderItemIndex = 0;
        /// <summary>
        /// next order-item id
        /// </summary>
        static private int s_orderItemID = 100000;
        /// <summary>
        /// next order id 
        /// </summary>
        static private int s_orderID = 100000;
        /// <summary>
        /// next product id
        /// </summary>
        //static private int s_productID = 100000;

        //public static int ProductID
        //{
        //    get { return s_productID++; }
        //}
        public static int OrderItemID
        {
            get { return s_orderItemID++; }
        }
        public static int OrderID
        {
            get { return s_orderID++; }
        }

    }

}
