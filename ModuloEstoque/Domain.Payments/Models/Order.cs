using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Pedido")]
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Column("codPedido")]
        public String OrderCode { get; set; }
        [Required]
        [Column("nome")]
        public String Name { get; set; }
        [Required]
        [Column("email")]
        public String Email {get;set;}
        [Required]
        [Column("CPF")]
        public String CPF { get; set; }
        [Required]
        [Column("local")]
        public Location Location { get; set; }
        [Required]
        [Column("pagamento")]
        public PaymentModel Payment { get; set; }
        [NotMapped]
        public ICollection<Showcase> Products { get; set; }
        [Column("PedidosVitrines")]
        public ICollection<OrdersShowcases> OrdersShowcases { get; set; }
       
        public Order Clone()
        {
            return this.MemberwiseClone() as Order;
        }

        public void Copy(Order order)
        {
            this.Name = order.Name;
            this.OrderCode = order.OrderCode;
            this.Email = order.Email;
            this.CPF = order.CPF;
            this.Location = order.Location;
            this.Products = order.Products;
            this.Payment = order.Payment;
        }
    }
}
