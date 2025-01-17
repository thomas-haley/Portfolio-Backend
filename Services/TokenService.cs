using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Portfolio_Backend.Services;

public class TokenService(IConfiguration config) : ITokenService
{

    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
        if (tokenKey.Length < 64) throw new Exception("Token key less than required 64 character minimum.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
