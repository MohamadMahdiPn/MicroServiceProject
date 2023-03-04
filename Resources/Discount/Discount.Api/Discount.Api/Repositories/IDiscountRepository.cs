using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Discount.Api.Entities;

namespace Discount.Api.Repositories
{
    public interface IDiscountRepository
    {
        Task<Copon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Copon copon);
        Task<bool> UpdateDiscount(Copon copon);
        Task<bool> DeleteDiscount(string productName);
    }
}
