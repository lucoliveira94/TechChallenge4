using CadastroAPI.Models;
using CadastroAPI.RabbitMQ;
using CadastroAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProductController(IProductService _productService, IRabitMQProducer rabitMQProducer)
        {
            productService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpGet("ProductList")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }
        [HttpGet("GetProductIP")]
        public Product GetProductById(int Id)
        {
            return productService.GetProductById(Id);
        }
        [HttpPost("AddProduct")]
        public Product AddProduct(Product product)
        {
            var productData = productService.AddProduct(product);

            _rabitMQProducer.SendProductMessage(productData);
            return productData;
        }
        [HttpPut("UpdateProduct")]
        public Product UpdateProduct(Product product)
        {
            return productService.UpdateProduct(product);
        }
        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct(int Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}