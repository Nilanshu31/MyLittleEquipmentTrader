using System.ComponentModel.DataAnnotations;

namespace MyLittleEquipmentTrader.Application.Models.DTOs
{
    public class RegisterDto
    {
        [Required] public string UserName { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required] public string Password { get; set; } = "";
        // optional role (Admin/User). In real apps restrict who can create Admins.
        public string Role { get; set; } = "User";
    }
}
