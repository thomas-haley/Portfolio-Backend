using System;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
    
}
