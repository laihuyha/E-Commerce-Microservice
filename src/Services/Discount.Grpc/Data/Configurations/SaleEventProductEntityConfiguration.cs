using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Grpc.Data.Configurations;

public class SaleEventProductEntityConfiguration : IEntityTypeConfiguration<SaleEventProduct>
{
    public void Configure(EntityTypeBuilder<SaleEventProduct> builder)
    {
        builder.HasKey(e => new { e.SaleEventId, e.ProductId });

        builder.HasOne(e => e.SaleEvent)
               .WithMany(e => e.SaleEventProducts)
               .HasForeignKey(e => e.SaleEventId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(e => e.Product)
               .WithMany(e => e.SaleEventProducts)
               .HasForeignKey(e => e.ProductId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        // Index for better query performance
        builder.HasIndex(sep => sep.SaleEventId);
        builder.HasIndex(sep => sep.ProductId);
    }
}
