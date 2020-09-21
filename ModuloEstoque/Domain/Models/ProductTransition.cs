using Stock.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("TransacaoProdutos")]
    public class ProductTransition
    {
        [Key]
        public Guid Id { get; set; }

        [Column("DataDaTransicao")]
        public DateTime Transition { get; set; }

        [Column("TipoTransacao")]
        [DefaultValue(TransitionType.INPUT)]
        public TransitionType TransitionType { get; set; }

        [Range(0, 9999999)]
        [Column("CustoUnitario")]
        [DefaultValue(0)]
        public double UnitCost { get; set; }

        [Column("Quantidade")]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Column("Produto")]
        [Required]
        public Product Product { get; set; }

        [Column("Deposito")]
        [Required]
        public Warehouse Warehouse { get; set; }

        [Column("NotaFiscal")]
        public Invoice Invoice { get; set; }

        public ProductTransition Clone()
        {
            return this.MemberwiseClone() as ProductTransition;
        }

        public void Copy(ProductTransition productTransition)
        {
            this.Warehouse = productTransition.Warehouse;
            this.Invoice = productTransition.Invoice;
            this.Product = productTransition.Product;
            this.Quantity = productTransition.Quantity;
            this.Transition = productTransition.Transition;
            this.UnitCost = productTransition.UnitCost;
        }

    }
}
