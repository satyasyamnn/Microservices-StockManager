using Products.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string code);
        Task<int> InsertProduct(Product product);
        Task<int> UpdateProduct(string code, Product product);
        Task<int> DeleteProduct(string code);
    }
}
