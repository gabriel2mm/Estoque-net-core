using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Interfaces
{
    public interface IInvoice
    {
        Invoice CreateInvoice();
        String ViewInvoice();
    }
}
