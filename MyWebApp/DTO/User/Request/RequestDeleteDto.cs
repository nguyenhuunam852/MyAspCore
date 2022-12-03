using System.ComponentModel.DataAnnotations;

namespace MyWebApp.DTO.User.Request
{
    public class RequestDeleteDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
