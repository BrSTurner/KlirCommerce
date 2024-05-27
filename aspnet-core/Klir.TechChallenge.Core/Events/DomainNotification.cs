using FluentValidation.Results;
using Klir.TechChallenge.Core.Messages;
using MediatR;

namespace Klir.TechChallenge.Core.Events
{
    public sealed class DomainNotification : Event, INotification
    {
        private List<string> _messages { get; set; } = new List<string>();

        public IReadOnlyList<string> Messages
        {
            get
            {
                return _messages.AsReadOnly();
            }
        }

        public DomainNotification(string message) => _messages.Add(message);
        public DomainNotification(ValidationFailure failure) => _messages.Add(failure?.ErrorMessage ?? "An unknown error has occurred");
        public DomainNotification(ValidationResult result) => _messages.AddRange(result.Errors.Select(x => x.ErrorMessage));
        public DomainNotification(Exception exception) => _messages.Add(exception?.InnerException?.Message ?? exception?.Message ?? "An unknown error has occurred");
    }
}
