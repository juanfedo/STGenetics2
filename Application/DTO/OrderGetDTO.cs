namespace Application.DTO
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float TotalPrice { get; set; }
        public List<OrderItemGetDTO> Items { get; set; } = new List<OrderItemGetDTO>();
    }
}
