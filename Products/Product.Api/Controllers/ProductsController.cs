using Microsoft.AspNetCore.Mvc;
using Products.Api.Models;
using Products.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _service.GetProducts();
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<Product> GetProduct(string code)
        {
            return await _service.GetProductById(code);
        }

        [HttpPost]
        public async Task<int> AddProduct(Product product)
        {
            return await _service.InsertProduct(product);
        }

        [HttpPost]
        [Route("{code}")]
        public async Task<int> UpdateProduct(string code, Product product)
        {
            return await _service.UpdateProduct(code, product);
        }

        [HttpDelete]
        [Route("{code}")]
        public async Task<int> DeleteProduct(string code)
        {
            return await _service.DeleteProduct(code);
        }
    }
}
