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

        public override void Update(Product obj)
        {
            _context.Entry(obj.Providers).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
