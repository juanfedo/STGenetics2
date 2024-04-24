using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class OrderItemPatchDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int FoodId { get; set; }
    }
}
