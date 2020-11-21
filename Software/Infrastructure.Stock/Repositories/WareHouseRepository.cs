using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class WareHouseRepository : Repository<Warehouse>
    {
        protected new readonly EfContext _context;
        public WareHouseRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Warehouse> GetAll()
        {
            return base.GetAll().Include(w => w.Location).Include(w => w.BranchOffice).Include(w => w.ProductManagements);
        }

        public override Warehouse Find(params object[] key)
        {
            Warehouse warehouse = base.Find(key);
            if (warehouse != null)
            {
                _context.Entry(warehouse).Collection<ProductManagement>(w => w.ProductManagements).Load();
                _context.Entry(warehouse).Reference<BranchOffice>(w => w.BranchOffice).Load();
                _context.Entry(warehouse).Reference<Location>(w => w.Location).Load();
            }
            return warehouse;
        }

        public override void Update(Warehouse obj)
        {
            _context.Entry(obj.BranchOffice).State = EntityState.Modified;
            _context.Entry(obj.Location).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
