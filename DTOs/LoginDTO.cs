using System.ComponentModel.DataAnnotations;

namespace Portfolio_Backend.DTOs;

public class LoginDTO
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

}
