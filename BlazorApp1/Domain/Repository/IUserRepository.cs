using BlazorApp1.Domain.Entity;
using BlazorApp1.Infrastructure.Persistence;

namespace BlazorApp1.Domain.Repository
{
    public interface IUserRepository
    {
        Task AddAsync(User user, IUnitOfWork uow);
        Task<User?> GetByIdAsync(int id, IUnitOfWork uow);
        Task<IEnumerable<User>> GetAllAsync(IUnitOfWork uow);
        Task UpdateAsync(User user, IUnitOfWork uow);
        Task DeleteAsync(int id, IUnitOfWork uow);
    }
}
