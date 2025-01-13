using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Entities;

[Table("content_list_x_content")]

public class AppContentListContent
{
    public int Id {get; set;}
    public required AppContentList content_list {get; set;}
    public required AppContent content {get; set;}
    public int order {get; set;} = 0;
}