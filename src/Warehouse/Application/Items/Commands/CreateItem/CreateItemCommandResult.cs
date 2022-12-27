using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Commands.CreateItem;

public sealed class CreateItemCommandResult : IMapFrom<Item>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}