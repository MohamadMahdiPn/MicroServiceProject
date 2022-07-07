using System.Threading.Tasks;
using Basket.Api.Entities;
using  Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepositories
    {
        private readonly IDistributedCache _redisCashe;

        public BasketRepository(IDistributedCache redisCashe)
        {
            _redisCashe = redisCashe;
        }
        public async Task<ShoppingCard> GetUserBasket(string userName)
        {
            var basket = await _redisCashe.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCard>(basket);
        }

        public async Task<ShoppingCard> UpdateBasket(ShoppingCard basket)
        {
            await _redisCashe.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetUserBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCashe.RemoveAsync(userName);
        }
    }
}
