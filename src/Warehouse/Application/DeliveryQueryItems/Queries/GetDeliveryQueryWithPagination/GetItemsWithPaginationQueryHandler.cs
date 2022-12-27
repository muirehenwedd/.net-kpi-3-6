using Application.Common.Abstractions;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DeliveryQueryItems.Queries.GetDeliveryQueryWithPagination;

public sealed class GetDeliveryQueryWithPaginationQueryHandler
    : IRequestHandler<GetDeliveryQueryWithPaginationQuery, PaginatedList<DeliveryQueryItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetDeliveryQueryWithPaginationQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<DeliveryQueryItemDto>> Handle(
        GetDeliveryQueryWithPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.DeliveryQueryItems
            .Include(i => i.Item)
            .OrderByDescending(i => i.RequestTimestamp)
            .ProjectTo<DeliveryQueryItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}