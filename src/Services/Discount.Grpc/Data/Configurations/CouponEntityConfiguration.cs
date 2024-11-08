using System;
using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Grpc.Data.Configurations;

public class CouponEntityConfiguration : IEntityTypeConfiguration<Models.Coupon>
{
    public void Configure(EntityTypeBuilder<Models.Coupon> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Amount).HasColumnType("REAL").IsRequired();
        builder.Property(e => e.Type)
               .HasColumnType("INTEGER")
               .IsRequired()
               .HasConversion(v => (int)v,
                   v => ValidateAndConvertDiscountType(v)
               );
        builder.Property(e => e.Description).IsRequired(false);
        builder.Property(e => e.ProductName).IsRequired();
        builder.Property(e => e.ExpiryDate).IsRequired(false);
    }

    private static DiscountType ValidateAndConvertDiscountType(int v)
    {
        if (!Enum.IsDefined(typeof(DiscountType), v))
        {
            throw new ArgumentException($"Invalid DiscountType value: {v}");
        }

        return (DiscountType)v;
    }
}
