using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using vlabver01.Models;

namespace vlabver01.Context
{
    public class PLCContext : DbContext
    {
        public DbSet<PLC> PLC { get; set; }
    }
}