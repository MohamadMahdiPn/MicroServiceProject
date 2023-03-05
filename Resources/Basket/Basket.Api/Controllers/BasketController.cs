using System.Net;
using System.Threading.Tasks;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region Constructor

        private readonly IBasketRepositories _basketRepositories;
        private readonly DiscountGrpcService _discountService;
        public BasketController(IBasketRepositories basketRepositories, DiscountGrpcService discountService)
        {
            _basketRepositories = basketRepositories;
            _discountService = discountService;
        }

        #endregion


        #region GetBasket
        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCard), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> GetBasket(string userName)
        {
            var basket = await _basketRepositories.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCard(userName));

        }
        #endregion

        #region Update Basket
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCard), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> UpdateBasket([FromBody] ShoppingCard basket)
        {
            //Add Grpc
            foreach (var item in basket.ShoppingCardItems)
            {
                var coupon = await _discountService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _basketRepositories.UpdateBasket(basket));
        }
        #endregion

        #region DeleteBasket
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepositories.DeleteBasket(userName);
            return Ok();
        }
        #endregion


    }
}
