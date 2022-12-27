using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public sealed class ItemRefiledEvent : BaseEvent
{
    public ItemRefiledEvent(Item item, int countBeforeRefill)
    {
        Item = item;
        CountBeforeRefill = countBeforeRefill;
    }

    public Item Item { get; }
    public int CountBeforeRefill { get; }
}