using Application.Common.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetAllItems;

public sealed class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, GetAllItemsQueryResult>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllItemsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<GetAllItemsQueryResult> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        return new GetAllItemsQueryResult
        {
            Items = await _dbContext.Items
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}