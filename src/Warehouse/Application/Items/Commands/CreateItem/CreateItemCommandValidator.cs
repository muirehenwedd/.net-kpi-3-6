using FluentValidation;

namespace Application.Items.Commands.CreateItem;

public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(command => command.Name)
            .MinimumLength(5)
            .MaximumLength(50)
            .NotEmpty();
    }
}