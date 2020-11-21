using Stock.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Reserva")]
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }
        public ProductManagement ProductManagement { get; set; }
    }
}
