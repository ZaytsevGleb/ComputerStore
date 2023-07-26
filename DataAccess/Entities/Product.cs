namespace DataAccess.Entities;

public sealed class Product : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public int Amount { get; set; }
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

