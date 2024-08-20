using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Interface for Query
    /// </summary>
    /// <typeparam name="TQuery">Should be a class inherited from IQuery.</typeparam>
    /// <typeparam name="TQueryResult">The result type query should returned.</typeparam>
    public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
    where TQueryResult : notnull
    { }
}