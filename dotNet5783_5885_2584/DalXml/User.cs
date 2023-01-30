using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class User : DalApi.IUser
{
    string UserFile = @"User.xml";

    public int ID { get; set; }
    public string? UserName { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerEmail { get; set; }

    public string? Password { get; set; }
    public bool IsManager { get; set; }

    public List<DO.OrderItem?>? CartItems { get; set; }

    public List<int>? Orders { get; set; }

    public int Create(DO.User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
            userList.RemoveAll(x => x == null || x?.ID == id);
            XMLTools.SaveListToXMLSerializer(userList, UserFile);
        }
        catch (DO.XMLFileLoadCreateException e)
        {
            throw new(e.Message);
        }
    }

    public DO.User Read(Func<DO.User?, bool>? f)
    {
        if (f != null)
        {
            List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
            DO.User? user = userList.FirstOrDefault(x => f(x));
            if (user == null)
                throw new DO.ExceptionEntityNotFound("user not found");
            return (DO.User)user;
        }
        else

            throw new DO.ExceptionEntityNotFound("user not found");
    }

    public IEnumerable<DO.User?> ReadAll(Func<DO.User?, bool>? f = null)
    {
        List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
        return userList.Where(x => f == null || f(x));
    }

    public void Update(DO.User entity)
    {
        List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
        userList.RemoveAll(x => x == null || x?.ID == entity.ID);
        userList.Add(entity);
        XMLTools.SaveListToXMLSerializer(userList, UserFile);
    }
}
