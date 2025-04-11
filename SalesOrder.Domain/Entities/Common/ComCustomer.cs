using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Domain.Entities.Common
{
    [Table("COM_CUSTOMER")]
    public class ComCustomer
    {
        [Key]
        [Column("COM_CUSTOMER_ID")]
        public int CustomerId { get; set; }
        [Column("COSTUMER_NAME")]
        public string CustomerName { get; set; }
    }
}
