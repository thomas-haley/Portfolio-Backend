using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Backend.DTOs;

public class RegisterDTO
{
    public required string Email {get; set;}
    public required string Username {get; set;}
    public required string Password {get; set;}
}
