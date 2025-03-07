using System;
using Google.Protobuf.WellKnownTypes;
using Mapster;

namespace Discount.Grpc.Services.MappingProfile;

public class MapsterRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Global DateTime <-> Timestamp conversion -> Applied for all object has these type
        // In your mapping configuration
        config.NewConfig<DateTime?, Timestamp>()
              .MapWith(src => src.HasValue
                  ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.Value, DateTimeKind.Utc))
                  : null);

        config.NewConfig<Timestamp, DateTime?>()
              .MapWith(src => src != null ? src.ToDateTime() : null);

        // Model specific mappings => Can be removed
        // config.NewConfig<Models.Coupon, Coupon>()
        //       .Map(dest => dest.Amount, src => (double)src.Amount)
        //       .Map(dest => dest.Type, src => (int)src.Type);
        //
        // config.NewConfig<Coupon, Models.Coupon>()
        //       .Map(dest => dest.Amount, src => (decimal)src.Amount)
        //       .Map(dest => dest.Type, src => (Models.DiscountType)src.Type);
    }
}
