namespace Klir.TechChallenge.Core.Notifications
{
    public interface INotificator
    {
        public void AddMessage(string message);
        public void AddMessages(params string[] messages);
        public IReadOnlyCollection<string> GetErrorMessages();
        public bool HasErrors { get; }
    }
}
