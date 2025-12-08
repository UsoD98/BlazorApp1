using System.Data;
using Microsoft.Data.SqlClient;

namespace BlazorApp1.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

#pragma warning disable CS8618 // null을 허용하지 않는 필드는 생성자를 종료할 때 null이 아닌 값을 포함해야 합니다. 'required' 한정자를 추가하거나 nullable로 선언하는 것이 좋습니다.
        public DapperContext(IConfiguration configuration)
#pragma warning restore CS8618 // null을 허용하지 않는 필드는 생성자를 종료할 때 null이 아닌 값을 포함해야 합니다. 'required' 한정자를 추가하거나 nullable로 선언하는 것이 좋습니다.
        {
            _configuration = configuration;
#pragma warning disable CS8601 // 가능한 null 참조 할당입니다.
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601 // 가능한 null 참조 할당입니다.
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
