using FluentValidation.Results;
using MediatR;

namespace Klir.TechChallenge.Core.Messages
{
    public abstract class Command<T> : Message, IRequest<CommandResult<T>>
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid() => throw new NotImplementedException();
    }
}
