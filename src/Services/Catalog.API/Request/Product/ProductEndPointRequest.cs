using System.Collections.Generic;

namespace Catalog.API.Request.Product
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile, decimal Price);
}