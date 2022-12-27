using MediatR;

namespace Application.Items.Queries.FindItem;

public record FindItemQuery(string Name) : IRequest<FindItemQueryResult>;