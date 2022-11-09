﻿namespace DalTest;
using DO;
using Dal;
using static DO.Enums;
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
                        Console.WriteLine(exp.Message);
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
                        Console.WriteLine(exp.Message);
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
                        Console.WriteLine(exp.Message);
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
            Console.WriteLine("Enter 3 to get all your orders info");
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
        CRUDOp orderItemChoices;
        do
        {
            Console.WriteLine("Enter 1 to create new order-item");
            Console.WriteLine("Enter 2 to get info about order-item by ID");
            Console.WriteLine("Enter 3 to get all the order-items info");
            Console.WriteLine("Enter 4 to update an order-item");
            Console.WriteLine("Enter 5 to delete an order-item");
            Console.WriteLine("Enter 6 to get an order item by order id and product id");
            Console.WriteLine("Enter 7 to get all the items in an order");
            Console.WriteLine("Enter 0 to exit");
        
            Enum.TryParse(Console.ReadLine(), out orderItemChoices);

        
            switch (orderItemChoices)
            {
                case CRUDOp.Create:
                    Console.WriteLine("Enter order id: ");
                    int orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter product id");
                    int productID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter price of item");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter amount :");
                    int amount = int.Parse(Console.ReadLine());
                    Console.WriteLine("order item #" + _dalOrderItem.Create(new OrderItem(productID, orderID, price, amount)) + "created");
                    break;
                case CRUDOp.Delete:
                    Console.WriteLine("Enter orderItem ID for delete");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        _dalOrderItem.Delete(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CRUDOp.Read:
                    Console.WriteLine("Enter Order-Item ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(_dalOrderItem.Read(id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CRUDOp.ReadAll:
                    foreach (OrderItem oi in _dalOrderItem.Read())
                    {
                        Console.WriteLine(oi);
                    }
                    break;
                case CRUDOp.ReadByProductAndOrder:
                    Console.WriteLine("Enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter product id");
                    productID = int.Parse(Console.ReadLine());
                    Console.WriteLine(_dalOrderItem.Read(orderID, productID));
                    break;
                case CRUDOp.ReadAllByOrder:
                    Console.WriteLine("Enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    foreach (OrderItem oi in _dalOrderItem.ReadByOrder(orderID))
                    {
                        Console.WriteLine(oi);
                    }
                    break;

                case CRUDOp.Update:
                    Console.WriteLine("Enter order-item ID to update");
                    id = int.Parse(Console.ReadLine());
                    OrderItem tempOrderItem = _dalOrderItem.Read(id);
                    try
                    {
                        Console.WriteLine(tempOrderItem);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }

                    Console.WriteLine("Enter order id name");
                    string input = Console.ReadLine();
                    if (input != "")
                        tempOrderItem.OrderID = int.Parse(input);
                    Console.WriteLine("Enter product id price");
                    input = Console.ReadLine();
                    if (input != "")
                        tempOrderItem.ProductID = int.Parse(input);

                    Console.WriteLine("Enter price of item");
                    input = Console.ReadLine();
                    if (input != "")
                        tempOrderItem.Price = double.Parse(input);
                    Console.WriteLine("Enter amount of item");
                    input = Console.ReadLine();
                    if (input != "")
                        tempOrderItem.Amount = int.Parse(input);
                    _dalOrderItem.Update(tempOrderItem);
                    break;

                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;

            }
        } while (orderItemChoices != CRUDOp.Exit);
    }
}