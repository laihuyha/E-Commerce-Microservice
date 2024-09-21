using Basket.API.Application.DTO.Results;
using Basket.API.Domain.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.DTO.Command;

public record StoreCartCommand(Cart Cart) : ICommand<StoreCartResult>;
public record DeleteCartCommand(string UserId) : ICommand<DeleteCartResult>;