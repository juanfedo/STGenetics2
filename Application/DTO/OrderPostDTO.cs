using Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public  class OrderPostDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [ValidateDuplicateItems, ValidateItemsCountInOrder]
        public List<OrderItemPostDTO> Items { get; set; } = new List<OrderItemPostDTO>();
    }
}
