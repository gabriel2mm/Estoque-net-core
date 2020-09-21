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
    public class ProductTransitionController : ControllerBase
    {
        private readonly IRepository<ProductTransition> _repository;
        public ProductTransitionController(IRepository<ProductTransition> repository)
        {
            _repository = repository;
        }

        // GET: api/productTransitions
        [HttpGet]
        public IEnumerable<ProductTransition> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/productTransitions/5
        [HttpGet("{id}")]
        public ProductTransition Get(Guid id)
        {
            return _repository.Find(id);
        }

        // DELETE api/productTransitions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
