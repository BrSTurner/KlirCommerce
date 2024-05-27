namespace Klir.TechChallenge.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
