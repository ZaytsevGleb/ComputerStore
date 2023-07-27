/*using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
[Produces("application/json")]
public sealed class OrdersController : ControllerBase
{
    [HttpGet("{guid:id}", Name = "GetOrder")]
    [ProducesResponseType(Status200OK, Type = typeof(OrderDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<OrderDto>> GetOrderAsync(Guid id)
    {
        return Ok();
    }

    [HttpGet(Name = "GetOrders")]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<OrderDto>))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync([FromQuery] ProductSearchDto searchDto)
    {
        return Ok();
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(Status201Created, Type = typeof(OrderDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<OrderDto>> CreateOrderAsync(OrderDto dto)
    {
        return Created("", default);
    }

    [HttpPut("{guid:id}", Name = "UpdateOrder")]
    [ProducesResponseType(Status200OK, Type = typeof(OrderDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<OrderDto>> UpdateOrderAsync(Guid id, OrderDto dto)
    {
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> DeleteOrderAsync(Guid id)
    {
        return NoContent();
    }
}
*/