using GenAiDemo.Application.Dtos;
using GenAiDemo.Domain.Entities;

namespace GenAiDemo.Application.Services;

public interface IOrderService
{
    Task<OrderId> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken = default);
}
