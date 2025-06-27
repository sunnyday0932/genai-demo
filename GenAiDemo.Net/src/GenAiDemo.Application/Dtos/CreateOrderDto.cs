namespace GenAiDemo.Application.Dtos;

public record CreateOrderItemDto(Guid ProductId, int Quantity);
public record CreateOrderDto(List<CreateOrderItemDto> Items);
