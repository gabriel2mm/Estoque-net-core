using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Financial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BillTransactionController : ControllerBase
    {
        private readonly IRepository<BillTransaction> _repository;
        public BillTransactionController(IRepository<BillTransaction> repository)
        {
            _repository = repository;
        }

        public void CreateTransaction(BillTransaction billTransaction)
        {
            _repository.Add(billTransaction);
            _repository.SaveAll();
        }
    }
}
