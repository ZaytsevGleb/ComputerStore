namespace DataAccess.Entities;

public sealed class Order : IEntity
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public List<Product>? Products { get; set; } = new();
}

public enum OrderStatus
{
    Added,
    Paided,
    OnTheWay,
    Completed
}
