using MyWebApp.Shared;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MyWebApp.DTO.User.Request
{
    public class RequestUserLoginDTO
    {
        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexUserName)]
        public string UserName { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(RegexValidation.RegexPassword)]
        public string UserPassword { get; set; }
    }
}
