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
    public class BranchOfficeController : ControllerBase
    {
        private readonly IRepository<BranchOffice> _repository;
        public BranchOfficeController(IRepository<BranchOffice> repository)
        {
            _repository = repository;
        }

        // GET: api/branchOffices
        [HttpGet]
        public IEnumerable<BranchOffice> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/branchOffices/5
        [HttpGet("{id}")]
        public BranchOffice Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/branchOffices
        [HttpPost]
        public void Post([FromBody] BranchOffice branchOffice)
        {
            _repository.Add(branchOffice);
            _repository.SaveAll();
        }

        // PUT api/branchOffices/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] BranchOffice value)
        {
            BranchOffice branchOffice = _repository.Find(id);
            if (branchOffice != null)
            {
                branchOffice.Copy(value);
                _repository.Update(branchOffice);
                _repository.SaveAll();
            }
        }

        // DELETE api/branchOffices/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
