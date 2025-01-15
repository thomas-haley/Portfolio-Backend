using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IUnitOfWork unitOfWork, ICustomLogger logger, IMapper mapper, ITokenService tokenService): ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<SessionDTO?>> Login(LoginDTO loginDTO)
    {
        AppUser? user = await unitOfWork.UserRepository.GetByUsernameAsync(loginDTO.Username.ToLower());
        if(user == null){
            return Unauthorized("Invalid username or password");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
        }

        SessionDTO session = mapper.Map<SessionDTO>(user);
        var token = tokenService.CreateToken(user);
        session.Token = token;
        return Ok(session);
    }

    [HttpPost("register")]
    public async Task<ActionResult<SessionDTO?>> Register(RegisterDTO registerDTO)
    {
        AppUser? user = await unitOfWork.UserRepository.GetByUsernameAsync(registerDTO.Username.ToLower());
        if(user != null){
            return BadRequest("Username already in use");
        }
        using var hmac = new HMACSHA512();
        
        var newUser = mapper.Map<AppUser>(registerDTO);
        newUser.Username = newUser.Username.ToLower();
        newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
        newUser.PasswordSalt = hmac.Key;
        
        var userCreated = unitOfWork.UserRepository.CreateUser(newUser);

        if(userCreated == null){
            return Problem("Unable to create account");
        }
        //Finalize user in db before generating token
        await unitOfWork.Complete();
        var token = tokenService.CreateToken(newUser);

        SessionDTO session = mapper.Map<SessionDTO>(newUser);
        session.Token = token;
        return Ok(session);
    }

}