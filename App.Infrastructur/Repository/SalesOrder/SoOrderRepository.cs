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
    public interface ISoOrderRepository : IBaseRepository<SoOrder>
    {
        Task<IEnumerable<SoOrder>> GetAllWithCustomerAsync();
        Task DeleteOrderWithItemsAsync(long orderId);
        Task<SoOrder> GetByOrderIdAsync(long orderId);
    }

    public class SoOrderRepository : BaseRepository<SoOrder>, ISoOrderRepository
    {
        private readonly SalesOrderContext _context;
        public SoOrderRepository(SalesOrderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SoOrder>> GetAllWithCustomerAsync()
        {
            return await _context.SoOrders
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public async Task DeleteOrderWithItemsAsync(long orderId)
        {
            var items = await _context.SoItems
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            _context.SoItems.RemoveRange(items);

            var order = await _context.SoOrders.FindAsync(orderId);
            if (order != null)
            {
                _context.SoOrders.Remove(order);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<SoOrder> GetByOrderIdAsync(long id)
        {
            return await _context.SoOrders.FirstOrDefaultAsync(x => x.OrderId == id);
        }
    }
}
