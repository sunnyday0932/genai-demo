using GenAiDemo.Domain.Entities;

namespace GenAiDemo.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
}
