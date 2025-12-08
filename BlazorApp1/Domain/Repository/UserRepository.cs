using BlazorApp1.Applictaion.Dto;
using BlazorApp1.Domain.Entity;
using BlazorApp1.Domain.ValueObject;
using Dapper;
using Microsoft.Data.SqlClient;
using BlazorApp1.Infrastructure.Persistence;

namespace BlazorApp1.Domain.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public async Task AddAsync(User user, IUnitOfWork uow)
        {
            var sql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
            var parameters = new { Name = user.Name, Email = user.Email.Value };

            try
            {
                await uow.Connection.ExecuteAsync(sql, parameters, uow.Transaction);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "DB 오류 발생: ADD User, User: {@User}", user);
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(int id, IUnitOfWork uow)
        {
            var sql = "SELECT Id, Name, Email FROM Users WHERE Id = @Id";
            var dto = await uow.Connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { Id = id }, uow.Transaction);

            return dto == null ? null : new User(dto.Id, dto.Name, new Email(dto.Email));
        }

        public async Task<IEnumerable<User>> GetAllAsync(IUnitOfWork uow)
        {
            var sql = "SELECT Id, Name, Email FROM Users";
            var dtos = await uow.Connection.QueryAsync<UserDto>(sql, transaction: uow.Transaction);

            return dtos.Select(dto => new User(dto.Id, dto.Name, new Email(dto.Email)));
        }

        public async Task UpdateAsync(User user, IUnitOfWork uow)
        {
            var sql = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
            var parameters = new { Name = user.Name, Email = user.Email.Value, Id = user.Id };

            await uow.Connection.ExecuteAsync(sql, parameters, uow.Transaction);
        }

        public async Task DeleteAsync(int id, IUnitOfWork uow)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            await uow.Connection.ExecuteAsync(sql, new { Id = id }, uow.Transaction);
        }
    }
}