using Stock.Domain.Enumerators;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Utils.Helpers
{
    public class ChainOutputInvoice : HandlerInvoiceBase
    {
        public override Invoice Handle(TransitionType type)
        {
            if (type == TransitionType.OUTPUT)
            {
                Random random = new Random();
                int r = random.Next(1000000000, 999999999);

                Invoice invoice = new Invoice()
                {
                    Transition = DateTime.Now,
                    InvoiceType = InvoiceType.VENDA,
                    Name = "Nota fiscal venda",
                    IdentificationNumber = String.Format("{0}{1}", DateTime.Now.Ticks.ToString(), r.ToString()),
                    Description = "Descrição de Saida",
                    Emission = DateTime.Now,                   
               };

                return invoice;
            }

            return base.Handle(type);
        }
    }
}
