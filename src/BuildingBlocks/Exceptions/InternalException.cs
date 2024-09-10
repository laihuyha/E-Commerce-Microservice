using System;

namespace BuildingBlocks.Exceptions;

public class InternalException : Exception
{
    public InternalException(string message) : base(message)
    {
    }

    public InternalException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string Details { get; }
}
