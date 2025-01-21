using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Portfolio_Backend.Attributes;

namespace Portfolio_Backend.Entities;

[Table("inquiry")]
public class AppInquiry 
{
    public int Id { get; set; }
    [UpdateAllow]
    public required string Name { get; set; }
    [UpdateAllow]
    public required string Email { get; set; }
    [UpdateAllow]
    public required string Type { get; set; }
    [UpdateAllow]
    public string? Comment { get; set; }

    public required DateTime CreatedDate {get; set;}
    [UpdateAllow]
    public required bool Seen {get; set;}
}