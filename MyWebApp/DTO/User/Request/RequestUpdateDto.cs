using MyWebApp.Shared;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MyWebApp.DTO.User.Request
{
    public class RequestUpdateDto
    {
        [Required]
        public int UserId { get; set; }

        [MinLength(5), MaxLength(100)]
        public string FullName { get; set; }

        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexEmail, ErrorMessage = "Email Not Valid!")]
        public string Email { get; set; }
    }
}
