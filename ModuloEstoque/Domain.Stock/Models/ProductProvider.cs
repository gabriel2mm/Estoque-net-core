using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("ProdutoFornecedor")]
    public class ProductProvider
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
