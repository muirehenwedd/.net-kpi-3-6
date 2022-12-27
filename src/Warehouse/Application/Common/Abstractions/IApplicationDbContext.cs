using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Item> Items { get; }
    DbSet<DeliveryQueryItem> DeliveryQueryItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}