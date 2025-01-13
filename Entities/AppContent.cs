using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Entities;

[Table("content")]

public class AppContent
{
    public int Id {get; set;}
    public bool visible {get; set;} = true;
    public string? title {get; set;}
    public string? description {get; set;}
    public string? media {get; set;}
    public string mediaPosition {get; set;} = "left";
}