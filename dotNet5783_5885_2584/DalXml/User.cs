using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(DO.User entity)
    {
        List<DO.User> list = XMLTools.LoadListFromXMLSerializer<DO.User>(UserFile);
        var identify = XMLTools.LoadListFromXMLElement("config.xml");
        int id = int.Parse(identify.Elements().ToList()[0].Value);
        identify.Elements().ToList()[0].Value = (id + 1).ToString();
        entity.ID = id;
        list.Add(entity);
        XMLTools.SaveListToXMLElement(identify, "config.xml");
        XMLTools.SaveListToXMLSerializer(list,UserFile);
        return id;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.User?> ReadAll(Func<DO.User?, bool>? f = null)
    {
        List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
        return userList.Where(x => f == null || f(x));
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.User entity)
    {
        List<DO.User?> userList = XMLTools.LoadListFromXMLSerializer<DO.User?>(UserFile);
        userList.RemoveAll(x => x == null || x?.ID == entity.ID);
        userList.Add(entity);
        XMLTools.SaveListToXMLSerializer(userList, UserFile);
    }
}
