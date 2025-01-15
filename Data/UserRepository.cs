using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;

namespace Portfolio_Backend.Data;

public class UserRepository(DataContext context, IMapper mapper): IUserRepository
{
    public void Update(AppUser user){
        context.Entry(user).State = EntityState.Modified;
    }

    public async Task<AppUser?> CreateUser(AppUser newUser){
        var entity = context.User.Add(newUser);
        if(entity.IsKeySet){
            return newUser;
        }

        return null;
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Username == username);
    }


}