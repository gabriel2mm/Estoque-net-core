using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>
    {
        protected new readonly EfContext _context;
        public ReservationRepository(EfContext context): base(context)
        {
            _context = context;
        }

        public override IQueryable<Reservation> GetAll()
        {
            return base.GetAll().Include(r => r.ProductManagement);
        }
        public override void Add(Reservation obj)
        {
            _context.Attach(obj.ProductManagement);
            _context.Set<Reservation>().Add(obj);
        }

        public override void SaveAll()
        {
            _context.SaveChanges();
        }

        public override Reservation Find(params object[] key)
        {
            Reservation order = base.Find(key);
            if (order != null)
            {
                _context.Entry(order).Reference(o => o.ProductManagement).Load();
            }
            return order;
        }

        public override void Update(Reservation obj)
        {
            _context.Entry(obj.ProductManagement).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }

    }
}
