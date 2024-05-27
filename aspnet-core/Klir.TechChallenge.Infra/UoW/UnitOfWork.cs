using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Infra.Data;

namespace Klir.TechChallenge.Infra.UoW
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly KlirCommerceContext _context;

        public UnitOfWork(KlirCommerceContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                var affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
