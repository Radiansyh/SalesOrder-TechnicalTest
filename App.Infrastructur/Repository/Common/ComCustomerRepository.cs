using Microsoft.EntityFrameworkCore;
using SalesOrder.Domain.Entities.Common;
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
        public ComCustomerRepository(SalesOrderContext context) : base(context) { }
    }
}
