using FluentValidation.Results;
using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.Events;
using Klir.TechChallenge.Core.Mediator;

namespace Klir.TechChallenge.Core.Handlers
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;
        protected readonly IMediatorHandler _mediator;

        protected CommandHandler(IMediatorHandler mediatorHandler)
        {
            ValidationResult = new ValidationResult();
            _mediator = mediatorHandler;
        }

        protected async Task AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
            await NotifyDomainErrors();
        }

        protected async Task<ValidationResult> CommitChanges(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.CommitAsync())
            {
                await AddError("Something went wrong commiting data to the database");
                await NotifyDomainErrors();
            }

            return ValidationResult;
        }

        protected async Task NotifyDomainErrors() => await NotifyDomainErrors(ValidationResult);
        protected async Task NotifyDomainErrors(ValidationResult validationResult) => await _mediator.PublishEvent(new DomainNotification(validationResult));
    }
}
