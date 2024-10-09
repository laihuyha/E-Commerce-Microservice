namespace Catalog.API.Application.DTO;

public class CategoryDto
{
    public string ID { get; set; }
    public string Name { get; set; } = default!;
    public string ParentCateId { get; set; } = default!;
    public string Description { get; set; } = default!;
}
