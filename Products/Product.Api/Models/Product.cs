using System.ComponentModel.DataAnnotations;

namespace Products.Api.Models
{
    public class Product
    {
        [Key]
        public string Sku { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public string Code { get; set; }

    }
}
