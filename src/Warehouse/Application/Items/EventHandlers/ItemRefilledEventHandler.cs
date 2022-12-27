using Application.Common.Abstractions;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.EventHandlers;

public sealed class ItemRefilledEventHandler : INotificationHandler<ItemRefiledEvent>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ItemRefilledEventHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(ItemRefiledEvent notification, CancellationToken cancellationToken)
    {
        if (notification.CountBeforeRefill < 0 && notification.Item.Count >= 0)
        {
            var queryItem = await _dbContext
                .DeliveryQueryItems
                .FirstAsync(
                    i => i.Item == notification.Item,
                    cancellationToken: cancellationToken);

            _dbContext.DeliveryQueryItems.Remove(queryItem);
        }
    }
}