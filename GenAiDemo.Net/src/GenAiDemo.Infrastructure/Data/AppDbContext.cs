using GenAiDemo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GenAiDemo.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Order> Orders => Set<Order>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var domainEntities = ChangeTracker.Entries<Order>()
            .Select(e => e.Entity)
            .ToList();

        foreach (var entity in domainEntities)
        {
            foreach (var domainEvent in entity.Events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
            entity.ClearEvents();
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().OwnsMany(o => o.Items);
    }
}
