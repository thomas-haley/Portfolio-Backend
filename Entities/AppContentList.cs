using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Portfolio_Backend.DTOs;

namespace Portfolio_Backend.Entities;

[Table("content_list")]

public class AppContentList
{
    [Key]
    public int Id {get; set;}
    public required string Tag {get; set;}


    public void UpdateFromDTO(ContentListDTO contentList, IMapper mapper){
        AppContentList mappedContent = mapper.Map<AppContentList>(contentList);
        this.Tag = mappedContent.Tag;
    }
}