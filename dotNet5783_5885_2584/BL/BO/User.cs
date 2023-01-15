using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class User
{
    public int ID { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
     public BO.Cart? Cart { get; set; }
    public bool IsAdmin { get; set; }
    public List<int>? Orders { get; set; }
}
