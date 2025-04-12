using Microsoft.EntityFrameworkCore;
using SalesOrder.Domain.Entities.Common;
using SalesOrder.Domain.Entities.SalesOrder;
using SalesOrder.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Infrastructure.Repository.Customer
{
    public interface IComCustomerRepository : IBaseRepository<ComCustomer>
    {
    }

    public class ComCustomerRepository : BaseRepository<ComCustomer>, IComCustomerRepository
    {
        private readonly SalesOrderContext _context;
        public ComCustomerRepository(SalesOrderContext context) : base(context)
        {
            _context = context;
        }

    }
}
