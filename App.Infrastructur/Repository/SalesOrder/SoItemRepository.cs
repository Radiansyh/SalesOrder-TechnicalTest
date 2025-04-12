using Microsoft.EntityFrameworkCore;
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
        Task<List<SoItem>> GetItemsByOrderIdAsync(long orderId);
    }

    public class SoItemRepository : BaseRepository<SoItem>, ISoItemRepository
    {
        private readonly SalesOrderContext _context;
        public SoItemRepository(SalesOrderContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<SoItem>> GetItemsByOrderIdAsync(long orderId)
        {
            return await _context.SoItems.Where(x => x.OrderId == orderId).ToListAsync();
        }
    }

}
