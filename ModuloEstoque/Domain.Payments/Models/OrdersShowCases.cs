using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("PedidosVitrine")]
    public class OrdersShowcases
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ShowcaseId { get; set; }
        public Showcase ShowCase { get; set; }
    }
}
