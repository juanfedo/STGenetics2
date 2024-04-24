namespace Domain.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public string Type { get; set; } = null!;
    }
}
