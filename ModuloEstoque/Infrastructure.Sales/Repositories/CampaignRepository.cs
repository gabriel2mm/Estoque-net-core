using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class CampaignRepository : Repository<Campaign>
    {
        protected new readonly EfContext _context;
        public CampaignRepository(EfContext context): base(context)
        {
            _context = context;
        }

        public override IQueryable<Campaign> GetAll()
        {
            return base.GetAll().Include(c => c.Showcase).ThenInclude(c => c.ProductManagement).ThenInclude(c => c.Product);
        }
        public override void Add(Campaign obj)
        {
            _context.Attach(obj.Showcase);
            _context.Set<Campaign>().Add(obj);
        }

        public override void SaveAll()
        {
            _context.SaveChanges();
        }

        public override Campaign Find(params object[] key)
        {
            Campaign order = base.Find(key);
            if (order != null)
            {
                _context.Entry(order).Reference(o => o.Showcase).Load();
            }
            return order;
        }

        public override void Update(Campaign obj)
        {
            _context.Entry(obj.Showcase).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }

    }
}
