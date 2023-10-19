namespace WebAPI.Dtos
{
    public sealed class ErrorDto
    {
        public string Message { get; set; } = default!;
        public string? Description { get; set; } = default!;
    }
}
