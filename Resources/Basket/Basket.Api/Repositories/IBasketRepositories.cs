using System.Threading.Tasks;
using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepositories
    {
        Task<ShoppingCard> GetUserBasket(string userName);
        Task<ShoppingCard> UpdateBasket(ShoppingCard basket);
        Task DeleteBasket(string userName);



    }
}
