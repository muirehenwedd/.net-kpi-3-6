using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.OrderItem;

public sealed class OrderItemCommandHandler : IRequestHandler<OrderItemCommand, OrderItemCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderItemCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderItemCommandResult> Handle(OrderItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item is null)
            throw new NotFoundException("item", request.ItemId);
        
        item.Order(request.Quantity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderItemCommandResult>(item);
    }
}