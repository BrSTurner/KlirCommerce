using Klir.TechChallenge.Core.Messages;

namespace Klir.TechChallenge.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<CommandResult<TResult>> SendCommand<TResult, TRequest>(TRequest command) where TRequest : Command<TResult>;
    }
}
