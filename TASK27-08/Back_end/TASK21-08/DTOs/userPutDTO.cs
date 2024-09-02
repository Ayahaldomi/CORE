using System.ComponentModel.DataAnnotations;
using TASK21_08.Models;

namespace TASK21_08.DTOs
{
    public class userPutDTO
    {

        public string Username { get; set; } = null!;

        public string OldPassword { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;




    }
}
