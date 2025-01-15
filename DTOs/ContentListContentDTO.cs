namespace Portfolio_Backend.DTOs;

public class ContentListContentDTO
{
    public int Id {get; set;}

    public required ContentListDTO ContentList {get; set;}
    public required ContentDTO Content {get; set;}

    public int? Order {get; set;}
}