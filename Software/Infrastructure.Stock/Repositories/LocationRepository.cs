using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class LocationRepository : Repository<Location>
    {
        protected new readonly EfContext _context;
        public LocationRepository(EfContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(Location obj)
        {
            _context.Entry(obj.Warehouses).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }

        public override IQueryable<Location> GetAll()
        {
            return base.GetAll().Include(l => l.Warehouses);
        }

        public override Location Find(params object[] key)
        {
            Location location = base.Find(key);
            if (location != null)
            {
                _context.Entry(location).Collection<Warehouse>(l => l.Warehouses).Load();
            }

            return location;
        }
    }
}
