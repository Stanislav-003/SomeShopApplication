using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;

namespace SomeShop.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(product => product.Value, value => new ProductId(value));

        builder.OwnsOne(p => p.Name);

        builder.OwnsOne(p => p.Category);

        builder.OwnsOne(p => p.Article);

        builder.OwnsOne(p => p.Price);

        //
        builder.HasMany(p => p.PurchaseItems).WithOne().HasForeignKey(pi => pi.ProductId);
    }
}
