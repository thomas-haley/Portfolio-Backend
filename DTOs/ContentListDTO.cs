namespace Portfolio_Backend.DTOs;

public class ContentListDTO
{
    public int Id {get; set;}

    public required string Tag {get; set;}
    public ContentDTO[]? Content {get; set;}
}