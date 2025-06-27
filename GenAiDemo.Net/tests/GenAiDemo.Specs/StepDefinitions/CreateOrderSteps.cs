using GenAiDemo.Application.Dtos;
using GenAiDemo.Application.Services;
using GenAiDemo.Domain.Entities;
using GenAiDemo.Domain.Repositories;
using MediatR;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace GenAiDemo.Specs.StepDefinitions;

[Binding]
public class CreateOrderSteps
{
    private CreateOrderDto _dto = null!;
    private OrderId _result;

    [Given("an order item")]
    public void GivenAnOrderItem()
    {
        _dto = new CreateOrderDto(new List<CreateOrderItemDto>{new(Guid.NewGuid(),1)});
    }

    [When("the order is created")]
    public async Task WhenTheOrderIsCreated()
    {
        var repo = new Mock<IOrderRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        var mediator = new Mock<IMediator>();
        var service = new OrderService(repo.Object, mediator.Object);
        _result = await service.CreateOrderAsync(_dto);
    }

    [Then("the result should contain an id")]
    public void ThenTheResultShouldContainAnId()
    {
        Assert.NotEqual(default, _result);
    }
}
