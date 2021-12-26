using Microsoft.EntityFrameworkCore;
using Products.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Products.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(string code)
        {
            return await _context.Products.Where(e => e.Code == code).FirstOrDefaultAsync(); ;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> InsertProduct(Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateProduct(string code, Product product)
        {
            Product productToUpdate = _context.Products.Where(e => e.Code == code).Select(e => e).FirstOrDefault();
            if (productToUpdate != null)
            {
                productToUpdate.Code = code;
                productToUpdate.Sku = product.Sku;
                productToUpdate.Description = product.Description;
                productToUpdate.Name = product.Name;
                productToUpdate.Type = product.Type;
                productToUpdate.UnitType = product.UnitType;
                _context.Products.Update(productToUpdate);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> DeleteProduct(string code)
        {
            Product product = _context.Products.Where(e => e.Code == code).Select(e => e).FirstOrDefault();
            if (product != null)
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

    }
}
