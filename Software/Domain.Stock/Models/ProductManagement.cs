using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("ProdutoControle")]
    public class ProductManagement
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Column("CustoMedio")]
        public double AverageCost { get; set; }

        [Range(0, 9999999)]
        [Column("Custo total")]
        [DefaultValue(0)]
        public double TotalCost { get; set; }

        [Range(0, 9999999)]
        [Column("Quantidade")]
        [DefaultValue(0)]
        public int Amount { get; set; }

        [ForeignKey("WareHouseProductControlId")]
        public Warehouse Warehouse { get; set; }

        public ProductManagement Clone()
        {
            return this.MemberwiseClone() as ProductManagement;
        }
        public void Copy(ProductManagement productControl)
        {
            this.Amount = productControl.Amount;
            this.AverageCost = productControl.AverageCost;
            this.Product = productControl.Product;
            this.Warehouse = productControl.Warehouse;
            this.TotalCost = productControl.TotalCost;
        }
    }
}
