using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string? _connectionString;

        public OrderRepository(IOptions<AppSettingsModel> settings)
        {
            _connectionString = settings.Value.LocalConnection;
        }

        public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var orders = await con.QueryAsync<Order>(new CommandDefinition("" +
                "SELECT Id, TotalPrice, UserId " +
                "FROM Orders ORDER BY 1", cancellationToken: cancellationToken));
            return orders.ToList();
        }

        public async Task<List<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var order = await con.QueryAsync<Order>(new CommandDefinition("" +
                "SELECT Id, TotalPrice, UserId " +
                "FROM Orders " +
                "WHERE Id = @id", parameters: new { id }, cancellationToken: cancellationToken));
            if (order == null)
            {
                throw new Exception("Order doesn´t exists");
            }
            return order.ToList();
        }

        public async Task<int> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var param = new DynamicParameters();
            param.Add("@TotalPrice", order.TotalPrice);
            param.Add("@UserId", order.UserId);
            param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var res = await con.ExecuteAsync(new CommandDefinition("SP_CreateOrder", param, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            var createID = param.Get<int>("@retID");

            return createID;
        }

        public async Task<int> DeleteOrderAsync(int id, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var order = await GetOrderByIdAsync(id, cancellationToken);
            var result = await con.ExecuteAsync(new CommandDefinition("" +
                "DELETE " +
                "FROM Orders " +
                "WHERE Id = @id", parameters: new { id }, cancellationToken: cancellationToken));
            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            using SqlConnection con = new(_connectionString);
            var orderInDb = await GetOrderByIdAsync(order.Id, cancellationToken);
            var result = await con.ExecuteAsync(new CommandDefinition("" +
                "UPDATE Orders " +
                "SET UserId = @UserId, TotalPrice = @TotalPrice " +
                "WHERE Id = @Id", parameters: new { order.UserId, order.TotalPrice, order.Id }, cancellationToken: cancellationToken));
            return Convert.ToInt32(result);
        }
    }
}
