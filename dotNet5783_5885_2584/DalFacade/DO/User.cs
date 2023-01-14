using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public struct User
{
    public int ID { get; set; } 
    public string UserName { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerEmail { get; set; }
    
    public string Password { get; set; }

    public List<OrderItem?> CartItems { get; set; }

    public List<int> Orders { get; set; }


}
