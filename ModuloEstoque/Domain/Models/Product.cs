

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain.Models
{
    [Table("Produto")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Column("CodProduto")]
        public String ProductCode { get; set; }
        [Column("Nome")]
        [Required]
        public String Name { get; set; }
        [Column("Unidade")]
        [Required]
        public String Unit { get; set; }
        [Column("Medida")]
        [Required]
        public String Measure { get; set; }
        [Column("Fornecedor")]
        [Required]
        public ICollection<ProductProvider> Providers { get; set; }
        [Column("Entrada")]
        public DateTime Input { get; set; }
        [Column("Saida")]
        public DateTime Output { get; set; }
        [Range(0, 9999999)]
        [Column("Preço")]
        public double Price { get; set; }

        public Product Clone()
        {
            return this.MemberwiseClone() as Product;
        }

        public void Copy(Product product)
        {
            this.ProductCode = product.ProductCode;
            this.Input = product.Input;
            this.Measure = product.Measure;
            this.Name = product.Name;
            this.Output = product.Output;
            this.Price = product.Price;
            this.Providers = product.Providers;
            this.Unit = product.Unit;
        }
    }
}
