using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Linq;

namespace Stock.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product> 
    {
        protected readonly new EfContext _context;
        public ProductRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Product> GetAll()
        {
            return base.GetAll().Include(p => p.Provider);
        }

        public override Product Find(params object[] key)
        {
            Product product = base.Find(key);
            if (product != null)
            {
                _context.Entry(product).Reference<Provider>(p => p.Provider).Load();
            }
            return product;
        }

        public override void Update(Product obj)
        {
            _context.Entry(obj.Provider).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
