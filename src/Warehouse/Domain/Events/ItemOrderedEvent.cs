using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public sealed class ItemOrderedEvent : BaseEvent
{
    public ItemOrderedEvent(Item item, int countBeforeOrder)
    {
        Item = item;
        CountBeforeOrder = countBeforeOrder;
    }

    public Item Item { get; }
    public int CountBeforeOrder { get; }
}