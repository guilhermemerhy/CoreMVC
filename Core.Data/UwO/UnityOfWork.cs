using Core.Domain.UwO;

namespace Core.Data.UwO
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private AppDataContext _context;

        public UnitOfWork(AppDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
