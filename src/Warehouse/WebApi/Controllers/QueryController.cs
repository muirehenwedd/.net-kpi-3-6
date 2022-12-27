using Application.Common.Models;
using Application.DeliveryQueryItems.Queries.GetDeliveryQuery;
using Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination;
using Microsoft.AspNetCore.Mvc;
using DeliveryQueryItemDto = Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination.DeliveryQueryItemDto;

namespace WebApi.Controllers;

public sealed class QueryController : ApiControllerBase
{
    [HttpGet("paginated")]
    public async Task<ActionResult<PaginatedList<DeliveryQueryItemDto>>> GetItemsWithPagination(
        [FromQuery] GetDeliveryQueryWithPaginationQuery query
    )
    {
        return await Mediator.Send(query);
    }

    [HttpGet()]
    public async Task<ActionResult<DeliveryQueryDto>> GetItems()
    {
        return await Mediator.Send(new GetDeliveryQueryQuery());
    }
}