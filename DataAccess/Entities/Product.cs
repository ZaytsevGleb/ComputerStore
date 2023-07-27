namespace DataAccess.Entities;

public sealed class Product : BaseEntity
{
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public ProductType Type { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

public enum ProductType
{
    RAM,
    MotherBoard,
    CPU,
    SSD,
    HDD,
    GPU,
    Mouse,
    PSU,
    Case,
    KeyBoard,
    Speakers,
    Monitor
}

