using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Products.Api.Models;

namespace Products.Api
{
    public static class DataSeedExtensionscs
    {
        public static void UseSeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<ProductDbContext>())
                {
                    if (context.Database.EnsureCreated())
                    {
                        context.Products.Add(new Product { Code = "1", Description = "Product 1", Name = "Description 1", Sku = "Sku 1", Type = "Type 1", UnitType = "Unit Type 1" });
                        context.Products.Add(new Product { Code = "2", Description = "Product 2", Name = "Description 2", Sku = "Sku 2", Type = "Type 2", UnitType = "Unit Type 2" });
                        context.Products.Add(new Product { Code = "3", Description = "Product 3", Name = "Description 3", Sku = "Sku 3", Type = "Type 3", UnitType = "Unit Type 3" });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
