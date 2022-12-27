using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItem;

public sealed class GetItemQueryHandler : IRequestHandler<GetItemQuery, GetItemQueryResult>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetItemQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<GetItemQueryResult> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (item is null)
            throw new NotFoundException("item", request.Id);

        return _mapper.Map<GetItemQueryResult>(item);
    }
}