using Stock.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("TransaçãoContas")]
    public class BillTransaction
    {
        [Key]
        public Guid Id { get; set; }
        [Column("DataDaTransacao")]
        public DateTime TransactionDate { get; set; }

        [Column("tipoOperacao")]
        public OperationType TransactionOperation { get; set; }

        [Column("TipoTransacao")]
        public TransactionType TransactionType { get; set; }
        [Column("codTransacao")]
        public string TransactionCode { get; set; }
        [Column("Valor")]
        public double Value { get; set; }
        [Column("Juros")]
        public double Interest { get; set; }
        [Column("Multa")]
        public double Fine { get; set; }
        [Column("Disconto")]
        public double Discount { get; set; }
        public BillsToReceive BillsToReceive { get; set; }

        public BillsToPay BillsToPay { get; set; }
    }
}
