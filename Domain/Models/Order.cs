namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
