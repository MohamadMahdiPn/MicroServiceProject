using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon copon);
        Task<bool> UpdateDiscount(Coupon copon);
        Task<bool> DeleteDiscount(string productName);
    }
}
