using System;
using Stock.Domain.Interfaces;

namespace Stock.Domain.Models.DTO
{
    public class EntryInvoice : Invoice, IInvoice
    {
        public Invoice CreateInvoice()
        {
            return new Invoice()
            {
                Description = "Nota de Entrada emitida",
                Emission = DateTime.Now,
                IdentificationNumber = base.IdentificationNumber,
                InvoiceType = Stock.Domain.Enumerators.InvoiceType.COMPRA,
                Name = base.Name,
                ProductTransitions = base.ProductTransitions,
                Transition = base.Transition
            };
        }

        public string ViewInvoice()
        {
            return String.Format("Nota de entrada criada");
        }
    }
}
