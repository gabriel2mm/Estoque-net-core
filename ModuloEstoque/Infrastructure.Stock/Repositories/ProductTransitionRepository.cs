using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class ProductTransitionRepository : Repository<ProductTransition>
    {
        protected new readonly EfContext _context;
        public ProductTransitionRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<ProductTransition> GetAll()
        {
            return base.GetAll()
                .Include(p => p.Product)
                    .ThenInclude(p => p.Providers)
                .Include(p => p.Warehouse)
                    .ThenInclude(w => w.Location)
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.ProductTransitions);
        }

        public override ProductTransition Find(params object[] key)
        {
            ProductTransition productTransition = base.Find(key);
            if (productTransition != null)
            {
                _context.Entry<ProductTransition>(productTransition).Reference<Product>(p => p.Product).Load();
                _context.Entry<ProductTransition>(productTransition).Reference<Invoice>(p => p.Invoice).Load();
                _context.Entry<ProductTransition>(productTransition).Reference<Warehouse>(p => p.Warehouse).Load();
            }

            return productTransition;
        }
        public override void Add(ProductTransition obj)
        {
            _context.Attach(obj.Warehouse);
            _context.Attach(obj.Product);
            _context.Set<ProductTransition>().Add(obj);
        }
        public override void SaveAll()
        {
            _context.SaveChanges();
        }

        public override void Update(ProductTransition obj)
        {
            _context.Entry(obj.Warehouse).State = EntityState.Unchanged;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
