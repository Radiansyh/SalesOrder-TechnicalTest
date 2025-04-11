using Microsoft.EntityFrameworkCore;
using SalesOrder.Domain.Entities.Common;
using SalesOrder.Domain.Entities.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Infrastructure.Context
{
    public class SalesOrderContext : DbContext
    {
        public SalesOrderContext(DbContextOptions<SalesOrderContext> options) : base(options)
        { }

        public DbSet<ComCustomer> Customers { get; set; }
        public DbSet<SoItem> SoItems { get; set; }
        public DbSet<SoOrder> SoOrders { get; set; }
    }
}
