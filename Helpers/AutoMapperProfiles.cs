using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppContent, ContentDTO>();
    }
}
