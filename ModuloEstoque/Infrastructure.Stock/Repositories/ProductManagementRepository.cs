using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class ProductManagementRepository : Repository<ProductManagement>
    {
        protected new readonly EfContext _context;
        public ProductManagementRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(ProductManagement obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public override IQueryable<ProductManagement> GetAll()
        {
            return base.GetAll().Include(p => p.Product).Include(p => p.Warehouse);
        }

        public override ProductManagement Find(params object[] key)
        {
            ProductManagement productControl = base.Find(key);

            if (productControl != null)
            {
                _context.Entry(productControl).Reference<Product>(p => p.Product).Load();
                _context.Entry(productControl).Reference<Warehouse>(p => p.Warehouse).Load();
            }

            return productControl;
        }
    }
}
