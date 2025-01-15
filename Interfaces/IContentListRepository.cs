using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentListRepository
{
    void Update(AppContentList content);

    public Task<bool> CreateContentList(ContentListDTO content);

    public Task<bool> UpdateContentList(int id, ContentListDTO content);
    public Task<string> RemoveContentListByIDAsync(int id);
    public Task<ContentListDTO?> GetContentListByIDAsync(int id);
    public Task<Object?> GetContentListFieldByIDAsync(int id, string field);
    public Task<bool> SetContentListFieldByIDAsync(int id, string field, object value);

}