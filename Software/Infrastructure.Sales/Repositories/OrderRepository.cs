using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        protected new readonly EfContext _context;
        public OrderRepository(EfContext context): base(context)
        {
            _context = context;
        }

        public override IQueryable<Order> GetAll()
        {
            return base.GetAll().Include(o => o.Products).Include(o=> o.OrdersShowcases).Include(o => o.Location).Include(o => o.Payment);
        }

        public override void Add(Order obj)
        {
            _context.Attach(obj.Location);
            _context.Attach(obj.Payment);
            _context.Set<Order>().Add(obj);
        }

        public override void SaveAll()
        {
            _context.SaveChanges();
        }

        public override Order Find(params object[] key)
        {
            Order order = base.Find(key);
            if (order != null)
            {
                _context.Entry(order).Collection(o => o.OrdersShowcases).Load();
                _context.Entry(order).Reference(o => o.Payment).Load();
                _context.Entry(order).Reference(o => o.Location).Load();
                _context.Entry(order).Collection(o => o.Products).Load();
            }
            return order;
        }

        public override void Update(Order obj)
        {
            _context.Entry(obj.OrdersShowcases).State = EntityState.Modified;
            _context.Entry(obj.Location).State = EntityState.Modified;
            _context.Entry(obj.Payment).State = EntityState.Modified;
            _context.Entry(obj.Products).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }

    }
}
