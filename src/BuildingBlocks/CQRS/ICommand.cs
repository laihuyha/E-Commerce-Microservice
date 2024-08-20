using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// This Command just for excute something and don't need response back from DB.
    /// <br/>
    /// <br/>
    /// Example: Like Delete() record from DB or Savechange() and we don't care much about result of them.
    /// </summary>
    public interface ICommand : IRequest<Unit> { }

    /// <summary>
    /// Excute command and need the response from DB.
    /// <br/>
    /// <br/>
    /// Example: Create, Update, Savechange and Delete incase we need status for some conditional statement.
    /// </summary>
    /// <typeparam name="TReponse"></typeparam>
    public interface ICommand<out TReponse> : IRequest<TReponse> where TReponse : notnull { }
}