
using BlApi;
using BlImplementation;
using BO;


namespace BlTest;

internal class Program
{
    static private IBl _bl = new Bl();
    static void Main(string[] args)
    {
        Entity entityChoice;
        do
        {
            Console.WriteLine("enter 1 for Products");
            Console.WriteLine("enter 2 for Orders");
            Console.WriteLine("enter 3 for carts");
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
                    _cartsChoosen();
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
        CrudProduct productChoices;
        do
        {
            Console.WriteLine("Enter 1 to create new product");
            Console.WriteLine("Enter 2 to get info about product by ID");
            Console.WriteLine("Enter 3 to get all the products info");
            Console.WriteLine("Enter 4 to update a product");
            Console.WriteLine("Enter 5 to delete a product");
            Console.WriteLine("Enter 6 to get product for catalog");
            Console.WriteLine("Enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out productChoices);

            switch (productChoices)
            {
                case CrudProduct.Create:
                    Console.WriteLine("Enter product name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter product price");
                    double price = 0;
                    try
                    {
                        price = Double.Parse(Console.ReadLine());
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                        price = Double.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("Enter product category");
                    Console.WriteLine("our categories:");
                    Console.WriteLine("Family, Race, Sport, SUV, Tiny, Motorcycle, VIP, Big");
                    Category category;
                    Enum.TryParse(Console.ReadLine(), out category);
                    Console.WriteLine("Enter amount in stock");
                    int instock = int.Parse(Console.ReadLine());
                    Console.WriteLine("product #" + _bl.Product.AddProduct(new Product() { Category = category, InStock = instock, Name = name, Price = price }) + " created");
                    break;

                case CrudProduct.Read:
                    Console.WriteLine("Enter product ID");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(_bl.Product.Read(id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CrudProduct.ReadAll:
                    foreach (var p in _bl.Product.ReadAll())
                    {
                        Console.WriteLine(p);
                    }
                    break;
                case CrudProduct.Delete:
                    Console.WriteLine("Enter product ID for delete");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        _bl.Product.DelProduct(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CrudProduct.Update:
                    Console.WriteLine("Enter product ID to update");
                    id = int.Parse(Console.ReadLine());
                    Product tempProduct = _bl.Product.Read(id);
                    Console.WriteLine(tempProduct);
                    Console.WriteLine("Enter product name");
                    string input = Console.ReadLine();
                    if (input != "")
                        tempProduct.Name = input;
                    Console.WriteLine("Enter product price");
                    input = Console.ReadLine();
                    try
                    {
                        tempProduct.Price = Double.Parse(input);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                        tempProduct.Price = Double.Parse(Console.ReadLine());
                    }
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
                    try
                    {
                        _bl.Product.UpdateProduct(tempProduct);
                    }
                    catch (ExceptionEntityNotFound exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CrudProduct.ReadByIDAndCart:
                    Cart cart = new Cart();
                    Console.WriteLine("Enter product id");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Product.Read(id, cart));
                    break;
                case CrudProduct.Exit:
                    break;
                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;

            }
        } while (productChoices != CrudProduct.Exit);

    }

    private static void _ordersChoosen()
    {
        CrudOrder orderChoices;
        do
        {
            Console.WriteLine("Enter 1 to get order info by id");
            Console.WriteLine("Enter 2 to get all the orders info");
            Console.WriteLine("Enter 3 to update an order");
            Console.WriteLine("Enter 4 to ship order");
            Console.WriteLine("Enter 5 to deliver order");
            Console.WriteLine("Enter 6 for order tracking");
            Console.WriteLine("Enter 0 to exit");
            Enum.TryParse(Console.ReadLine(), out orderChoices);
            switch (orderChoices)
            {
                case CrudOrder.Exit:
                    break;
                case CrudOrder.Read:
                    Console.WriteLine("enter order id");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        BO.Order order = _bl.Order.Read(id);
                        Console.WriteLine(order);
                        {
                            Console.WriteLine(order);
                        }
                    }catch(BO.ExceptionEntityNotFound exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CrudOrder.ReadAll:
                    foreach (var item in _bl.Order.ReadAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case CrudOrder.UpdateOrder:

                    break;
                case CrudOrder.ShipOrder:
                    Console.WriteLine("Enter shipping order id");
                    id=int.Parse(Console.ReadLine());
                    _bl.Order.ShipOrder(id);
                    break;
                case CrudOrder.DeliveryOrder:
                    Console.WriteLine("Enter delivering order id");
                    id = int.Parse(Console.ReadLine());
                    _bl.Order.DeliveryOrder(id);
                    break;
                case CrudOrder.OrderTracking:
                    Console.WriteLine("Enter order id for tracking");
                    id = int.Parse(Console.ReadLine());
                    _bl.Order.OrderTracking(id);
                    break;
                default:
                    break;
            }
        } while (orderChoices != CrudOrder.Exit);

    }

    private static void _cartsChoosen()
    {
        CrudProduct productChoices;
        do
        {
            Console.WriteLine("Enter 1 to create new product");
            Console.WriteLine("Enter 2 to get info about product by ID");
            Console.WriteLine("Enter 3 to get all the products info");
            Console.WriteLine("Enter 4 to update a product");
            Console.WriteLine("Enter 5 to delete a product");
            Console.WriteLine("Enter 6 to get product for catalog");
            Console.WriteLine("Enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out productChoices);
        } while (productChoices != CrudProduct.Exit);

    }
}
