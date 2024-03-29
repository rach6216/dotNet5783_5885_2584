﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Dal;



internal class DataRepos
{


    #region Fields in Data source (Lists of data)
    /// <summary>
    /// random var for random actions
    /// </summary>
    static readonly Random rnd = new Random();
    /// <summary>
    /// static arrays for storing orders,products and order-items
    /// </summary>
    static internal List<DO.Order> Initorders = new();
    static internal List<DO.Product> Initproducts = new();
    static internal List<DO.OrderItem> InitorderItems = new();
    static internal List<DO.User> Initusers = new();
    #endregion

    #region Static ctor and init fuction
    /// <summary>
    /// static ctor-init the data array
    /// </summary>
    static DataRepos()
    {
        //s_Initialize();
    }
    /// <summary>
    /// init the data arrays
    /// </summary>
    static public void s_Initialize()
    {
        XElement identify = XMLTools.LoadListFromXMLElement("config.xml");
        var el = identify.Elements().ToList();
        //  List<object> list = XMLTools.LoadListFromXMLSerializer<object>("config.xml");
        var userID = int.Parse(identify.Elements().ToList()[0].Value);
        var orderID = int.Parse(identify.Elements().ToList()[1].Value);
        var orderItemID = int.Parse(identify.Elements().ToList()[2].Value);
        XElement products = XMLTools.LoadListFromXMLElement("Product.xml");
        //Initorders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Order.xml");
        //Initusers = XMLTools.LoadListFromXMLSerializer<DO.User>("User.xml");
        //InitorderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItem.xml");

        List<(string, int, DO.Category, int)> productDetails = new() {
        ("Chevrolet Spark",81700,DO.Category.Family,8),
       ("Tesla 2017 Model S",250000,DO.Category.VIP,4),
        ("Skoda Tsi Ambition Oktavia",104600,DO.Category.Family,1),
        ("Ford Mustang GT",80500,DO.Category.Sport,0),
       ("Chevrolet Camero LT",95650,DO.Category.Sport,6),
        ("Ligier JS 53 EVO 2",178000,DO.Category.Race,2),
        ("Smart Fortwo 60kW EQ Premium",15000,DO.Category.Tiny,10),
        ("Chrysler Pacifica Touring L",91000,DO.Category.Big,9),
        ("Mercedes-AMG G63 AMG 4x4²",600000,DO.Category.SUV,3),
        ("Yamaha TRACER 9 ",75985,DO.Category.Motorcycle,15)
        };
        List<DO.OrderItem> orderDetails = new();
        List<(string, string, string, bool, string)> userDetails = new() {("Shira","sh3123373@gcom","zeev chaklay",true, "s1"),
        ("Rachel","rf3123373@gcom","bergman 5",false, "11"),
        ("Rivka","rhano@gcom","Ramot",false,"dz"),
        ("Danz","dzilbers@gmail.com","hunollolo",true,"23"),
        ("yeudisf","yeudisf@gcom","wherever",true,"90")
        };
        userDetails.ForEach(x => addUser(new DO.User() { ID = userID++, CustomerAddress = x.Item3, CustomerEmail = x.Item2, CustomerName = x.Item1, UserName = x.Item1, IsManager = x.Item4, Password = x.Item5.ToString() }));

        productDetails.ForEach(item => addProduct(new DO.Product() { Name = item.Item1, Price = item.Item2, Category = item.Item3, InStock = item.Item4 }));
        for (int i = 0; i < 9; i++)
        {
            (string, string, string, bool, string) user = userDetails[rnd.Next(userDetails.Count)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            DateTime dd = sd + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60));
            addOrder(new DO.Order(user.Item1, user.Item2, user.Item3, od, sd, dd, orderID++));
        }
        for (int i = 0; i < 7; i++)
        {
            (string, string, string, bool, string) user = userDetails[rnd.Next(userDetails.Count)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            DateTime sd = od + new TimeSpan(rnd.Next(10), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            addOrder(new DO.Order(user.Item1, user.Item2, user.Item3, od, sd, orderID++));
        }
        for (int i = 0; i < 4; i++)
        {
            (string, string, string, bool, string) user = userDetails[rnd.Next(userDetails.Count)];
            DateTime od = new DateTime(rnd.Next(2000, DateTime.Now.Year), rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day));
            addOrder(new DO.Order(user.Item1, user.Item2, user.Item3, od, orderID++));
        }
        int k = 0;
        for (int i = 0; i < 20 && k < 40; i++)
        {
            int j = rnd.Next(1, 4);
            for (int j2 = 0; j2 < j && k < 40; j2++)
            {
                k++;
                int r = rnd.Next(10);
                
                    int id = Initorders[i].ID;
                    DO.Product product = Initproducts[r];
                    addOrderItem(new DO.OrderItem(product.ID, id, product.Price, rnd.Next(1, 10),orderItemID++));
                

            }
        }
        identify.Element("UserID").Value = userID.ToString();
        identify.Element("OrderID").Value = orderID.ToString();
        identify.Element("OrderItemID").Value = orderItemID.ToString();
        Initproducts.ForEach(x => products.Add(new XElement("Product", new XElement("ID", x.ID),
                                   new XElement("Name", x.Name),
                                   new XElement("Category", x.Category),
                                   new XElement("InStock", x.InStock.ToString()),
                                   new XElement("Price", x.Price)
                                  )));
        XMLTools.SaveListToXMLElement(identify, "config.xml");
        XMLTools.SaveListToXMLElement(products, "Product.xml");
        XMLTools.SaveListToXMLSerializer(Initusers, "User.xml");
        XMLTools.SaveListToXMLSerializer(Initorders, "Order.xml");
        XMLTools.SaveListToXMLSerializer(InitorderItems, "OrderItem.xml");
    }
    #endregion

    #region Adding functions for the data arrays
    /// <summary>
    /// adding order to the orders array
    /// </summary>
    /// <param name="newOrder">order to add</param>
    static private void addOrder(DO.Order newOrder)
    {
        Initorders.Add(newOrder);
       }
    /// <summary>
    /// adding product to the products array,generating new random id
    /// </summary>
    /// <param name="newProduct">product to add</param>
    static private void addProduct(DO.Product newProduct)
    {
        // p.ID = Config.ProductID;
        Random r = new Random();
        int tID;
        //generate random id
        do
        {
            tID = r.Next(10000000, 99999999);
            Initproducts.ForEach(x => { if (x.ID == tID) tID = 0; });
        } while (tID == 0);
        newProduct.ID = tID;
        Initproducts.Add(newProduct);
    }

    static private void addUser(DO.User newUser)
    {
        Initusers.Add(newUser);
    }
    /// <summary>
    /// adding order-item to the order-items array
    /// </summary>
    /// <param name="newOrderItem">order-item to add</param>
    static private void addOrderItem(DO.OrderItem newOrderItem)
    {
       // newOrderItem.ID = Config.OrderItemID;
        InitorderItems.Add(newOrderItem);
    }
    #endregion

  
}
