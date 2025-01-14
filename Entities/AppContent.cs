using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using AutoMapper;
using Portfolio_Backend.DTOs;

namespace Portfolio_Backend.Entities;

[Table("content")]

public class AppContent
{
    [Key]
    public int Id {get; set;}
    public bool visible {get; set;} = true;
    public string? title {get; set;}
    public string? description {get; set;}
    public string? media {get; set;}
    public string mediaPosition {get; set;} = "left";


    public void UpdateFromDTO(ContentDTO content, IMapper mapper){
        AppContent mappedContent = mapper.Map<AppContent>(content);
        this.visible = mappedContent.visible;
        this.title = mappedContent.title;
        this.description = mappedContent.description;
        this.media = mappedContent.media;
        this.mediaPosition = mappedContent.mediaPosition;
    }
}