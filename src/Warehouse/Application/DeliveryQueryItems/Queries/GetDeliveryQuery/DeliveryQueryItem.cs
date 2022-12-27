using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQuery;

public sealed class DeliveryQueryItem : IMapFrom<Domain.Entities.DeliveryQueryItem>
{
    public string ItemName { get; set; }
    public int RequiredQuantity { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.DeliveryQueryItem, DeliveryQueryItem>()
            .ForMember(
                d => d.ItemName,
                opt => opt.MapFrom(s => s.Item.Name));
    }
}