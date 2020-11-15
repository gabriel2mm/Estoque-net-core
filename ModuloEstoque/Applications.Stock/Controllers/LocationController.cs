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
    public class LocationController : ControllerBase
    {
        private readonly IRepository<Location> _repository;
        public LocationController(IRepository<Location> repository)
        {
            _repository = repository;
        }

        // GET: api/locations
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/locations/5
        [HttpGet("{id}")]
        public Location Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/locations
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            _repository.Add(location);
            _repository.SaveAll();
        }

        // PUT api/locations/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Location value)
        {
            Location location = _repository.Find(id);
            if (location != null)
            {
                location.Copy(value);
                _repository.Update(location);
                _repository.SaveAll();
            }
        }

        // DELETE api/locations/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
