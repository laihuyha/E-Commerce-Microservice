using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Query is different from Command when query we always need the response from DB and Server
    /// </summary>
    /// <typeparam name="TReponse"><para>Could be <b>Unit</b> or <b>Object</b></para></typeparam>
    public interface IQuery<out TReponse> : IRequest<TReponse> where TReponse : notnull
    {

    }
}