using GenAiDemo.Application.Dtos;
using GenAiDemo.Domain.Entities;
using GenAiDemo.Domain.Repositories;
using MediatR;

namespace GenAiDemo.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orders;
    private readonly IMediator _mediator;

    public OrderService(IOrderRepository orders, IMediator mediator)
    {
        _orders = orders;
        _mediator = mediator;
    }

    public async Task<OrderId> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken = default)
    {
        var items = dto.Items.Select(i => new OrderItem(i.ProductId, i.Quantity));
        var order = Order.Create(items);
        await _orders.AddAsync(order, cancellationToken);
        foreach (var e in order.Events)
        {
            await _mediator.Publish(e, cancellationToken);
        }
        order.ClearEvents();
        return order.Id;
    }
}
