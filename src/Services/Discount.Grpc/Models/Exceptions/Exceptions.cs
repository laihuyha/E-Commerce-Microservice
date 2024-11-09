using BuildingBlocks.Exceptions;

namespace Discount.Grpc.Models.Exceptions;

public class NotFoundException : BuildingBlocks.Exceptions.NotFoundException
{
    public NotFoundException(string message) : base(message)
    {
    }
}

public class DbOperationException : DbErrorException
{
    public DbOperationException(string message) : base(message)
    {
    }
}
