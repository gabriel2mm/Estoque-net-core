using Stock.Domain.Enumerators;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain.Models
{
    [Table("NotaFiscal")]
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Nome")]
        public String Name { get; set; }
        [Column("NumeroIdentificacao")]
        public String IdentificationNumber { get; set; }
        [Column("Emissao")]
        public DateTime Emission { get; set; }
        
        [Column("TransacaoProdutos")]
        public ICollection<ProductTransition> ProductTransitions { get; set; }
       
        [Column("Transacao")]
        public DateTime Transition { get; set; }

        [Column("Descricao")]
        public String Description { get; set; }

        [Column("Tipo")]
        public InvoiceType InvoiceType { get; set; }

        public Invoice Clone()
        {
            return this.MemberwiseClone() as Invoice;
        }
        public void Copy(Invoice invoice)
        {
            this.Emission = invoice.Emission;
            this.IdentificationNumber = invoice.IdentificationNumber;
            this.InvoiceType = invoice.InvoiceType;
            this.Name = invoice.Name;
            this.ProductTransitions = invoice.ProductTransitions;
            this.Description = invoice.Description;
            this.Transition = invoice.Transition;
        }
    }
}
