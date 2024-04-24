namespace Domain.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public float Price { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
    }
}