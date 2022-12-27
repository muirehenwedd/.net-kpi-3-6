using Application.Items.Queries.GetItemsWithPagination;
using FluentValidation;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination;

public sealed class GetItemsWithPaginationQueryValidator : AbstractValidator<GetItemsWithPaginationQuery>
{
    public GetItemsWithPaginationQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(query => query.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}