using System.ComponentModel.DataAnnotations;

namespace MyNotes.Contracts.Users
{
    public record LoginUserRequest([Required]string Email, [Required]string Password);
}
