using SomeShop.Domain.Products;
using SomeShop.Infrastructure;

namespace SomeShop.Api.Extensions;

public static class SeedDataExtencions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Products.Any())
        {
            var random = new Random();
            var products = Enumerable.Range(1, 60).Select(i =>
            {
                var name = new Name($"Product {i}");
                var category = new Categtory("Category A");
                var article = new Article($"ART{i:000}");
                var price = new Price(random.Next(10, 10000));

                var product = Product.Create(name, category, article, price).Value;
                return product;
            }).ToList();

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
