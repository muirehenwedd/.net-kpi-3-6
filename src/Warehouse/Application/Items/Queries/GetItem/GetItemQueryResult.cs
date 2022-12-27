using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Queries.GetItem;

public sealed class GetItemQueryResult : IMapFrom<Item>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}