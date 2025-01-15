using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Entities;

[Table("user")]
public class AppUser 
{
    public int Id { get; set; }
    public required string Username { get; set; }

    public required byte[] PasswordHash {get; set;}
    public required byte[] PasswordSalt {get; set;}

}