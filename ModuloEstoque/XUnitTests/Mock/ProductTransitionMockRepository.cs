using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class ProductTransitionMockRepository : MockRepository<ProductTransition>
    {
        protected new readonly ICollection<ProductTransition> context;


        public ProductTransitionMockRepository()
        {
            context = new List<ProductTransition>()
            {
                new ProductTransition(){
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Invoice = new Invoice(),
                    Product = new Product(),
                    Quantity = 10,
                    Transition = DateTime.Now,
                    TransitionType = Domain.Enumerators.TransitionType.INPUT,
                    UnitCost = 10,
                    Warehouse = new Warehouse()
                }
            };
        }

        public override IQueryable<ProductTransition> GetAll()
        {
            return context.AsQueryable();
        }

        public override ProductTransition Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(ProductTransition obj)
        {
            context.Add(obj);
        }

        public override void Update(ProductTransition obj)
        {
            context.Remove(obj);
            obj.Quantity = 9999;
            context.Add(obj);
        }

        public override void Delete(Func<ProductTransition, bool> predicate)
        {
            ProductTransition productTransition = context.Where(predicate).FirstOrDefault();
            context.Remove(productTransition);
        }
    }
}
