using Application.Common.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQuery;

public sealed class GetDeliveryQueryQueryHandler : IRequestHandler<GetDeliveryQueryQuery, DeliveryQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetDeliveryQueryQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<DeliveryQueryDto> Handle(GetDeliveryQueryQuery request, CancellationToken cancellationToken)
    {
        return new DeliveryQueryDto
        {
            DeliveryQuery = await _dbContext.DeliveryQueryItems
                .AsNoTracking()
                .Include(i => i.Item)
                .OrderByDescending(i => i.RequestTimestamp)
                .ProjectTo<DeliveryQueryItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}