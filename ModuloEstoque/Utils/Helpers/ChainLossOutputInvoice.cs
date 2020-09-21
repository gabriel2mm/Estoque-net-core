using Stock.Domain.Enumerators;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Utils.Helpers
{
    public class ChainLossOutputInvoice : HandlerInvoiceBase
    {
        public override Invoice Handle(TransitionType type)
        {
            if (type == TransitionType.LOSS_OUTPUT)
            {
                Random random = new Random();
                int r = random.Next(1000000000, 999999999);

                Invoice invoice = new Invoice()
                {
                    Transition = DateTime.Now,
                    InvoiceType = InvoiceType.SAIDA_PERDA,
                    Name = "Nota fiscal perda",
                    IdentificationNumber = String.Format("{0}{1}", DateTime.Now.Ticks.ToString(), r.ToString()),
                    Description = "Descrição saída de perda",
                    Emission = DateTime.Now,                   
               };

                return invoice;
            }

            return base.Handle(type);
        }
    }
}
