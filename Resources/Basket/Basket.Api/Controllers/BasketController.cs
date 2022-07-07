using System.Net;
using System.Threading.Tasks;
using Basket.Api.Entities;
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

        public BasketController(IBasketRepositories basketRepositories)
        {
            _basketRepositories = basketRepositories;
        }

        #endregion


        [HttpGet("{userName}" , Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCard) , (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> GetBasket(string userName)
        {
            var basket = await _basketRepositories.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCard(userName));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCard), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> UpdateBasket([FromBody] ShoppingCard basket)
        {
            return Ok(await _basketRepositories.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name="DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepositories.DeleteBasket(userName);
            return Ok();
        }










    }
}
