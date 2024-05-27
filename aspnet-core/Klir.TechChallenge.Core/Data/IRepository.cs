using Klir.TechChallenge.Core.DomainObjects;
using System.Data.Common;

namespace Klir.TechChallenge.Core.Data
{
    public interface IRepository<T> : IDisposable where T : class, IAggregateRoot
    {
        DbConnection GetConnection();
    }
}
