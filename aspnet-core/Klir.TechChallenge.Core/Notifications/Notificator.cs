namespace Klir.TechChallenge.Core.Notifications
{
    public sealed class Notificator : INotificator
    {
        private List<string> Errors = new List<string>();

        public bool HasErrors
        {
            get
            {
                return Errors.Any();
            }
        }

        public void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Errors.Add(message);
        }

        public void AddMessages(params string[] messages)
        {
            var errorMessages = messages?.Where(x => !string.IsNullOrEmpty(x));

            if (errorMessages is null || !errorMessages.Any())
                return;

            Errors.AddRange(errorMessages);
        }

        public IReadOnlyCollection<string> GetErrorMessages() => Errors.AsReadOnly();
    }
}
