using Catalog.API.Domain.Models;
using Mapster;

namespace Catalog.API.Application.DTO;

public class BrandDto : IMapFrom<Brand>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;
}
