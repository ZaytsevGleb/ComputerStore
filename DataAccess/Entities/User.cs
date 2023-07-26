namespace DataAccess.Entities;

public sealed class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime EditedDate { get; set; }
}
