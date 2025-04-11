using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Domain.Entities.SalesOrder
{
    [Table("SO_ORDER")]
    public class SoOrder
    {
        [Key]
        [Column("SO_ORDER_ID")]
        public int Order_id { get; set; }
        [Column("ORDER_NO")]
        public string OrderNumber { get; set; }
        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }
        [Column("COM_CUSTOMER_ID")]
        public int CustomerId { get; set; }
        public string Address { get; set; }
    }
}
