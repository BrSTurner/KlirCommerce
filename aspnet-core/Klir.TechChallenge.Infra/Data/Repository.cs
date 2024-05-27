using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace Klir.TechChallenge.Infra.Data
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly KlirCommerceContext _context;
        private readonly DbSet<T> _entity;

        public Repository(KlirCommerceContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
