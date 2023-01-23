using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class User : DalApi.IUser
{
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
        throw new NotImplementedException();
    }

    public DO.User Read(Func<DO.User?, bool>? f)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.User?> ReadAll(Func<DO.User?, bool>? f = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.User entity)
    {
        throw new NotImplementedException();
    }
}
