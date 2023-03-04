using System.Net;
using System.Threading.Tasks;
using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;

        }

        [HttpGet("{productName}" , Name = "GetDiscount")]
        [ProducesResponseType(typeof(Copon),(int) HttpStatusCode.OK)]
        public async Task<ActionResult<Copon>> GetDiscount(string productName)
        {
            var copon = await _discountRepository.GetDiscount(productName);
            return copon;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Copon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Copon>> CreateDiscount([FromBody] Copon copon)
        {
            await _discountRepository.CreateDiscount(copon);
            return CreatedAtRoute("GetDiscount",new { productName = copon.ProductName} , copon);
        }



        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Copon>> UpdateDiscount([FromBody] Copon copon)
        {
            return Ok(await _discountRepository.UpdateDiscount(copon));
        }

        [HttpDelete("{productName}" )]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Copon>> DeleteDiscount(string productName)
        {
            return Ok(await _discountRepository.DeleteDiscount(productName));
        }
    }
}
