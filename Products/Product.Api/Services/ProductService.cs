using Products.Api.Events;
using Products.Api.Models;
using Products.Api.Notifications;
using Products.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly INotifier _notifier;
        public ProductService(IProductRepository repository, INotifier notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }       

        public async Task<Product> GetProductById(string code)
        {
            return await _repository.GetProductById(code);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }

        public async Task<int> InsertProduct(Product product)
        {
            int result = await _repository.InsertProduct(product);
            NotifyProductChangeEvent(product, "Update");
            return result;
        }

        public async Task<int> UpdateProduct(string code, Product product)
        {
            int result =  await _repository.UpdateProduct(code, product);
            NotifyProductChangeEvent(product, "Update");
            return result;
        }

        public async Task<int> DeleteProduct(string code)
        {
            Product product = await  GetProductById(code);
            int result = await _repository.DeleteProduct(code);
            NotifyProductChangeEvent(product, "Delete");
            return result;
        }

        private void NotifyProductChangeEvent(Product product, string eventType)
        {
            ProductChangeEvent productChangeEvent = new ProductChangeEvent(product);
            productChangeEvent.EventType = eventType;
            _notifier.Notify(productChangeEvent);
        }

    }
}
