using Catalog.API.Domain.Models;
using Mapster;

namespace Catalog.API.Application.DTO;

public class AttributeDto : IMapFrom<ProductAttribute>
{
    public string ID { get; set; }
    public string Type { get; set; } = default!;
    public string Value { get; set; } = default!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<ProductAttribute, AttributeDto>()
            .Map(dest => dest.Type, source => source.Type.ToString());
    }
}
