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
        [Column("Name")]
        [Required]
        public String Name { get; set; }

        public Provider Clone()
        {
            return this.MemberwiseClone() as Provider;
        }

        public void Copy(Provider provider)
        {
            this.Name = provider.Name;
        }
    }
}
