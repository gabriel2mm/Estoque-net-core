using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class ShowcaseRepository : Repository<Showcase>
    {
        protected new readonly EfContext _context;
        public ShowcaseRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Showcase> GetAll()
        {
            return base.GetAll().Include(s => s.ProductManagement).ThenInclude(p => p.Product);
        }

        public override Showcase Find(params object[] key)
        {
            Showcase showcase = base.Find(key);
            if (showcase != null)
            {
                _context.Entry(showcase).Reference(s=> s.ProductManagement).Load();
            }
            return showcase;
        }

        public override void Update(Showcase obj)
        {
            _context.Entry(obj.ProductManagement).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
