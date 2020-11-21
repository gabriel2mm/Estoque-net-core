using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IRepository<Provider> _repository;
        public ProviderController(IRepository<Provider> repository)
        {
            _repository = repository;
        }

        // GET: api/providers
        [HttpGet]
        public IEnumerable<Provider> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/providers/5
        [HttpGet("{id}")]
        public Provider Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/providers
        [HttpPost]
        public void Post([FromBody] Provider provider)
        {
            _repository.Add(provider);
            _repository.SaveAll();
        }

        // PUT api/providers/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Provider value)
        {
            Provider provider = _repository.Find(id);
            if (provider != null)
            {
                provider.Copy(value);
                _repository.Update(provider);
                _repository.SaveAll();
            }
        }

        // DELETE api/providers/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
