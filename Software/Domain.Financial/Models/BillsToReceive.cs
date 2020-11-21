using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("ContasAReceber")]
    public class BillsToReceive : Bill
    {
        [Key]
        public new Guid Id { get; set; }
        [Column("NumeroSacado")]
        public Int32 PayerNumber { get; set; }
        [Column("Sacado")]
        public String Payer { get; set; }
        public ICollection<BillTransaction> BillTransactions { get; set; }
        public BillsToReceive NewTitle { get; set; }
    }
}
