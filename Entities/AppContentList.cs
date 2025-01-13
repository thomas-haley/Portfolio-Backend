using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Entities;

[Table("content_list")]

public class AppContentList
{
    public int Id {get; set;}
    public required string tag {get; set;}

}