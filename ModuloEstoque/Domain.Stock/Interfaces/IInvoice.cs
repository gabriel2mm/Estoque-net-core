using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Interfaces
{
    public interface IInvoice
    {
        Invoice CreateInvoice();
        /// <summary>
        /// Exemplo get values into Invoice
        /// </summary>
        /// <returns>String data of Invoice</returns>
        String ViewInvoice();
    }
}
