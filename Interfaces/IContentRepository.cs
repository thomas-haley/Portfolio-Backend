using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentRepository
{
    void Update(AppContent content);

    public Task<bool> CreateContent(ContentDTO content);

    public Task<AppContent?> GetContentByIDAsync(int id);
    public Task<Object?> GetContentFieldByIDAsync(int id, string field);

    public Task<bool> SetContentFieldByIDAsync(int id, string field, object value);
}