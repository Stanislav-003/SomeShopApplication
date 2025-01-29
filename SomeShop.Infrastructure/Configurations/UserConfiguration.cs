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

        builder.Property(u => u.Id).HasConversion(user => user.Value, value => new UserId(value));

        builder.OwnsOne(u => u.FullName, fn =>
        {
            fn.Property(f => f.FirstName).HasColumnName("FirstName").IsRequired();
            fn.Property(f => f.LastName).HasColumnName("LastName").IsRequired();
        });

        builder.Property(u => u.DateOfBirth).IsRequired();

        builder.Property(u => u.RegistrationDate).IsRequired();
       
        //
        builder.HasMany(u => u.Purchases).WithOne().HasForeignKey(p => p.UserId);
    }
}
