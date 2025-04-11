using SalesOrder.Domain.Entities.SalesOrder;
using SalesOrder.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Infrastructure.Repository.SalesOrder
{
    public interface ISoOrderRepository : IBaseRepository<SoItem>
    {
    }

    public class SoOrderRepository : BaseRepository<SoItem>, ISoOrderRepository
    {
        public SoOrderRepository(SalesOrderContext context) : base(context) { }
    }
}
