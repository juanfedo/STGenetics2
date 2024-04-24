using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
    public class FoodTypeRepository : IFoodTypeRepository
    {
        private readonly string? _connectionString;

        public FoodTypeRepository(IOptions<AppSettingsModel> settings)
        {
            _connectionString = settings.Value.LocalConnection;
        }

        public async Task<FoodType> GetFoodTypeByIdAsync(int id, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var foodType = await con.ExecuteScalarAsync<FoodType>(new CommandDefinition("" +
                "SELECT Id, Name " +
                "FROM FoodType " +
                "WHERE Id = @id", parameters: new { id }, cancellationToken: cancellationToken));
            if (foodType == null)
            {
                throw new Exception("Food type doesnt exists");
            }
            return foodType;
        }
    }
}
