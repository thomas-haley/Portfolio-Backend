using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);

    public Task<AppUser?> CreateUser(AppUser newUser);

    public Task<AppUser?> GetByUsernameAsync(string username);
}