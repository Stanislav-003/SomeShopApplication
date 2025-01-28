using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SomeShop.Domain.Purchases;

namespace SomeShop.Infrastructure.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(p => p.Number)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new Number(v))
            .HasMaxLength(50);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.TotalPrice)
            .IsRequired()
            .HasConversion(
                v => v.Value, 
                v => new TotalPrice(v));

        builder.HasOne(p => p.User)
            .WithMany(u => u.Purchases)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Items)
            .WithOne()
            .HasForeignKey(i => i.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
