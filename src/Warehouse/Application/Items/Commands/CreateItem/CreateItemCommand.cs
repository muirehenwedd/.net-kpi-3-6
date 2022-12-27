using MediatR;

namespace Application.Items.Commands.CreateItem;

public sealed record CreateItemCommand(string Name) : IRequest<CreateItemCommandResult>;