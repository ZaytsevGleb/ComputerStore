using Shared.Enums;

namespace DataAccess.Entities;

public sealed class Product : BaseEntity
{
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public ProductType Type { get; set; }
}
