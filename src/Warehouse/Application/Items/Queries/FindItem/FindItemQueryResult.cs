using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Queries.FindItem;

public sealed class FindItemQueryResult : IMapFrom<Item>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}