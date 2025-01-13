namespace Portfolio_Backend.Entities;

public class AppUser 
{
    public int Id { get; set; }
    public required string Username { get; set; }

    public required byte[] PasswordHash {get; set;}
    public required byte[] PasswordSalt {get; set;}

    public bool DataUploaded {get; set;} = false;
    public bool AllowUpload {get; set;} = true;
}