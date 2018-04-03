using System;
using System.Threading.Tasks;

namespace Core.Domain.UwO
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
