using System.Threading.Tasks;
using Discount.Api.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection _connection;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
        }



        public async Task<Copon> GetDiscount(string productName)
        {
            
            var copon = await _connection.QueryFirstOrDefaultAsync<Copon>(
                "SELECT * FROM copon WHERE ProductName = @ProductName", new { ProductName = productName });
            if (copon == null)
                return new Copon() {ProductName = "NO Discount", Amount = 0, Description = "Noting Found", Id = 0};
return copon;

        }

        public async Task<bool> CreateDiscount(Copon copon)
        {
            var affected = await _connection.ExecuteAsync(
                "INSERT INTO copon(ProductName,Description,amount) VALUES (@ProductName,@Description,@amount);", new { ProductName = copon.ProductName, Description =copon.Description, amount = copon.Amount});

            if (affected == 0) 
                return false;
            return true;

        }

        public async Task<bool> UpdateDiscount(Copon copon)
        {
            var affected = await _connection.ExecuteAsync(
                "UPDATE copon SET ProductName = @ProductName,Description = @Description, amount = @amount  WHERE Id = @Id);", new {Id = copon.Id ,  ProductName = copon.ProductName, Description = copon.Description, amount = copon.Amount });

            if (affected == 0)
                return false;
            return true;

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var affected = await _connection.ExecuteAsync(
                "DELETE FROM copon WHERE ProductName = @ProductName);", new { ProductName = productName });

            if (affected == 0)
                return false;
            return true;
        }
    }
}
