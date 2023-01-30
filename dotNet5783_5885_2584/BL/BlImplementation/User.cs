using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

public class User:IUser
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();

    public void AddItemToCart(int userID, string pass, List<OrderItem>? oi)
    {
        
        //if (myUser != null)
        //{
        //    _dal?.User.Update(new DO.User() { ID=myUser.ID,CartItems=myUser.Cart.Items.Select(x=>new DO.OrderItem() { Amount=x.Amount,ID=x.ID,Price=x.Price,ProductID=x.ProductID}).ToList<DO.OrderItem?>(),CustomerAddress=myUser.Cart?.CustomerAddress,CustomerEmail=myUser.Cart.CustomerEmail,CustomerName=myUser.Cart.CustomerName,IsManager=myUser.IsAdmin,Orders=myUser.Orders,Password=myUser.Password,UserName=myUser.UserName};
        //}
      
         
        DO.User user = _dal?.User.Read(x => x?.ID == userID)??new DO.User();
        user.CartItems ??= new();
        if (oi != null)
        {
            user.CartItems = oi?.Select(oi => (DO.OrderItem?)(new DO.OrderItem() { Amount = oi.Amount, ID = oi.ID, Price = oi.Price, ProductID = oi.ProductID })).ToList<DO.OrderItem?>();
        }
        // user.CartItems?.Add(new DO.OrderItem() { Amount=oi.Amount,ID=oi.ID,Price=oi.Price,ProductID=oi.ProductID});
        _dal?.User.Update(user);
    }

    public void AddOrder(string pass,int UserID,int orderID)
    {
        DO.User? user = _dal?.User.Read(x => x?.ID == UserID);
        if (user?.Password == pass)
        {
            user?.Orders.Add(orderID);
            _dal?.User.Update(user ?? default);
        }
        
    }

    public BO.User Login(string username, string password)
    {
        DO.User? user = _dal?.User.Read(x => x?.UserName==username);
        if (user?.Password == password)
        {

            return Read(x => x?.UserName == user?.UserName);
        }
        throw new ExceptionEntityNotFound("user not found");
    }
   
    public BO.User Read(Func<DO.User?, bool>? f)
    {if (f != null)
        {
            DO.User u = _dal?.User.Read(x => f(x))??default;
            u.CartItems ??= new();
            List<BO.OrderItem? > orderItems = (from oi in u.CartItems
                                              select new BO.OrderItem() { Amount = oi?.Amount ?? 0, ID = oi?.ID ?? 0, Price = oi?.Price ?? 0, ProductID = oi?.ProductID ?? 0, ProductName = _dal?.Product.Read(x => x?.ID == oi?.ProductID).Name ?? " ", TotalPrice = oi?.Price ?? 0 * oi?.Amount ?? 0 }).ToList();
            double total = orderItems.Sum(x => x.TotalPrice);
            return new BO.User() {Password=u.Password, ID = u.ID, Orders = u.Orders, UserName = u.UserName,IsAdmin=u.IsManager, Cart = new BO.Cart() { CustomerAddress = u.CustomerAddress, CustomerEmail = u.CustomerEmail, CustomerName = u.CustomerName, Items = orderItems, TotalPrice = total } };
        }
        throw new ExceptionEntityNotFound("user not found");
    }

    public int SignUp(string username, string password, string cname, string cemail, string caddress,bool isAdmin)
    {
        return _dal?.User.Create(new DO.User() {IsManager=isAdmin, CustomerName = cname, CustomerEmail = cemail, CustomerAddress = caddress, UserName = username, Password = password }) ?? 0;
    }

    public void UpdateOrder(string pass, int UserID, int orderID)
    {
        DO.User? user = _dal?.User.Read(x => x?.ID == UserID);
        if(user != null && user?.Password == pass)
        {
            user?.Orders.Add(orderID);
            _dal?.User.Update(user ?? new DO.User());
        }
        }
}

