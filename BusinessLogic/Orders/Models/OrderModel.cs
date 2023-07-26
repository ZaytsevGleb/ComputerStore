using BusinessLogic.Products.Models;
using DataAccess.Entities;

namespace BusinessLogic.Orders.Models;

public sealed class OrderModel
{
    public Guid Id { get; init; }
    public Guid UserID { get; init; }
    public OrderStatus Status { get; set; }
    public List<ProductModel> Products { get; set; } = new();
    public DateTime CreatedDate { get; init; }
    public DateTime? ModifiedDate { get; set; }
}
