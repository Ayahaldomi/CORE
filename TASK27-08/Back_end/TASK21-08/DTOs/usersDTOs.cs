using System.ComponentModel.DataAnnotations;

namespace TASK21_08.DTOs
{
    public class usersDTOs
    {


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;
    }
}
