using Application.Common.Abstractions;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Items.EventHandlers;

public sealed class ItemOrderedEventHandler : INotificationHandler<ItemOrderedEvent>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ItemOrderedEventHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task Handle(ItemOrderedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.CountBeforeOrder >= 0 && notification.Item.Count < 0)
        {
            var queryItem = new DeliveryQueryItem
            {
                Item = notification.Item,
                RequestTimestamp = _dateTimeProvider.UtcNow
            };

            _dbContext.DeliveryQueryItems.Add(queryItem);
        }

        return Task.CompletedTask;
    }
}