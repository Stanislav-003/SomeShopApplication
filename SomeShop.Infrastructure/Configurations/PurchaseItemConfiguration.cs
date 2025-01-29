using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Infrastructure.Configurations;

public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.ToTable("PurchaseItems");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Id).HasConversion(purchaseItem => purchaseItem.Value, value => new PurchaseItemId(value));
        
        builder.OwnsOne(p => p.Quantity);

        builder.OwnsOne(p => p.PricePerUnit);

        builder.HasOne<Product>().WithMany().HasForeignKey(pi => pi.ProductId);

        builder.HasOne<Purchase>().WithMany().HasForeignKey(pi => pi.PurchaseId);
    }
}
