using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Infrastructure.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(purchase => purchase.Value, value => new PurchaseId(value));

        builder.OwnsOne(p => p.Number);

        builder.OwnsOne(p => p.TotalPrice);

        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasOne<User>().WithMany().HasForeignKey(p => p.UserId).IsRequired();

        //
        builder.HasMany(p => p.PurchaseItems).WithOne().HasForeignKey(pi => pi.PurchaseId);
    }
}
