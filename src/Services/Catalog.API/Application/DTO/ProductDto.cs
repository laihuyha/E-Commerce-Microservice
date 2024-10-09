using System.Collections.Generic;
using Catalog.API.Domain.Models;
using Mapster;

namespace Catalog.API.Application.DTO;

public class ProductDto : IMapFrom<Product>
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<AttributeDto> Attributes { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Categories, source => source.Categories.Adapt<List<CategoryDto>>())
            .Map(dest => dest.Attributes, source => source.Attributes.Adapt<List<AttributeDto>>());
    }
}
