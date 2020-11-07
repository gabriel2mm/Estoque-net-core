using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class ProductProviderRepository : Repository<ProductProvider>
    {
        protected new readonly EfContext _context;

        public ProductProviderRepository(EfContext context) : base(context)
        {
            _context = context;
        }
        public override IQueryable<ProductProvider> GetAll()
        {
            return base.GetAll().Include(p => p.Product).Include(p => p.Provider);
        }

        public override ProductProvider Find(params object[] key)
        {
            ProductProvider productProvider = base.Find(key);
            if (productProvider != null)
            {
                _context.Entry(productProvider).Reference(p => p.Product).Load();
                _context.Entry(productProvider).Reference(p => p.Provider).Load();
            }
            return productProvider;
        }

        public override void Update(ProductProvider obj)
        {
            _context.Entry(obj.Product).State = EntityState.Modified;
            _context.Entry(obj.Provider).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }


    }
}
