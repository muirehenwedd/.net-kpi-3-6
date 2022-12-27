using MediatR;

namespace Application.Items.Queries.GetAllItems;

public sealed record GetAllItemsQuery() : IRequest<GetAllItemsQueryResult>;