using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination;

public sealed class DeliveryQueryItemDto : IMapFrom<DeliveryQueryItem>
{
    public string ItemName { get; set; }
    public int RequiredQuantity { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeliveryQueryItem, DeliveryQueryItemDto>()
            .ForMember(
                d => d.ItemName,
                opt => opt.MapFrom(s => s.Item.Name));
    }
}