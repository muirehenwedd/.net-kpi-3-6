using MediatR;

namespace Application.Items.Commands.RefillItem;

public sealed record RefillItemCommand(Guid ItemId, int Quantity) : IRequest<RefillItemCommandResult>;