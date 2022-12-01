#nullable disable

using MyWebApp;
using MyWebApp.Shared;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.DTO.User.Request
{
    public class RequestRegisterUserDTO
    {
        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexUserName ,ErrorMessage = "Username Not Valid!")]
        public string UserName { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexPassword , ErrorMessage = "Password Not Valid!")]
        public string UserPassword { get; set; }

        [MinLength(5), MaxLength(100)]
        public string FullName { get; set; }

        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexEmail, ErrorMessage = "Email Not Valid!")]
        public string Email { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexPassword, ErrorMessage = "Confirm Password Not Valid!")]
        public string ConfirmPassword { get; set; }
    }
}
