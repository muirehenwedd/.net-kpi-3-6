using Domain.Common;

namespace Domain.Entities;

public sealed class DeliveryQueryItem : BaseEntity
{
    public Item Item { get; set; } = null!;
    public Guid ItemId { get; set; }
    public DateTime RequestTimestamp { get; set; }

    public int RequiredQuantity => 0 - Item.Count;
}