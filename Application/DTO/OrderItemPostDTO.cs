using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class OrderItemPostDTO
    {
        [Required]
        public int FoodId { get; set; }
    }
}
