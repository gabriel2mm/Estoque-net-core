using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("ContasAPagar")]
    public class BillsToPay : Bill
    {
        [Key]
        public new Guid Id { get; set; }
        public Int32 BeneficiaryNumber { get; set; }
        public String Beneficiary { get; set; }

        [Column("TituloSubstituicao")]
        public BillsToPay NewTitle { get; set; }

        public ICollection<BillTransaction> BillTransactions { get; set; }
    }
}
