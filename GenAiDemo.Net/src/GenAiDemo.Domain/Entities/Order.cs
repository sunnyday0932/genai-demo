using MediatR;
using GenAiDemo.Domain.Events;

namespace GenAiDemo.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();
    private readonly List<INotification> _events = new();

    public OrderId Id { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<INotification> Events => _events.AsReadOnly();

    private Order(OrderId id)
    {
        Id = id;
    }

    public static Order Create(IEnumerable<OrderItem> items)
    {
        var order = new Order(OrderId.New());
        order._items.AddRange(items);
        order.AddEvent(new OrderCreatedEvent(order));
        return order;
    }

    private void AddEvent(INotification eventItem) => _events.Add(eventItem);
    public void ClearEvents() => _events.Clear();
}
