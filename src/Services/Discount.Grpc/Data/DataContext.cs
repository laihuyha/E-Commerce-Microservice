using System.Reflection;
using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DataContext : DbContext
{
    public virtual DbSet<Models.Coupon>    Coupons    { get; set; }
    public virtual DbSet<Models.SaleEvent> SaleEvents { get; set; }

    public virtual DbSet<SaleEventProduct> SaleEventProducts { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}