namespace GenAiDemo.Domain.Entities;

public class OrderItem
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public OrderItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
