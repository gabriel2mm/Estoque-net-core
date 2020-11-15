using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Fornecedor")]
    public class Provider
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Nome")]
        [Required]
        public String Name { get; set; }

        [Column("Produto")]
        [Required]
        public ICollection<ProductProvider> Products { get; set; }

        public Provider Clone()
        {
            return this.MemberwiseClone() as Provider;
        }

        public void Copy(Provider provider)
        {
            this.Name = provider.Name;
            this.Products = provider.Products;
        }
    }
}
