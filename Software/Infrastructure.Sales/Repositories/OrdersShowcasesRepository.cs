using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class OrdersShowcasesRepository : Repository<OrdersShowcases>
    {
        protected new readonly EfContext _context;

        public OrdersShowcasesRepository(EfContext context) : base(context)
        {
            _context = context;
        }
        public override IQueryable<OrdersShowcases> GetAll()
        {
            return base.GetAll().Include(p => p.Order).Include(p => p.ShowCase);
        }

        public override OrdersShowcases Find(params object[] key)
        {
            OrdersShowcases ordersShowcases = base.Find(key);
            if (ordersShowcases != null)
            {
                _context.Entry(ordersShowcases).Reference(p => p.Order).Load();
                _context.Entry(ordersShowcases).Reference(p => p.ShowCase).Load();
            }
            return ordersShowcases;
        }

        public override void Add(OrdersShowcases obj)
        {
            _context.Attach(obj.Order);
            _context.Attach(obj.ShowCase);

            _context.Set<OrdersShowcases>().Add(obj);
        }

        public override void Update(OrdersShowcases obj)
        {
            _context.Entry(obj.Order).State = EntityState.Modified;
            _context.Entry(obj.ShowCase).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
