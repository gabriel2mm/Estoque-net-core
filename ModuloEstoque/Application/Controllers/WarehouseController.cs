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
    public class WarehouseController : ControllerBase
    {
        private readonly IRepository<Warehouse> _repository;
        public WarehouseController(IRepository<Warehouse> repository)
        {
            _repository = repository;
        }

        // GET: api/warehouses
        [HttpGet]
        public IEnumerable<Warehouse> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/warehouses/5
        [HttpGet("{id}")]
        public Warehouse Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/warehouses
        [HttpPost]
        public void Post([FromBody] Warehouse warehouse)
        {
            _repository.Add(warehouse);
            _repository.SaveAll();
        }

        // PUT api/warehouses/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Warehouse value)
        {
            Warehouse warehouse = _repository.Find(id);
            if (warehouse != null)
            {
                warehouse.Copy(value);
                _repository.Update(warehouse);
                _repository.SaveAll();
            }
        }

        // DELETE api/warehouses/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
