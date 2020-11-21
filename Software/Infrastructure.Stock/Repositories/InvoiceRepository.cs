using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class InvoiceRepository : Repository<Invoice>
    {
        protected new readonly EfContext _context;
        public InvoiceRepository(EfContext context): base(context)
        {
            _context = context;
        }

        public override IQueryable<Invoice> GetAll()
        {
            return base.GetAll().Include(i => i.ProductTransitions);
        }

        public override Invoice Find(params object[] key)
        {
            Invoice invoice = base.Find(key);
            if (invoice != null)
            {
                _context.Entry(invoice).Collection(i => i.ProductTransitions).Load();
            }
            return invoice;
        }

        public override void Update(Invoice obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

    }
}
