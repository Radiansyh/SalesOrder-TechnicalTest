using SalesOrder.Domain.Entities.SalesOrder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Web.Models
{
    public class SoOrderModel
    {
        public IEnumerable<SoOrder> ListOrders { get; set; }
        public SoOrder Order { get; set; }
    }
}
