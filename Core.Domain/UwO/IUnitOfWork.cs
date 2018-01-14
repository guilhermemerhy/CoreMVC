using System;

namespace Core.Domain.UwO
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
