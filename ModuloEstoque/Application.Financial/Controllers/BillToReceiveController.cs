using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stock.Domain.Enumerators;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using System;

namespace Application.Financial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BillsToReceiveController : ControllerBase
    {
        private readonly IRepository<BillTransaction> _billTransactionRepository;
        private readonly IRepository<BillsToReceive> _repository;
        public BillsToReceiveController(IRepository<BillsToReceive> repository, IRepository<BillTransaction> billTransactionRepository)
        {
            _repository = repository;
            _billTransactionRepository = billTransactionRepository;
        }

        [HttpPost]
        public void CreateBillsToReceive(BillsToReceive billToReceive)
        {
            _repository.Add(billToReceive);
            _repository.SaveAll();

            this.CreateTransaction(billToReceive, OperationType.CRIACAO, TransactionType.Entrada);
        }

        [HttpPut("cancel/{id}")]
        public void CancelBillsToReceive(Guid Id)
        {
            BillsToReceive billToReceive = _repository.Find(Id);
            if (billToReceive != null)
            {
                billToReceive.Status = Stock.Domain.Enumerators.BillStatus.Cancelado;
                _repository.Update(billToReceive);
                _repository.SaveAll();

                this.CreateTransaction(billToReceive, OperationType.CANCELAMENTO, TransactionType.Entrada);
            }
        }
        [HttpPut("pay/totalValue/{id}")]
        public void PayTotalValueBillsToReceive(Guid Id)
        {
            BillsToReceive billToReceive = _repository.Find(Id);
            if (billToReceive != null)
            {
                billToReceive.Status = Stock.Domain.Enumerators.BillStatus.Pago;
                _repository.Add(billToReceive);
                _repository.SaveAll();

                this.CreateTransaction(billToReceive, OperationType.PAGAMENTO, TransactionType.Entrada);
            }
        }

        [HttpPut("pay/parcialValue/{id}")]
        public void PayParcialValueBillsToReceive(Guid Id, [FromBody] JObject obj)
        {
            BillsToReceive billToReceive = _repository.Find(Id);
            if (billToReceive != null)
            {
                double value = obj.Value<double>("value");

                BillsToReceive newBill = new BillsToReceive()
                {
                    Id = new Guid(),
                    Value = billToReceive.Value - value,
                    Payer = billToReceive.Payer,
                    DueDate = billToReceive.DueDate.AddDays(30),
                    Status = Stock.Domain.Enumerators.BillStatus.Aberto,
                    Invoice = billToReceive.Invoice,
                    Number = new int(),
                    PayerNumber = billToReceive.PayerNumber,
                    Discount = billToReceive.Discount,
                    Fine = billToReceive.Fine,
                    Interest = billToReceive.Interest,
                };

                billToReceive.Status = Stock.Domain.Enumerators.BillStatus.Pago;
                billToReceive.NewTitle = newBill;
                _repository.Add(newBill);
                _repository.Update(billToReceive);
                _repository.SaveAll();

                this.CreateTransaction(newBill, OperationType.CRIACAO, TransactionType.Entrada);
                this.CreateTransaction(billToReceive, OperationType.PAGAMENTO, TransactionType.Entrada);
            }
        }

        [HttpPut("pay/creditCard/{id}")]
        public void PayCreditCardBillsToReceive(Guid Id, [FromBody] JObject obj)
        {
            BillsToReceive billToReceive = _repository.Find(Id);
            if (billToReceive != null)
            {
                string beneficiary = obj.Value<String>("beneficiary");
                DateTime dueDate = new DateTime(long.Parse(obj.GetValue("dueDate")?.ToString()));

                BillsToReceive newBill = new BillsToReceive()
                {
                    Id = new Guid(),
                    Value = billToReceive.Value,
                    Payer = beneficiary,
                    DueDate = dueDate,
                    Status = Stock.Domain.Enumerators.BillStatus.Aberto,
                    Invoice = billToReceive.Invoice,
                    Number = new int(),
                    PayerNumber = billToReceive.PayerNumber,
                };

                billToReceive.Status = Stock.Domain.Enumerators.BillStatus.Substituido;
                billToReceive.NewTitle = newBill;
                _repository.Add(newBill);
                _repository.Update(billToReceive);
                _repository.SaveAll();

                this.CreateTransaction(newBill, OperationType.CRIACAO, TransactionType.Entrada);
                this.CreateTransaction(billToReceive, OperationType.SUBSTITUICAO, TransactionType.Entrada);
            }
        }

        [HttpPut("pay/installments/{id}")]
        public void PayInstallmentsBillsToReceive(Guid Id, [FromBody] JObject obj)
        {
            BillsToReceive billToReceive = _repository.Find(Id);
            if (billToReceive != null)
            {
                int installments = obj.Value<int>("installments");

                for(int i = 0; i < installments; i++)
                {
                    BillsToReceive newBill = new BillsToReceive()
                    {
                        Id = new Guid(),
                        Value = billToReceive.Value / installments,
                        Discount = billToReceive.Discount,
                        Fine = billToReceive.Fine,
                        Interest = billToReceive.Interest,
                        Payer = billToReceive.Payer,
                        PayerNumber = billToReceive.PayerNumber,
                        DueDate = DateTime.Now.AddDays((i + 1) * 30),
                        Status = Stock.Domain.Enumerators.BillStatus.Aberto,
                        Invoice = billToReceive.Invoice,
                        Number = new int(),
                    };

                    _repository.Add(newBill);
                    this.CreateTransaction(newBill, OperationType.CRIACAO, TransactionType.Entrada);
                }

                billToReceive.Status = Stock.Domain.Enumerators.BillStatus.Substituido;
                _repository.Update(billToReceive);
                _repository.SaveAll();
                
                this.CreateTransaction(billToReceive, OperationType.SUBSTITUICAO, TransactionType.Entrada);
            }
        }

        [HttpPut("{id}")]
        public void UpdateBillsToReceive(Guid id, [FromBody] BillsToReceive obj)
        {
            BillsToReceive billToReceive = _repository.Find(id);
            if (billToReceive != null)
            {
                obj.Id = billToReceive.Id;
                _repository.Update(obj);
                _repository.SaveAll();
            }

            this.CreateTransaction(billToReceive, OperationType.ATUALIZADO, TransactionType.Entrada);
        }

        private void CreateTransaction(BillsToReceive billToReceive, OperationType operationType, TransactionType transactionType)
        {
            BillTransaction transaction = new BillTransaction()
            {
                BillsToPay = null,
                TransactionDate = DateTime.Now,
                Value = billToReceive.Value,
                TransactionOperation = operationType,
                TransactionType = transactionType,
                Discount = billToReceive.Discount,
                Fine = billToReceive.Fine,
                BillsToReceive = billToReceive,
                Interest = billToReceive.Interest,
                TransactionCode = "tr" + new Random().Next(0, 100).ToString() + DateTime.Now.ToString()
            };

            _billTransactionRepository.Add(transaction);
            _billTransactionRepository.SaveAll();
        }
    }
}
