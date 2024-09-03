using System;
using System.Data.Common;
using BuildingBlocks.Enums;

namespace BuildingBlocks.Exceptions;

public class DbErrorException : DbException
{
    public DbErrorException(string message) : base(message) { }

    public DbErrorException(string message, Exception innerException) : base(message, innerException) { }

    public DbErrorException(Operations operations, string entityName) : base($"Error while {operations} {entityName}!") { }

    public DbErrorException(Operations operations, string entityName, string details) : base($"Error while {operations} {entityName}")
    {
        Details = details;
    }

    public DbErrorException(string message, Exception innerException, string details) : base(message, innerException)
    {
        Details = details;
    }

    public string Details { get; }
}
