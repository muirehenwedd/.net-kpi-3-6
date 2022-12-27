using MediatR;

namespace Application.Items.Commands.OrderItem;

public sealed record OrderItemCommand(Guid ItemId, int Quantity) : IRequest<OrderItemCommandResult>;