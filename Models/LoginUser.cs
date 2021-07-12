using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}