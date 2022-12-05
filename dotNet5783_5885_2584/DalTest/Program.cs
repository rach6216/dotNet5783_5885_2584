namespace DalTest;
using DO;
using Dal;


internal class Program
{
    static DalList DalList = new DalList();

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
                    Console.WriteLine("our categories:");
                    Console.WriteLine("Family, Race, Sport, SUV, Tiny, Motorcycle, VIP, Big");
                    Category category;
                    Enum.TryParse(Console.ReadLine(), out category);
                    Console.WriteLine("Enter amount in stock");
                    int instock = int.Parse(Console.ReadLine());
                    Console.WriteLine("product #" + DalList.Product.Create(new Product(){Name=name, Price=price, Category=category, InStock=instock}) + " created");
                    break;

                case CRUDOp.Read:
                    Console.WriteLine("Enter product ID");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(DalList.Product.Read(x=>x.Value.ID==id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }

                    break;
                case CRUDOp.ReadAll:
                    foreach (Product p in DalList.Product.ReadAll())
                    {
                        Console.WriteLine(p);
                    }
                    break;
                case CRUDOp.Delete:
                    Console.WriteLine("Enter product ID for delete");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        DalList.Product.Delete(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CRUDOp.Update:
                    Console.WriteLine("Enter product ID to update");
                    id = int.Parse(Console.ReadLine());
                    Product tempProduct = DalList.Product.Read(x=>x.Value.ID==id);
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
                    DalList.Product.Update(tempProduct);
                    break;
                case CRUDOp.Exit:
                    break;
                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;


            }
        } while (productChoices != CRUDOp.Exit);

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
                    Console.WriteLine("Enter name");
                    string cName = Console.ReadLine();
                    Console.WriteLine("Enter email");
                    string cEmail = Console.ReadLine();
                    Console.WriteLine("Enter address");
                    string cAddress = Console.ReadLine();

                    //the ship and delivery date will update in the order ctor
                    Console.WriteLine("order #" + DalList.Order.Create(new Order(cName, cEmail, cAddress, DateTime.Now)) + " created");
                    break;

                case CRUDOp.Delete:
                    Console.WriteLine("Enter order ID for delete");
                    int id = int.Parse(Console.ReadLine());
                    //add condition that check if the order can be deleted
                    try
                    {
                        DalList.Order.Delete(id);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }

                    break;

                case CRUDOp.Read:
                    Console.WriteLine("Enter order ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(DalList.Order.Read(x => x.Value.ID == id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }

                    break;

                case CRUDOp.Update:
                    Console.WriteLine("Enter order ID to update");
                    //add condition that check if the order can updating
                    id = int.Parse(Console.ReadLine());
                    Order tempOrder = DalList.Order.Read(x => x.Value.ID == id);
                    try
                    {
                        Console.WriteLine(tempOrder);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    Console.WriteLine("Enter name");
                    string inp = Console.ReadLine();
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
                    DalList.Order.Update(tempOrder);

                    break;

                case CRUDOp.ReadAll:
                    //In the future, the user will be saved automatically and we will not have to ask for details
                    //Console.WriteLine("Enter id");
                    //cName = Console.ReadLine();
                    foreach (Order o in DalList.Order.ReadAll())
                    {
                        //if (o.CustomerName == cName)
                        Console.WriteLine(o);
                    }

                    break;
                case CRUDOp.Exit:
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
                    Console.WriteLine("order item #" + DalList.OrderItem.Create(new OrderItem(productID, orderID, price, amount)) + "created");
                    break;
                case CRUDOp.Delete:
                    Console.WriteLine("Enter orderItem ID for delete");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        DalList.OrderItem.Delete(id);
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
                        Console.WriteLine(DalList.OrderItem.Read(x => x.Value.ID == id));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    break;
                case CRUDOp.ReadAll:
                    foreach (OrderItem oi in DalList.OrderItem.ReadAll())
                    {
                        Console.WriteLine(oi);
                    }
                    break;
                case CRUDOp.ReadByProductAndOrder:
                    Console.WriteLine("Enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter product id");
                    productID = int.Parse(Console.ReadLine());
                    Console.WriteLine(DalList.OrderItem.Read(x=>(x.Value.OrderID==orderID)&&(x.Value.ProductID==productID)));
                    break;
                case CRUDOp.ReadAllByOrder:
                    Console.WriteLine("Enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    foreach (var oi in DalList.OrderItem.ReadAll(x=>x.Value.OrderID==orderID))
                    {
                        Console.WriteLine(oi);
                    }
                    break;

                case CRUDOp.Update:
                    Console.WriteLine("Enter order-item ID to update");
                    id = int.Parse(Console.ReadLine());
                    OrderItem tempOrderItem = DalList.OrderItem.Read(x => x.Value.ID == id);
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
                    DalList.OrderItem.Update(tempOrderItem);
                    break;
                case CRUDOp.Exit:
                    break;
                default:
                    Console.WriteLine("No such option please enter correct choice");
                    break;

            }
        } while (orderItemChoices != CRUDOp.Exit);
    }
}