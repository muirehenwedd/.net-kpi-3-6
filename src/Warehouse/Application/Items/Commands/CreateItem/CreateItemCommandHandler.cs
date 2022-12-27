using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.CreateItem;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateItemCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CreateItemCommandResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Items.AnyAsync(i => i.Name == request.Name, cancellationToken))
            throw new ConflictException("Item with such name already created.");

        var item = new Item {Name = request.Name};

        _dbContext.Items.Add(item);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateItemCommandResult>(item);
    }
}