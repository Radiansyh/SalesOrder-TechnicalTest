using SalesOrder.Domain.Entities.SalesOrder;
using SalesOrder.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Infrastructure.Repository.SalesOrder
{
    public interface ISoItemRepository : IBaseRepository<SoItem>
    {
    }

    public class SoItemRepository : BaseRepository<SoItem>, ISoItemRepository
    {
        public SoItemRepository(SalesOrderContext context) : base(context) { }
    }
}
