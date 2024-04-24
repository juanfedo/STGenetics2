using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly string? _connectionString;

        public FoodRepository(IOptions<AppSettingsModel> settings)
        {
            _connectionString = settings.Value.LocalConnection;
        }

        public async Task<List<Food>> GetFoodByTypeAsync(int typeId, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var food = await con.QueryAsync<Food>(new CommandDefinition("" +
                "SELECT Food.Id, Food.Name, Price, Type.Name as Type " +
                "FROM Food " +
                "INNER JOIN FoodType as Type on Food.FoodTypeId = Type.Id " +
                "WHERE Type.Id = @typeId", parameters: new { typeId }, cancellationToken: cancellationToken));

            if (food is null || food.ToList().Count == 0)
            {
                throw new Exception("Type doesn't exists");
            }
            return food.ToList();
        }

        public async Task<List<Food>> GetAllFoodAsync(CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var food = await con.QueryAsync<Food>(new CommandDefinition("" +
                "SELECT Food.Id, Food.Name, Price, Type.Name as Type " +
                "FROM Food " +
                "INNER JOIN FoodType as Type on Food.FoodTypeId = Type.Id", cancellationToken: cancellationToken));

            if (food is null || food.ToList().Count == 0)
            {
                throw new Exception("Food doesn't exists");
            }
            return food.ToList();
        }
    }
}
