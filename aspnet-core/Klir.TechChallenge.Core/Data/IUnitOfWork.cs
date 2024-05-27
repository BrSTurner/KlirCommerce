namespace Klir.TechChallenge.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
