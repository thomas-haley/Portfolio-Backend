using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentListRepository
{
    void Update(AppContentList content);

    public Task<bool> CreateContentList(ContentListDTO content);

    public Task<bool> UpdateContentList(string tag, ContentListDTO content);
    public Task<string> RemoveContentListByTagAsync(string tag);
    public Task<ContentListDTO?> GetContentListByTagAsync(string tag);
    public Task<Object?> GetContentListFieldByTagAsync(string tag, string field);
    public Task<bool> SetContentListFieldByTagAsync(string tag, string field, object value);

}