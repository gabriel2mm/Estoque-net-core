using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class InvoiceMockRepository : MockRepository<Invoice>
    {
        protected new readonly ICollection<Invoice> context;


        public InvoiceMockRepository()
        {
            context = new List<Invoice>()
            {
                new Invoice(){
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Description = "Descripiton",
                    Emission = DateTime.Now,
                    IdentificationNumber = "2132465465465656",
                    InvoiceType = Domain.Enumerators.InvoiceType.COMPRA,
                    Name = "Nota fiscal 1",
                    Transition = DateTime.Now
                }
            };
        }

        public override IQueryable<Invoice> GetAll()
        {
            return context.AsQueryable();
        }

        public override Invoice Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(Invoice obj)
        {
            context.Add(obj);
        }

        public override void Update(Invoice obj)
        {
            context.Remove(obj);
            obj.Name = "Teste 1";
            context.Add(obj);
        }

        public override void Delete(Func<Invoice, bool> predicate)
        {
            Invoice invoice = context.Where(predicate).FirstOrDefault();
            context.Remove(invoice);
        }
    }
}
