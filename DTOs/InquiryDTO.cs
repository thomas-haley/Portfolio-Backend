namespace Portfolio_Backend.DTOs;

public class InquiryDTO
{
    public int? Id {get; set;}

    public required string Name {get; set;}
    public required string Email {get; set;}
    public required string Type {get; set;}
    public string? Comment {get; set;}
    public DateTime? CreatedDate {get; set;}
    public bool Seen {get; set;} = false;
}