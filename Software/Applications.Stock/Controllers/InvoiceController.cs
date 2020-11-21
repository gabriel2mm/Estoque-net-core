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
    public class InvoiceController : ControllerBase
    {
        private readonly IRepository<Invoice> _repository;
        public InvoiceController(IRepository<Invoice> repository)
        {
            _repository = repository;
        }

        // GET: api/invoices
        [HttpGet]
        public IEnumerable<Invoice> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/invoices/5
        [HttpGet("{id}")]
        public Invoice Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/invoices
        [HttpPost]
        public void Post([FromBody] Invoice invoice)
        {
            _repository.Add(invoice);
            _repository.SaveAll();
        }

        // PUT api/invoices/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Invoice value)
        {
            Invoice invoice = _repository.Find(id);
            if (invoice != null)
            {
                invoice.Copy(value);
                _repository.Update(invoice);
                _repository.SaveAll();
            }
        }

        // DELETE api/invoices/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
