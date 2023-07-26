using DataAccess.Entities;

namespace WebAPI.Dtos;

public sealed class OrderDto
{
    public Guid? Id { get; init; }
    public Guid UserID { get; init; }
    public OrderStatus Status { get; set; }
    public List<ProductDto> Products { get; init; } = new();
    public DateTime CreatedDate { get; init; }
}
