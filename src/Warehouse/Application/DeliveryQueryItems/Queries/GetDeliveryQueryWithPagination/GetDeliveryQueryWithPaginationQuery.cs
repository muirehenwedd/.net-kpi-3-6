using Application.Common.Models;
using MediatR;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination;

public record GetDeliveryQueryWithPaginationQuery(int PageNumber, int PageSize)
    : IRequest<PaginatedList<DeliveryQueryItemDto>>;