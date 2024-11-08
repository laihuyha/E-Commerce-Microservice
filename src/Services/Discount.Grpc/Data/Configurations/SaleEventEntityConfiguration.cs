using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Grpc.Data.Configurations;

public class SaleEventEntityConfiguration : IEntityTypeConfiguration<Models.SaleEvent>
{
    public void Configure(EntityTypeBuilder<Models.SaleEvent> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Description).HasColumnType("TEXT").HasMaxLength(255);
        builder.Property(e => e.SalePercent).HasColumnType("REAL").HasPrecision(5, 2).IsRequired();
        builder.Property(e => e.StartDate).HasColumnType("TEXT").IsRequired();
        builder.Property(e => e.EndDate).HasColumnType("TEXT").IsRequired();
    }
}
