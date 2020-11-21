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
    public class CampaignController : ControllerBase
    {
        private readonly IRepository<Campaign> _repository;
        public CampaignController(IRepository<Campaign> repository)
        {
            _repository = repository;
        }

        // GET: api/campaigns
        [HttpGet]
        public IEnumerable<Campaign> Get()
        {
            DateTime currentDate = DateTime.Now;
            ICollection<Campaign> collections = _repository.GetAll().ToList();
            return _repository.GetAll().Where(c => currentDate >= c.StartDate && currentDate <= c.EndDate).ToList();
        }

        // GET api/campaigns/5
        [HttpGet("{id}")]
        public Campaign Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/campaigns
        [HttpPost]
        public void Post([FromBody] Campaign campaign)
        {
            _repository.Add(campaign);
            _repository.SaveAll();
        }

        // PUT api/campaigns/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Campaign value)
        {
            Campaign campaign = _repository.Find(id);
            if (campaign != null)
            {
                campaign.Copy(value);
                _repository.Update(campaign);
                _repository.SaveAll();
            }
        }

        // DELETE api/campaigns/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
