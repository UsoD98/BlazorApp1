using BlazorApp1.Common;
using BlazorApp1.Data;
using BlazorApp1.Domain.Entity;
using BlazorApp1.Domain.Repository;
using BlazorApp1.Domain.ValueObject;
using BlazorApp1.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace BlazorApp1.Applictaion.Service
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly DapperContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, DapperContext context, ILogger<UserService> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<User>> RegisterUserAsync(int id, string name, string emailValue)

        {
            using var uow = new DapperUnitOfWork(_context);

            try
            {
                _logger.LogInformation("사용자 등록 시작: {Name}, {Email}", name, emailValue);

                var user = new User(id, name, new Email(emailValue));
                await _repository.AddAsync(user, uow);

                uow.Commit();
                _logger.LogInformation("사용자 등록 성공: Id={Id}", user.Id);

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                _logger.LogError(ex, "사용자 등록 실패: {Name}, {Email}", name, emailValue);
                return Result<User>.Failure("사용자 등록 중 오류가 발생했습니다.");
            }
        }

    }
}
