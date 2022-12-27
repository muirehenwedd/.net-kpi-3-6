namespace Application.DeliveryQueryItems.Queries.GetDeliveryQuery;

public sealed class DeliveryQueryDto
{
    public DeliveryQueryDto()
    {
        DeliveryQuery = new List<DeliveryQueryItem>();
    }

    public IList<DeliveryQueryItem> DeliveryQuery { get; set; }
}