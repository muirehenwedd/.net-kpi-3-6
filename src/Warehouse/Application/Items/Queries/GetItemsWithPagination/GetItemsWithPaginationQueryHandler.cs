using Application.Common.Abstractions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Items.Queries.GetItemsWithPagination;

public sealed class
    GetItemsWithPaginationQueryHandler : IRequestHandler<GetItemsWithPaginationQuery, PaginatedList<OrderItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetItemsWithPaginationQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<OrderItemDto>> Handle(
        GetItemsWithPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Items
            .OrderBy(i => i.Name)
            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}