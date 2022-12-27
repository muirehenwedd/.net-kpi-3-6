using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Items.Commands.RefillItem;

public sealed class RefillItemCommandResult : IMapFrom<Item>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}