using Application.Common.Models;
using MediatR;

namespace Application.Items.Queries.GetItemsWithPagination;

public record GetItemsWithPaginationQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<OrderItemDto>>;