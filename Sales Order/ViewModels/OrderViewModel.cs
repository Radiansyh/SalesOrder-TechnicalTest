using SalesOrder.Domain.Entities.Common;
using SalesOrder.Domain.Entities.SalesOrder;

namespace SalesOrder.Web.ViewModels
{
    public class OrderViewModel
    {
        public SoOrder Order { get; set; }
        public List<SoItem> Items { get; set; } = new List<SoItem>();
        public List<ComCustomer> Customers { get; set; }
        public bool IsEditMode { get; set; }
    }
}
