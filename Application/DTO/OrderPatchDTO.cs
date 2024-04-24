using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public  class OrderPatchDTO
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public List<OrderItemPatchDTO> Items { get; set; } = new List<OrderItemPatchDTO>();
    }
}
