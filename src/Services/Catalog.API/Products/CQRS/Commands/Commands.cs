using System.Collections.Generic;
using BuildingBlocks.CQRS;
using Catalog.API.Products.CQRS.Results;

namespace Catalog.API.Products.CQRS.Commands
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
}