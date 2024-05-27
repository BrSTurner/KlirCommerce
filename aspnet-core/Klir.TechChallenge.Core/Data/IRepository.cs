using Klir.TechChallenge.Core.DomainObjects;

namespace Klir.TechChallenge.Core.Data
{
    public interface IRepository<T> : IDisposable where T : class, IAggregateRoot
    {
    }
}
