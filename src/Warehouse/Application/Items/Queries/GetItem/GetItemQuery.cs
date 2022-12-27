using MediatR;

namespace Application.Items.Queries.GetItem;

public record GetItemQuery(Guid Id) : IRequest<GetItemQueryResult>;