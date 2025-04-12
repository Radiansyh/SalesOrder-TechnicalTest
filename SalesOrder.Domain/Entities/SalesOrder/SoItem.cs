﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Domain.Entities.SalesOrder
{
    [Table("SO_ITEM")]
    public class SoItem
    {
        [Key]
        [Column("SO_ITEM_ID")]
        public long ItemId { get; set; }
        [Column("SO_ORDER_ID")]
        public long OrderId { get; set; }
        [Column("ITEM_NAME")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
