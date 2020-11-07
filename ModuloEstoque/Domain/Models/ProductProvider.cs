using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Models
{
    public class ProductProvider
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
