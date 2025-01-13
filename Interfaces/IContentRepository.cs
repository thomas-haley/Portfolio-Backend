using System;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentRepository
{
    void Update(AppContent content);



    public Task<AppContent?> GetContentByIDAsync(int id);
}