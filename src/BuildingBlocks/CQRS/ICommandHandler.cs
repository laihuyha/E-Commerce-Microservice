using MediatR;

namespace BuildingBlocks.CQRS
{

    /// <summary>
    /// Interface for Command that doesn't require response from DB after excute command
    /// </summary>
    /// <typeparam name="TCommand">Should be a class inherited from ICommand.</typeparam>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
    {
    }

    /// <summary>
    /// Interface for Command
    /// </summary>
    /// <typeparam name="TCommand">Should be a class inherited from ICommand.</typeparam>
    /// <typeparam name="TCommandResult">The result type command should returned.</typeparam>
    public interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : ICommand<TCommandResult>
    where TCommandResult : notnull
    {
    }
}