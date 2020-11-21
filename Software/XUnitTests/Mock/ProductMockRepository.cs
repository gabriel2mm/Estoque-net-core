using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class ProductMockRepository : MockRepository<Product>
    {
        protected new readonly ICollection<Product> context;

        public ProductMockRepository()
        {
            context = new List<Product>()
            {
                new Product(){ 
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), 
                    Input = DateTime.Now,
                    Measure = "10",
                    Unit = "kg",
                    Name = "produto 1",
                    Output = DateTime.Now,
                    Price = 10,
                    ProductCode = "101A",
                    Providers = null
                }
            };
        }

        public override IQueryable<Product> GetAll()
        {
            return context.AsQueryable();
        }

        public override Product Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(Product obj)
        {
            context.Add(obj);
        }

        public override void Update(Product obj)
        {
            context.Remove(obj);
            obj.Name = "Teste 1";
            context.Add(obj);
        }

        public override void Delete(Func<Product, bool> predicate)
        {
            Product product = context.Where(predicate).FirstOrDefault();
            context.Remove(product);
        }
    }
}
