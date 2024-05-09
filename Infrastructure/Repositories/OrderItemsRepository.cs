using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly string? _connectionString;

        public OrderItemsRepository(IOptions<AppSettingsModel> settings)
        {
            _connectionString = settings.Value.LocalConnection;
        }

        public async Task<List<OrderItems>> GetOrderItemsByOrderIdAsync(int id, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var order = await con.QueryAsync<OrderItems>(new CommandDefinition("" +
                "SELECT OrderItems.Id as Id, Food.Name, Food.Id as FoodId, Price, FoodType.Name as Type " +
                "FROM OrderItems " +
                "INNER JOIN Food ON OrderItems.FoodId = Food.Id " +
                "INNER JOIN FoodType ON Food.FoodTypeId = FoodType.Id " +
                "WHERE OrderId = @id", parameters: new { id }, cancellationToken: cancellationToken));

            if (order == null)
            {
                throw new Exception("Order doesn´t exists");
            }
            return order.ToList();
        }

        public async Task CreateOrderItemsAsync(OrderItems orderItem, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var param = new
            {
                orderItem.OrderId,
                orderItem.FoodId,
            };

            await con.QueryAsync(new CommandDefinition("" +
                "INSERT INTO [OrderItems] (OrderId, FoodId) " +
                "VALUES (@OrderId, @FoodId); " +
                "SELECT CAST(SCOPE_IDENTITY() as int);", param, cancellationToken: cancellationToken));
        }

        public async Task<int> DeleteOrderItemsAsync(int orderId, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var result = await con.ExecuteAsync(new CommandDefinition("" +
                "DELETE " +
                "FROM OrderItems " +
                "WHERE OrderId = @orderId", parameters: new { orderId }, cancellationToken: cancellationToken));
            return Convert.ToInt32(result);
        }

        public async Task<List<Food>> GetOrderItemsByIdsAsync(string foodIds, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var result = await con.QueryAsync<Food>(new CommandDefinition("" +
                "SELECT Food.Name, Price, FoodType.Name as Type " +
                "FROM Food " +
                "INNER JOIN FoodType ON Food.FoodTypeId = FoodType.Id " +
                $"WHERE Food.Id IN ({foodIds})", cancellationToken: cancellationToken));

            if (result == null)
            {
                throw new Exception("Price not found");
            }
            return result.ToList();
        }

        public async Task<int> UpdateOrderItemsAsync(OrderItems orderItem, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var result = await con.ExecuteAsync(new CommandDefinition("" +
                "UPDATE OrderItems " +
                "SET FoodId = @FoodId " +
                "WHERE Id = @Id", parameters: new { orderItem.FoodId, orderItem.Id }, cancellationToken: cancellationToken));
            return Convert.ToInt32(result);
        }
    }
}
