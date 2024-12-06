using System.Reflection;
using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DataContext : DbContext
{
    public DbSet<Models.Coupon>    Coupons    { get; set; }
    public DbSet<Models.SaleEvent> SaleEvents { get; set; }

    public DbSet<SaleEventProduct> SaleEventProducts { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
