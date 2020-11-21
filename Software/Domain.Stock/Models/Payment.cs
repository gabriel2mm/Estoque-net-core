using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Pagamentos")]
    public class PaymentModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Payment { get; set; }
        public string Card { get; set; }
        public String DueDate { get; set; }
        public String CCV { get; set; }
        public String Plots { get; set; }
        public String BilletNumber { get; set; }
    }
}
