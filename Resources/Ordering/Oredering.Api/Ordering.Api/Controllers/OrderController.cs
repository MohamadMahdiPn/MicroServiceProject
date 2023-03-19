using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Feutures.Orders.Commands.CheckoutCommand;
using Ordering.Application.Feutures.Orders.Commands.DeleteOrder;
using Ordering.Application.Feutures.Orders.Commands.UpdateOrder;
using Ordering.Application.Feutures.Orders.Queries.GetOrderslist;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Constructor
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;

        }

        #endregion

        #region Get All Orders
        [HttpGet("{userName}", Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrderByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);

        }
        #endregion

        #region Checkout order
        [HttpPost(Name ="CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion


        //#region Update order
        //[HttpPost(Name = "UpdateOrder")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return NoContent();
        //}
        //#endregion
        #region update order

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Delete order
        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand() { Id = id});
            return NoContent();
        }
        #endregion
    }
}
