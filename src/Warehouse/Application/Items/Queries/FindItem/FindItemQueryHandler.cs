using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.FindItem;

public sealed class FindItemQueryHandler : IRequestHandler<FindItemQuery, FindItemQueryResult>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public FindItemQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<FindItemQueryResult> Handle(FindItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Name == request.Name, cancellationToken);

        if (item is null)
            throw new NotFoundException($"Item with name '{request.Name}' was not found.");

        return _mapper.Map<FindItemQueryResult>(item);
    }
}