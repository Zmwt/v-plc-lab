using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PLCLAB.Models;

namespace PLCLAB.Context
{
    public class PLCContext : DbContext
    {
        public DbSet<PLC> PLC { get; set; }
    }
}