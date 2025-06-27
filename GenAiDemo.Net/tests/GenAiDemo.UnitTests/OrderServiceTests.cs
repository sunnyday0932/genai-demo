using GenAiDemo.Application.Dtos;
using GenAiDemo.Application.Services;
using GenAiDemo.Domain.Entities;
using GenAiDemo.Domain.Repositories;
using Moq;
using MediatR;

namespace GenAiDemo.UnitTests;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrder_Returns_Id()
    {
        var repo = new Mock<IOrderRepository>();
        var mediator = new Mock<IMediator>();
        var service = new OrderService(repo.Object, mediator.Object);

        var dto = new CreateOrderDto(new List<CreateOrderItemDto>{new(Guid.NewGuid(),1)});
        var id = await service.CreateOrderAsync(dto);

        repo.Verify(r => r.AddAsync(It.IsAny<Order>(), default), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<INotification>(), default), Times.AtLeastOnce);
        Assert.NotEqual(default, id);
    }
}
