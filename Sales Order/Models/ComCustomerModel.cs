using SalesOrder.Domain.Entities.Common;

namespace SalesOrder.Web.Models
{
    public class ComCustomerModel
    {
        public IEnumerable<ComCustomer> ListCustomers { get; set; }
        public ComCustomer Customer { get; set; }
    }
}
