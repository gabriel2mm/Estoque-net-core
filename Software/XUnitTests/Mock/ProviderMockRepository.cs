using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class ProviderMockRepository : MockRepository<Provider>
    {
        protected new readonly ICollection<Provider> context;


        public ProviderMockRepository()
        {
            context = new List<Provider>()
            {
                new Provider(){ Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), Name = "Teste"}
            };
        }

        public override IQueryable<Provider> GetAll()
        {
            return context.AsQueryable();
        }

        public override Provider Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(Provider obj)
        {
            context.Add(obj);
        }

        public override void Update(Provider obj)
        {
            context.Remove(obj);
            obj.Name = "Teste 1";
            context.Add(obj);
        }

        public override void Delete(Func<Provider, bool> predicate)
        {
            Provider provider = context.Where(predicate).FirstOrDefault();
            context.Remove(provider);
        }
    }
}
