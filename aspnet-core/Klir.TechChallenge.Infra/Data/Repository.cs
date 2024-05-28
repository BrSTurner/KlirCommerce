using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Klir.TechChallenge.Infra.Data
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly KlirCommerceContext _context;
        protected readonly DbSet<T> _entity;

        public Repository(KlirCommerceContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

     
    }
}
