using Klir.TechChallenge.Core.Messages;
using MediatR;

namespace Klir.TechChallenge.Core.Mediator
{
    public sealed class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEvent<T>(T @event) where T : Event => _mediator.Publish(@event);

        public Task<CommandResult<TResult>> SendCommand<TResult, TRequest>(TRequest command) where TRequest : Command<TResult> => _mediator.Send(command);
        
    }
}
