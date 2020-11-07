using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductManagement> _productManagementRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Provider> _providerRepository;
        private readonly IRepository<ProductProvider> _productProviderRepository;
        private readonly IRepository<ProductTransition> _productTranstion;
        private readonly IRepository<Showcase> _showcaseRepository;
        public TesteController(IRepository<Location> locationRepository,
        IRepository<Product> productRepository,
        IRepository<ProductManagement> productManagementRepository,
        IRepository<Warehouse> warehouseRepository,
        IRepository<Provider> providerRepository,
        IRepository<ProductProvider> productProviderRepository,
        IRepository<ProductTransition> productTranstion,
        IRepository<Showcase> showcaseRepository)
        {
            _locationRepository = locationRepository;
            _productRepository = productRepository;
            _productManagementRepository = productManagementRepository;
            _warehouseRepository = warehouseRepository;
            _providerRepository = providerRepository;
            _productProviderRepository = productProviderRepository;
            _productTranstion = productTranstion;
            _showcaseRepository = showcaseRepository;
        }

        [HttpGet]
        public void CadastrarExemplos()
        {
            Location location = new Location()
            {
                CEP = "81230170",
                City = "curitiba",
                Complement = "teste",
                District = "Campo comprido",
                Name = "Local 1",
                Number = 3651,
                State = "PR",
                Street = "Renato polatti",
                Warehouses = null
            };

            Location location2 = new Location()
            {
                CEP = "82310260",
                City = "curitiba",
                Complement = "teste",
                District = "orleans",
                Name = "Local 2",
                Number = 461,
                State = "PR",
                Street = "Luiz foggiatto",
                Warehouses = null
            };

            _locationRepository.Add(location);
            _locationRepository.Add(location2);
            _locationRepository.SaveAll();

            Warehouse warehouse = new Warehouse()
            {
                Location = location,
                ThirdParty = true,
                WarehouseCode = "WH01"
            };

            Warehouse warehouse2 = new Warehouse()
            {
                Location = location2,
                ThirdParty = false,
                WarehouseCode = "WH02"
            };

            _warehouseRepository.Add(warehouse);
            _warehouseRepository.Add(warehouse2);
            _warehouseRepository.SaveAll();

            Product product = new Product()
            {
                Input = DateTime.Now,
                Name = "Camiseta Barcelona",
                Price = 199.00,
                ProductCode = "CB1",
                Unit = "UN",
                Measure = "G",
            };

            Product product1 = new Product()
            {
                Input = DateTime.Now,
                Name = "Camiseta Real madrid",
                Price = 199.00,
                ProductCode = "CB1",
                Unit = "UN",
                Measure = "G",
            };

            Product product2 = new Product()
            {
                Input = DateTime.Now,
                Name = "Camiseta Liverpool",
                Price = 199.00,
                ProductCode = "CB1",
                Unit = "UN",
                Measure = "G",
            };

            _productRepository.Add(product);
            _productRepository.Add(product1);
            _productRepository.Add(product2);
            _productRepository.SaveAll();

            Provider provider = new Provider()
            {
                Name = "Addidas",
            };

            Provider provider1 = new Provider()
            {
                Name = "Nike",
            };
            Provider provider2 = new Provider()
            {
                Name = "Puma",
            };

            _providerRepository.Add(provider);
            _providerRepository.Add(provider1);
            _providerRepository.Add(provider2);

            _providerRepository.SaveAll();

            ProductProvider ProductProvider = new ProductProvider()
            {
                Product = product,
                Provider = provider,
            };

            ProductProvider ProductProvider1 = new ProductProvider()
            {
                Product = product1,
                Provider = provider1,
            };

            ProductProvider ProductProvider2 = new ProductProvider()
            {
                Product = product2,
                Provider = provider2,
            };

            _productProviderRepository.Add(ProductProvider);
            _productProviderRepository.Add(ProductProvider1);
            _productProviderRepository.Add(ProductProvider2);
            _productProviderRepository.SaveAll();

            ProductManagement productManagement = new ProductManagement() {
                Product = product,
                Amount = 1,
                AverageCost = product.Price * 1,
                TotalCost = product.Price,
                Warehouse = warehouse,
            };

            ProductManagement productManagement1 = new ProductManagement()
            {
                Product = product1,
                Amount = 1,
                AverageCost = product.Price * 1,
                TotalCost = product.Price,
                Warehouse = warehouse2,
            };

            ProductManagement productManagement2 = new ProductManagement()
            {
                Product = product2,
                Amount = 1,
                AverageCost = product.Price * 1,
                TotalCost = product.Price,
                Warehouse = warehouse2,
            };

            _productManagementRepository.Add(productManagement);
            _productManagementRepository.Add(productManagement1);
            _productManagementRepository.Add(productManagement2);
            _productManagementRepository.SaveAll();

            ProductTransition productTransition = new ProductTransition()
            {
                Product = product,
                Quantity = 1,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = product.Price,
                Warehouse = warehouse
            };

            ProductTransition productTransition1 = new ProductTransition()
            {
                Product = product1,
                Quantity = 1,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = product.Price,
                Warehouse = warehouse2
            };

            ProductTransition productTransition2 = new ProductTransition()
            {
                Product = product2,
                Quantity = 1,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = product.Price,
                Warehouse = warehouse2
            };

            _productTranstion.Add(productTransition);
            _productTranstion.Add(productTransition1);
            _productTranstion.Add(productTransition2);
            _productTranstion.SaveAll();
        }

        [HttpGet("showcase")]
        public void CadastrarVitrine()
        {

            ProductManagement p = _productManagementRepository.Get(p => p.Id.Equals(new Guid("4F4F7916-761A-432F-456D-08D882ADC925"))).FirstOrDefault();
            ProductManagement p1 = _productManagementRepository.Get(p => p.Id.Equals(new Guid("2D729FCC-AF9B-4C6B-456E-08D882ADC925"))).FirstOrDefault();
            ProductManagement p2 = _productManagementRepository.Get(p => p.Id.Equals(new Guid("398DD27F-96AE-4A82-456F-08D882ADC925"))).FirstOrDefault();

            Showcase showcase = new Showcase() {
                Color = "D35269",
                Description = "Only for Curles! Seu coração barcelonista bate mais forte com a nova Camisa do Barcelona Home 20/21 s/nº Torcedor Nike Masculina. Entortando qualquer varal, a camisa titular do Barça traz peso e tradição que só quem torce por Més que un club sabe a dimensão. O manto traz faixas em azul e grená com linhas em amarelo que contornam cada faixa e a gola redonda. O desing é inspirado no uniforme da década de 1920, a primeira Era de Ouro do vitorioso Barça. Vista a sua Camisa do Barcelona I 2020 Nike Masculina!",
                Image = "barcelonaIMG.jpg",
                ProductManagement = p,
            };

            Showcase showcase1 = new Showcase()
            {
                Color = "3B5249",
                Description = "Vista a sua vibração e amor pelo Real com a nova Camisa do Real Madrid II 20/21 s/nº Torcedor Adidas Masculina! Com tom de rosa diferenciado para marcar presença em campo, a nova Camisa reserva do Real Madrid é inspirada na 'college and decollage', que mescla arte contemporânea e a cultura do grafite. O escudo do Rey e o logo Adidas são aplicados em azul marinho em conjunto com as Três Listras laterais, para você jogar e torcer com muito estilo. Mostre a força de ser Madrilenho com a Camisa Rosa do Real Madrid II 2020 Adidas Masculina!",
                Image = "RealIMG.jpg",
                ProductManagement = p1,
            };

            Showcase showcase2 = new Showcase()
            {
                Color = "823038",
                Description = "Os torcedores do Liverpool não podem deixar de adquirir a nova Camisa de Treino do clube. Confeccionado com tecido leve e responsável, a Camisa Liverpool Masculina é predominantemente preta com detalhes em vermelho que deixam o manto ainda mais de responsa. Para torcer, bater uma bola com os amigos ou só adicionar na sua coleção de camisas de futebol, garanta já a sua Camisa Liverpool Nke e marque esse golaço!",
                Image = "LiverpoolIMG.jpg",
                ProductManagement = p2,
            };

            _showcaseRepository.Add(showcase);
            _showcaseRepository.Add(showcase1);
            _showcaseRepository.Add(showcase2);
            _showcaseRepository.SaveAll();
        }
    }
}
