using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
{
    public void Configure(EntityTypeBuilder<ProductRating> builder)
    {
        builder.ToTable("ProductRatings");

        builder.HasKey(u => new { u.ProductId, u.UserId});
        builder.Property(u => u.Rating).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();

        builder.HasOne<User>()
            .WithMany(u => u.ProductRatings)
            .HasForeignKey(pr => pr.UserId);
    }
}