namespace Dal;
using DO;
using System.Reflection;



/// <summary>
/// static structure for storing the data
/// </summary>
internal static class DataSource
{
   
    #region Fields in Data source (Lists of data)
    /// <summary>
    /// random var for random actions
    /// </summary>
    static readonly Random rnd = new Random();
    /// <summary>
    /// static arrays for storing orders,products and order-items
    /// </summary>
    static internal List<Order?> s_orders=new();
    static internal List<Product?> s_products=new();
    static internal List<OrderItem?> s_orderItems = new();
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
        List<(string, int, Category, int)> productDetails =new() {
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

        productDetails.ForEach(item => addProduct(new Product() { Name = item.Item1, Price = item.Item2, Category = item.Item3, InStock = item.Item4 }));
        for (int i = 0; i < 9; i++)
        {
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            DateTime dd = sd + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, sd, dd, Config.OrderID));
        }
        for (int i = 0; i < 7; i++)
        {
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, sd, Config.OrderID));
        }
        for (int i = 0; i < 4; i++)
        {
            (string, string, string) user = userDetails[rnd.Next(userDetails.Length)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            addOrder(new Order(user.Item1, user.Item2, user.Item3, od, Config.OrderID));
        }
        int k = 0;
        for (int i = 0; i < 20 && k < 40; i++)
        {
            int j = rnd.Next(1, 4);
            for (int j2 = 0; j2 < j && k < 40; j2++)
            {
                k++;
                int r=rnd.Next(10);
                if (s_products[r] != null && s_orders[i]!=null)
                {
                    int id = s_orders[i]?.ID??0;
                    Product product = (Product)s_products[r];
                    addOrderItem(new OrderItem(product.ID,id, product.Price, rnd.Next(1, 10)));
                }
                
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
        s_orders.Add(newOrder);
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
            s_products.ForEach(x => { if (x?.ID == tID) tID = 0; });
        } while (tID == 0);
        newProduct.ID = tID;
        s_products.Add(newProduct);
    }
    /// <summary>
    /// adding order-item to the order-items array
    /// </summary>
    /// <param name="newOrderItem">order-item to add</param>
    static private void addOrderItem(OrderItem newOrderItem)
    {
        newOrderItem.ID = Config.OrderItemID;
        s_orderItems.Add(newOrderItem);
    }
    #endregion

    #region Configuration inner class
    /// <summary>
    /// inner class for configurations of the data array structures
    /// </summary>
    static internal class Config
    {
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
