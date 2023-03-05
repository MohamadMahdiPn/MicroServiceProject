using System.Threading.Tasks;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
namespace Discount.Grpc.Repositories
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



        public async Task<Coupon> GetDiscount(string productName)
        {
            
            var copon = await _connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM copon WHERE ProductName = @ProductName", new { ProductName = productName });
            if (copon == null)
                return new Coupon() {ProductName = "NO Discount", Amount = 0, Description = "Noting Found", Id = 0};
return copon;

        }

        public async Task<bool> CreateDiscount(Coupon copon)
        {
            var affected = await _connection.ExecuteAsync(
                "INSERT INTO copon(ProductName,Description,amount) VALUES (@ProductName,@Description,@amount);", new { ProductName = copon.ProductName, Description =copon.Description, amount = copon.Amount});

            if (affected == 0) 
                return false;
            return true;

        }

        public async Task<bool> UpdateDiscount(Coupon copon)
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
