using Products.Api.Models;

namespace Products.Api.Events
{
    public class ProductChangeEvent
    {
        public ProductChangeEvent(Product product)
        {
            Metadata = new ProductMetadata
            {
                Code = product.Code,
                Name = product.Name,
                UnitType = product.UnitType,
                Description = product.Description
            };
        }

        public ProductMetadata Metadata { get; private set; }

        public string EventType { get; set; }
    }
}
