using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;

namespace SomeShop.Infrastructure.Configurations;

public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.ToTable("PurchaseItems");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(pi => pi.ProductId)
            .IsRequired();

        builder.Property(pi => pi.PurchaseId)
            .IsRequired();

        builder.Property(pi => pi.Quantity)
            .IsRequired();

        builder.HasOne(pi => pi.Product)
            .WithMany()
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(pi => pi.Purchase)
            .WithMany(p => p.Items)
            .HasForeignKey(pi => pi.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
