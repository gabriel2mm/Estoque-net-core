using Stock.Domain.Enumerators;
using Stock.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Models
{
    public class Bill : IDocument
    {
        public Guid Id { get; set; }
        public Int32 Number { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime DueDate { get; set; }
        public double Value { get; set; }
        public double Interest { get; set; }
        public double Fine { get; set; }
        public double Discount { get; set; }
        public BillStatus Status { get; set; }
    }
}
