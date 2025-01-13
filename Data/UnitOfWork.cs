using System;
using Portfolio_Backend.Interfaces;

namespace Portfolio_Backend.Data;

public class UnitOfWork(DataContext context, IContentRepository contentRepository, IContentListRepository contentListRepository, IContentListContentRepository contentListContentRepository) : IUnitOfWork
{
    public IContentRepository ContentRepository => contentRepository;
    public IContentListRepository ContentListRepository => contentListRepository;
    // public IVideoRepository VideoRepository => videoRepository;
    public IContentListContentRepository ContentListContentRepository => contentListContentRepository;

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
