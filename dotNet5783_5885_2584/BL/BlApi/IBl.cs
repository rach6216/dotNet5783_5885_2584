using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBl
    {
        /// <summary>
        /// cart interface
        /// </summary>
        public ICart Cart { get; }
        /// <summary>
        /// product interface
        /// </summary>
        public IProduct Product { get; }
        /// <summary>
        /// order interface
        /// </summary>
        public IOrder Order { get; }
    }
}
