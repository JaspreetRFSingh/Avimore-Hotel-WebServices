using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Models
{
    public class ApiError
    {
        public string Message
        {
            get; set;
        }
        public int code { get; set; }
    }
}
