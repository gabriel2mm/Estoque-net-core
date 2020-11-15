using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ShowcaseController
    {

        private readonly IRepository<Showcase> _repository;
        public ShowcaseController(IRepository<Showcase> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("search/{q}")]
        public IEnumerable<Showcase> Search(String q)
        {
            return _repository.Get(s => s.ProductManagement.Product.Name.ToLower().Contains(q.ToLower()) || s.Description.ToLower().Contains(q.ToLower())).ToList();
        }

        // GET: api/Showcases
        [HttpGet]
        public IEnumerable<Showcase> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/Showcases/5
        [HttpGet("{id}")]
        public Showcase Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/Showcases
        [HttpPost]
        public void Post([FromBody] Showcase showcase)
        {
            _repository.Add(showcase);
            _repository.SaveAll();
        }

        // PUT api/Showcases/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Showcase value)
        {
            Showcase showcase = _repository.Find(id);
            if (showcase != null)
            {
                showcase.Copy(value);
                _repository.Update(showcase);
                _repository.SaveAll();
            }
        }

        // DELETE api/Showcases/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }

        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        [HttpPost]
        [Route("upload")]
        public async Task<String> UploadFile([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return "{'error' : 'Não foi possível receber arquivo'}";
            }

            string[] name = file.FileName.Split(".");
            string filename = name[0] + DateTime.Now.Ticks.ToString() + "." + name[1];

            using (AmazonS3Client client = new AmazonS3Client("AKIAJHVXNBETAHQFW2TA", "dWRoeaVJGETwohUsMdPY6SHFlwwbz9D2BcDZIbp1", RegionEndpoint.SAEast1))
            {
                using MemoryStream newMemoryStream = new MemoryStream();
                file.CopyTo(newMemoryStream);
                TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = filename,
                    BucketName = "imagensshowcase/imagens",
                    CannedACL = S3CannedACL.PublicRead,
                };

                TransferUtility fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }
            return "{'status': 200}";

        }
    }
}
