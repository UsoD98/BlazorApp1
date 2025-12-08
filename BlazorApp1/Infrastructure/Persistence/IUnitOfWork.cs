using System.Data;

namespace BlazorApp1.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();

    }
}
