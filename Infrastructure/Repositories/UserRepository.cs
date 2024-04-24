using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;

        public UserRepository(IOptions<AppSettingsModel> settings)
        {
            _connectionString = settings.Value.LocalConnection;
        }

        public async Task<int> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);

            var param = new DynamicParameters();
            param.Add("@Name", user.Name);
            param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var res = await con.ExecuteAsync(new CommandDefinition("SP_CreateUser", param, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            var createID = param.Get<int>("@retID");

            return createID;
        }
        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var users = await con.QueryAsync<User>(new CommandDefinition("" +
                "SELECT Id, Name " +
                "FROM Users " +
                "ORDER BY 1", cancellationToken: cancellationToken));
            return users.ToList();
        }

    }
}
