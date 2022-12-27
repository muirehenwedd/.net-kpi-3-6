using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Items.Commands.OrderItem;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.RefillItem;

public sealed class RefillItemCommandHandler : IRequestHandler<RefillItemCommand, RefillItemCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public RefillItemCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RefillItemCommandResult> Handle(RefillItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item is null)
            throw new NotFoundException("item", request.ItemId);
        
        item.Refill(request.Quantity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RefillItemCommandResult>(item);
    }
}