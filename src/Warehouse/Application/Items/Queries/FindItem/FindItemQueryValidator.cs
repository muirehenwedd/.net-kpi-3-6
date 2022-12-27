using FluentValidation;

namespace Application.Items.Queries.FindItem;

public sealed class FindItemQueryValidator : AbstractValidator<FindItemQuery>
{
    public FindItemQueryValidator()
    {
        RuleFor(query => query.Name)
            .MinimumLength(5)
            .MaximumLength(50)
            .NotEmpty();
    }
}