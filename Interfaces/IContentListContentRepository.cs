using System;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentListContentRepository
{
    void Update(AppContentListContent content);

    public Task<string> CreateRecordAsync(ContentListContentDTO clcDTO);
}