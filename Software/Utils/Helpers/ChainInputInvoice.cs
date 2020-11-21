using Stock.Domain.Enumerators;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Utils.Helpers
{
    public class ChainInputInvoice : HandlerInvoiceBase
    {
        public override Invoice Handle(TransitionType type)
        {
            if (type == TransitionType.INPUT)
            {
                Random random = new Random();
                int r = random.Next(10000, 99999);

                Invoice invoice = new Invoice()
                {
                    Transition = DateTime.Now,
                    InvoiceType = InvoiceType.COMPRA,
                    Name = "Nota fiscal Entrada",
                    IdentificationNumber = String.Format("{0}{1}", DateTime.Now.Ticks.ToString(), r.ToString()),
                    Description = "Descrição de entrada",
                    Emission = DateTime.Now,
                };

                return invoice;
            }

            return base.Handle(type);
        }

    }
}
