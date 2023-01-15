using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IUser
{
    public BO.User Login(string username, string password);
    public void AddOrder(string pass, int UserID,int orderID);
    public void UpdateOrder(string pass, int UserID,int orderID);
    public BO.User Read(Func<DO.User?, bool>? f);
    public int SignUp(string username, string password, string cname, string cemail, string caddress,bool isAdmin);

    public void AddItemToCart(int userID, string pass, BO.OrderItem oi);
     
    



}
