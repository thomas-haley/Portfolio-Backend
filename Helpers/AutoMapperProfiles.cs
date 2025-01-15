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
        CreateMap<ContentDTO, AppContent>();
        CreateMap<AppContentList, ContentListDTO>();
        CreateMap<ContentListDTO, AppContentList>();
        CreateMap<AppUser, SessionDTO>();
        CreateMap<RegisterDTO, AppUser>();
    }
}
