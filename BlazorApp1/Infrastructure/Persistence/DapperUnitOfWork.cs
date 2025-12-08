using BlazorApp1.Data;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BlazorApp1.Infrastructure.Persistence
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public DapperUnitOfWork(DapperContext context)
        {
            _connection = context.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public void Commit() => _transaction.Commit();
        public void Rollback() => _transaction.Rollback();

        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();
        }
    }
}
