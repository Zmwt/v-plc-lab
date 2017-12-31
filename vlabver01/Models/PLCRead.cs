using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLCLAB.Models
{
    public class PLCRead
    {
        public int StartingAddress { get; set; }
        public int Quantity { get; set; }
        public int FunctionN { get; set; }

        public dynamic Val { get; set; } 

    }
}