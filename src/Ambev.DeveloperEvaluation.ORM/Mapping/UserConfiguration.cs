using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.OwnsOne(a => a.Address, address =>
        {
            address.Property(a => a.State).HasColumnName("State").HasMaxLength(3);
            address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(128);
            address.Property(a => a.City).HasColumnName("City").HasMaxLength(64);
            address.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(8);
            address.Property(a => a.Number).HasColumnName("Number").HasMaxLength(8);
            address.Property(a => a.GeoLocation).HasColumnName("Geo").HasMaxLength(32);
        });

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

    }
}
