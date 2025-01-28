using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SomeShop.Domain.Products;

namespace SomeShop.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasConversion(
                v => v.Value, 
                v => new Name(v))
            .HasMaxLength(100);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasConversion(
                v => v.Value, 
                v => new Categtory(v))
            .HasMaxLength(50);

        builder.Property(p => p.Article)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new Article(v))
            .HasMaxLength(50);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new Price(v));
    }
}
