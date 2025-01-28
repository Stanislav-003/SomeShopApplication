using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SomeShop.Domain.Users;

namespace SomeShop.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired()
            .ValueGeneratedNever(); 

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new FirstName(v)) 
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasConversion(
                v => v.Value, 
                v => new LastName(v)) 
            .HasMaxLength(50); 

        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.HasMany(u => u.Purchases)
            .WithOne(p => p.User) 
            .HasForeignKey(p => p.UserId) 
            .OnDelete(DeleteBehavior.Cascade);
    }
}
