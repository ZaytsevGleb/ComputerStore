using ComputerStore.Services.CS.Shared.Enums;

namespace ComputerStore.Services.CS.DataAccess.Entities;

public sealed class Product : BaseEntity
{
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public ProductType Type { get; set; }
}
