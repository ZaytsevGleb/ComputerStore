namespace ComputerStore.Services.OrderService.WebAPI.Entities
{
    public class Order : EntityBase
    {
        public Guid UserId { get; set; }
        public int MyProperty { get; set; }
        public List<Guid> ProductsId { get; set; } = new List<Guid>();
    }
}
