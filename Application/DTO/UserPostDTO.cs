using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class UserPostDTO
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
