using MediatR;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQuery;

public record GetDeliveryQueryQuery() : IRequest<DeliveryQueryDto>;