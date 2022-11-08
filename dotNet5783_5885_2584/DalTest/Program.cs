namespace DalTest;
using DO;
using Dal;
using static DO.Enums;
using System.Net;
using System.Xml.Linq;
using System;

internal class Program
{
    private static DalOrder _dalOrder = new DalOrder();
    internal static DalProduct _dalProduct = new DalProduct();
    private static DalOrderItem _dalOrderItem = new DalOrderItem();

    private static void Main(string[] args)
    {
        Entity entityChoice;
        do
        {
            Console.WriteLine("enter 1 for Products");
            Console.WriteLine("enter 2 for Orders");
            Console.WriteLine("enter 3 for OrderItems");
            Console.WriteLine("enter 0 to exit");
            Enum.TryParse(Console.ReadLine(), out entityChoice);
            switch (entityChoice)
            { 
                case Entity.Products:
                    _productChoosen();
                    break;
                case Entity.Orders:
                    _ordersChoosen();
                    break;

                case Entity.OrderItems:
                    _orderItemsChoosen();
                    break;

                case Entity.Exit:
                    break;

                default:
                    Console.WriteLine("choice is not exist");
                    break;
            }
        }
        while (entityChoice != Entity.Exit);
           
    }

    internal static void _productChoosen()
    {
        CRUDOp productChoices;
        do
        {
            Console.WriteLine("Enter 1 to create new product");
            Console.WriteLine("Enter 2 to get info about product by ID");
            Console.WriteLine("Enter 3 to get all the products info");
            Console.WriteLine("Enter 4 to update a product");
            Console.WriteLine("Enter 5 to delete a product");
            Console.WriteLine("Enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out productChoices);

            switch (productChoices)
            {
                case CRUDOp.Create:
                    Console.WriteLine("Enter product name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter product price");
                    double price = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter product category");
                    //show the categories
                    Category category;
                    Enum.TryParse(Console.ReadLine(), out category);
                    Console.WriteLine("Enter amount in stock");
                    int instock = int.Parse(Console.ReadLine());
                    Console.WriteLine("product #"+_dalProduct.Create(new Product(name, price, category, instock))+"created");
                   
                    break;

                case CRUDOp.Read:
                    Console.WriteLine("Enter product ID");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(_dalProduct.Read(id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }

                    break;
                case CRUDOp.ReadAll:
                    foreach (Product p in _dalProduct.Read())
                    {
                        Console.WriteLine(p);
                    }
                    break;
                case CRUDOp.Delete:
                    Console.WriteLine("Enter product ID for delete");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        _dalProduct.Delete(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }
                    break;
                case CRUDOp.Update:
                    Console.WriteLine("Enter product ID to update");
                    id = int.Parse(Console.ReadLine());
                    Product tempProduct = _dalProduct.Read(id);
                    try
                    {
                        Console.WriteLine(tempProduct);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }

                    Console.WriteLine("Enter product name");
                    string input = Console.ReadLine();
                    if (input != "")
                        tempProduct.Name = input;
                    Console.WriteLine("Enter product price");
                    input = Console.ReadLine();
                    if (input != "")
                        tempProduct.Price = Double.Parse(input);
                    Console.WriteLine("Enter product category");
                    //show the categories
                    input = Console.ReadLine();
                    Category c;
                    if (input != "")
                    {
                        Enum.TryParse(input, out c);
                        tempProduct.Category = c;
                    }

                    Console.WriteLine("Enter amount in stock");
                    input = Console.ReadLine();
                    if (input != "")
                        tempProduct.InStock = int.Parse(input);
                    _dalProduct.Update(tempProduct);
                    break;


                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;


            }
        }while(productChoices!=CRUDOp.Exit);
        
    }
    internal static void _ordersChoosen()
    { 
        CRUDOp orderChoices;
        do 
        {
            Console.WriteLine("Enter 1 to create new order");
            Console.WriteLine("Enter 2 to get info about your order by ID");
            Console.WriteLine("Enter 3 to get all the your orders info");
            Console.WriteLine("Enter 4 to update a order");
            Console.WriteLine("Enter 5 to delete a order");
            Console.WriteLine("Enter 0 to exit");
            Enum.TryParse(Console.ReadLine(), out orderChoices);
            switch (orderChoices)
            {
                case CRUDOp.Create:
                    Console.WriteLine("create");
                    Console.WriteLine("Enter name");
                    string cName = Console.ReadLine();
                    Console.WriteLine("Enter email");
                    string cEmail = Console.ReadLine();
                    Console.WriteLine("Enter address");
                    string cAddress = Console.ReadLine();

                    //the ship and delivery date will update in the order ctor
                    Console.WriteLine("order #" + _dalOrder.Create(new Order(cName, cEmail, cAddress, DateTime.Now, DateTime.Now, DateTime.Now)) + " created");
              
                    break;

                case CRUDOp.Delete:
                    Console.WriteLine("delete");
                    Console.WriteLine("Enter order ID for delete");
                    int id = int.Parse(Console.ReadLine());
                    //add condition that check if the order can be deleted
                    try
                    {
                        _dalOrder.Delete(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }

                    break;

                case CRUDOp.Read:
                    Console.WriteLine("read");
                    Console.WriteLine("Enter order ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(_dalOrder.Read(id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }

                    break;

                case CRUDOp.Update:
                    Console.WriteLine("update");
                    Console.WriteLine("Enter order ID to update");
                    //add condition that check if the order can updating
                    id = int.Parse(Console.ReadLine());
                    Order tempOrder = _dalOrder.Read(id);
                    try
                    {
                        Console.WriteLine(tempOrder);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }
                    Console.WriteLine("Enter name");
                    string inp= Console.ReadLine();
                    if (inp != "")
                        tempOrder.CustomerName = inp;
                    Console.WriteLine("Enter email");
                    inp = Console.ReadLine();
                    if (inp != "")
                        tempOrder.CustomerEmail = inp;
                    Console.WriteLine("Enter adress");
                    inp = Console.ReadLine();
                    if (inp != "")
                        tempOrder.CustomerAddress = inp;
                    _dalOrder.Update(tempOrder);
                   
                    break;

                case CRUDOp.ReadAll:
                    //In the future, the user will be saved automatically and we will not have to ask for details
                    Console.WriteLine("Enter name");
                    cName = Console.ReadLine();
                    foreach (Order o in _dalOrder.Read())
                    {
                       if (o.CustomerName == cName)
                          Console.WriteLine(o);
                    }

                    break;

                default:
                    Console.WriteLine("No such option please enter correct choice");
                   
                    break;

            } 
        } while (orderChoices != CRUDOp.Exit);
    }
    internal static void _orderItemsChoosen()
    {
        Console.WriteLine("Enter 1 to create new product");
        Console.WriteLine("Enter 2 to get info about product by ID");
        Console.WriteLine("Enter 3 to get all the products info");
        Console.WriteLine("Enter 4 to update a product");
        Console.WriteLine("Enter 5 to delete a product");
        Console.WriteLine("Enter 0 to exit");
        CRUDOp productChoices;
        Enum.TryParse(Console.ReadLine(), out productChoices);
        while (productChoices != CRUDOp.Exit)
        {
            switch (productChoices)
            {
                case CRUDOp.Create:
                   


                    break;
                case CRUDOp.Delete:

                    break;
                case CRUDOp.Read:

                    break;
                case CRUDOp.Update:
                    break;
                case CRUDOp.ReadAll:
                    break;

                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;

            }
        }
    }
}