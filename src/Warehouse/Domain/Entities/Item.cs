using Domain.Common;
using Domain.Events;

namespace Domain.Entities;

public sealed class Item : BaseEntity
{
    public required string Name { get; set; }
    public int Count { get; set; }
    public DeliveryQueryItem? DeliveryQueryItem { get; set; }

    public void Order(int takenCount)
    {
        if (takenCount <= 0)
            throw new ArgumentOutOfRangeException();

        var countBefore = Count;
        Count -= takenCount;

        AddDomainEvent(new ItemOrderedEvent(this, countBefore));
    }

    public void Refill(int refillCount)
    {
        if (refillCount <= 0)
            throw new ArgumentOutOfRangeException();

        var countBefore = Count;
        Count += refillCount;

        AddDomainEvent(new ItemRefiledEvent(this, countBefore));
    }
}