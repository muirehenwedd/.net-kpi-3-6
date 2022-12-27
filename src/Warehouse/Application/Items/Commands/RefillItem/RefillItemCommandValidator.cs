using Application.Items.Commands.OrderItem;
using FluentValidation;

namespace Application.Items.Commands.RefillItem;

public sealed class OrderItemCommandValidator : AbstractValidator<OrderItemCommand>
{
    public OrderItemCommandValidator()
    {
        RuleFor(command => command.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"{nameof(OrderItemCommand.Quantity)} at least greater than or equal to 1.");
    }
}