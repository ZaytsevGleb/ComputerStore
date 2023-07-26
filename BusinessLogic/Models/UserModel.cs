namespace BusinessLogic.Models
{
    public sealed class UserModel
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public DateTime CreatedDate { get; init; }
        public DateTime EditedDate { get; set; }
    }
}
