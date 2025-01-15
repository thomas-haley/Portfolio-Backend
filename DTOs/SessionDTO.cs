using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Backend.DTOs;

public class SessionDTO
{
    public required int Id {get; set;}
    public required string Username {get; set;}
    public required string Token {get; set;}
}
