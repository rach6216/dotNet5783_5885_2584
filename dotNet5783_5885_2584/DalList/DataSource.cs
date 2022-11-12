namespace Dal;
using DO;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using static DO.Enums;

/// <summary>
/// static structure for storing the data
/// </summary>
internal static class DataSource
{
    #region Fields in Data source (arrays of data)
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
    #endregion

    #region Static ctor and init fuction
    /// <summary>
    /// static ctor-init the data array
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// init the data arrays
    /// </summary>
    static private void s_Initialize()
    {
        (string, int, Category, int)[] productDetails = {
        ("Chevrolet Spark",81700,Category.Family,8),
       ("Tesla 2017 Model S",250000,Category.VIP,4),
        ("Skoda Tsi Ambition Oktavia",104600,Category.Family,1),
        ("Ford Mustang GT",80500,Category.Sport,0),
       ("Chevrolet Camero LT",95650,Category.Sport,6),
        ("Ligier JS 53 EVO 2",178000,Category.Race,2),
        ("Smart Fortwo 60kW EQ Premium",15000,Category.Tiny,10),
        ("Chrysler Pacifica Touring L",91000,Category.Big,9),
        ("Mercedes-AMG G63 AMG 4x4²",600000,Category.SUV,3),
        ("Yamaha TRACER 9 ",75985,Category.Motorcycle,15)
        };
        OrderItem[] orderDetails = new OrderItem[40];
        (string, string, string)[] userDetails = {("Shira","sh3123373@gcom","zeev chaklay"),
        ("Rachel","rf3123373@gcom","bergman 5"),
        ("Rivka","rhano@gcom","Ramot"),
        ("Danz","dzilbers@gmail.com","hunollolo"),
        ("yeudisf","yeudisf@gcom","wherever")
        };

        foreach ((string, int, Category, int) item in productDetails)
        {
            addProduct(new Product(item.Item1, item.Item2, item.Item3, item.Item4));
        }
        for (int i = 0; i < 9; i++)
        {
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(1, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            DateTime dd = sd + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, sd, dd, Config.OrderID));
        }
        for (int i = 0; i < 7; i++)
        {
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(1, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, sd, Config.OrderID));
        }
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine("3");
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(1, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, Config.OrderID));
        }
        int k = 0;
        for (int i = 0; i < 20&&k<40; i++)
        {
            Console.WriteLine("4");
            int j = rnd.Next(1, 4);
            for (int j2 = 0; j2 < j&&k<40; j2++)
            {
                k++;
                Product product = s_products[rnd.Next(10)];
                addOrderItem(new OrderItem(product.ID, s_orders[i].ID, product.Price, rnd.Next(1, 10)));
            }
        }
    }
    #endregion

    #region Adding functions for the data arrays
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
        s_products[Config.s_productIndex++] = newProduct;
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
    #endregion

    #region Configuration inner class
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

    #endregion

}
