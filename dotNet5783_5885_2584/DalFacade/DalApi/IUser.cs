using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IUser : ICrud<User>
{
    public DO.User Login(string username, string password);
    public void AddOrder(int userID,int orderID);

}
