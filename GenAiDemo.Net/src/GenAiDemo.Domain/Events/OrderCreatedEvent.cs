using MediatR;
using GenAiDemo.Domain.Entities;

namespace GenAiDemo.Domain.Events;

public record OrderCreatedEvent(Order Order) : INotification;
