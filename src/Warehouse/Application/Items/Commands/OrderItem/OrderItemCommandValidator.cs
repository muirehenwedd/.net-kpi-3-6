using FluentValidation;

namespace Application.Items.Commands.OrderItem;

public sealed class OrderItemCommandValidator : AbstractValidator<OrderItemCommand>
{
    public OrderItemCommandValidator()
    {
        RuleFor(command => command.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"{nameof(OrderItemCommand.Quantity)} at least greater than or equal to 1.");
    }
}