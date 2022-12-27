using Application.Common.Models;
using Application.Items.Commands.CreateItem;
using Application.Items.Commands.OrderItem;
using Application.Items.Commands.RefillItem;
using Application.Items.Queries.GetAllItems;
using Application.Items.Queries.GetItemsWithPagination;
using Microsoft.AspNetCore.Mvc;
using OrderItemDto = Application.Items.Queries.GetItemsWithPagination.OrderItemDto;

namespace WebApi.Controllers;

public sealed class ItemsController : ApiControllerBase
{
    [HttpGet("paginated")]
    public async Task<ActionResult<PaginatedList<OrderItemDto>>> GetItemsWithPagination(
        [FromQuery] GetItemsWithPaginationQuery query
    )
    {
        return await Mediator.Send(query);
    }

    [HttpGet("")]
    public async Task<ActionResult<GetAllItemsQueryResult>> GetAllItems()
    {
        return await Mediator.Send(new GetAllItemsQuery());
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateItemCommandResult>> CreateItem(CreateItemCommand command)
    {
        return await Mediator.Send(command);
    }

    public record OrderItemRequestBody(int Quantity);

    [HttpPut("{itemId:guid}/order")]
    public async Task<ActionResult<OrderItemCommandResult>> OrderItem(OrderItemRequestBody body, Guid itemId)
    {
        var command = new OrderItemCommand(itemId, body.Quantity);
        return await Mediator.Send(command);
    }

    public record RefillItemRequestBody(int Quantity);
    
    [HttpPut("{itemId:guid}/refill")]
    public async Task<ActionResult<RefillItemCommandResult>> RefillItem(RefillItemRequestBody body, Guid itemId)
    {
        var command = new RefillItemCommand(itemId, body.Quantity);

        return await Mediator.Send(command);
    }
}