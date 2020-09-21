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
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _repository;
        public ProductController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        // GET: api/products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/products
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _repository.Add(product);
            _repository.SaveAll();
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Product value)
        {
            Product product = _repository.Find(id);
            if (product != null)
            {
                product.Copy(value);
                _repository.Update(product);
                _repository.SaveAll();
            }
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
