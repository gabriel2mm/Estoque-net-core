using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly EfContext _context;
        private readonly IRepository<Reservation> _repository;
        public ReservationController(IRepository<Reservation> repository)
        {
            EfContextFactory factory = new EfContextFactory();
            _context = factory.CreateDbContext(null);
            _repository = repository;
        }

        [HttpPost]
        public string Reserve([FromBody] dynamic json)
        {
            Guid id = json.id;
            int qtd = json.quantidade;

            if (id != null && qtd > 0 )
            {
               ProductManagement productManagement = _context.Set<ProductManagement>().Find(id);
                if (productManagement!= null && productManagement.Amount >= qtd)
                {
                    productManagement.Amount = productManagement.Amount - qtd;
                }

                _context.Set<ProductManagement>().Update(productManagement);
                _context.SaveChanges();

               Reservation reservation = new Reservation()
                {
                    Id = new Guid(),
                    ProductManagement = productManagement,
                    Quantity = qtd,
                    Status = Domain.Enumerators.Status.RESERVADO
                };

                _repository.Add(reservation);
                _repository.SaveAll();

                return reservation.Id.ToString();
            }

            return "";
        }

        [HttpDelete]
        public void CancelReserve([FromBody] dynamic json)
        {
            Guid id = json.id;
            Reservation reservation = _repository.Get(r => r.ProductManagement.Id.Equals(id)).FirstOrDefault();
            if (reservation != null )
            {
                reservation.Status = Domain.Enumerators.Status.CANCELADO;
                reservation.ProductManagement.Amount += reservation.Quantity;

                _repository.Update(reservation);
                _repository.SaveAll();
            }
        }
    }
}
