using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Data;

public class ContentRepository(DataContext context) : IContentRepository
{
    public void Update(AppContent channel){
        context.Entry(channel).State = EntityState.Modified;
    }

    public async Task<AppContent?> GetContentByIDAsync(int id){
        AppContent? test = await context.Content.FindAsync(id);
        return test;
    }
}