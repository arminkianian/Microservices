using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OrderService.WebApi.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Product> products = new List<Product>
            {
                new Product
                {
                    ProductId=1,
                    Price = 10000,
                    ProductName = "ساعت مچی"
                },
                new Product
                {
                    ProductId=2,
                    Price = 33000,
                    ProductName = "کیبورد"
                },
                new Product
                {
                    ProductId=3,
                    Price = 75000,
                    ProductName = "مانیتور"
                },
            };
        private static readonly List<OrderLine> orderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderId=1,
                    Count=3,
                    OrderLineId=1,
                    ProductId = products.FirstOrDefault(c=>c.ProductId == 1).ProductId,
                    Product=products.FirstOrDefault(c=>c.ProductId == 1),
                    Price = 3 * products.FirstOrDefault(c=>c.ProductId == 1).Price
                 },
                new OrderLine
                {
                    OrderId=1,
                    Count=5,
                    OrderLineId=2,
                    ProductId = products.FirstOrDefault(c=>c.ProductId == 2).ProductId,
                    Product=products.FirstOrDefault(c=>c.ProductId == 2),
                    Price = 5 * products.FirstOrDefault(c=>c.ProductId == 2).Price
                }

            };
        private static readonly List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderId =1,
                    OrderDate = DateTime.Now,
                    PersonId=1,
                    TotalPrice = orderLines.Sum(c=>c.Price),
                    OrderLines = orderLines
                },
                new Order
                {
                    OrderId =2,
                    OrderDate = DateTime.Now,
                    PersonId=2,
                    TotalPrice = orderLines.Sum(c=>c.Price),
                    OrderLines = orderLines
                },
                new Order
                {
                    OrderId =3,
                    OrderDate = DateTime.Now,
                    PersonId=1,
                    TotalPrice = orderLines.Sum(c=>c.Price),
                    OrderLines = orderLines
                },
            };


        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public List<Order> Get(int id = 1)
        {
            return orders.Where(c => c.PersonId == id).ToList();
        }
    }
}
