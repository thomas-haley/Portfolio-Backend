using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Entities;

[Table("content_list")]

public class AppContentList
{
    [Key]
    public int Id {get; set;}
    public required string Tag {get; set;}

}