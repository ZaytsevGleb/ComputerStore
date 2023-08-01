using Shared.Enums;

namespace BusinessLogic.Products.Models;

public sealed class ProductModel
{
    public Guid Id { get; init; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public ProductType Type { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime? ModifiedDate { get; set; }
}
