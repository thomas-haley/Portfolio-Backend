using System;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;

namespace Portfolio_Backend.Data;

public class UnitOfWork(DataContext context, IContentRepository contentRepository, IContentListRepository contentListRepository, IContentListContentRepository contentListContentRepository, IUserRepository userRepository, IInquiryRepository inquiryRepository) : IUnitOfWork
{
    public IContentRepository ContentRepository => contentRepository;
    public IContentListRepository ContentListRepository => contentListRepository;
    public IUserRepository UserRepository => userRepository;
    // public IVideoRepository VideoRepository => videoRepository;
    public IContentListContentRepository ContentListContentRepository => contentListContentRepository;

    public IInquiryRepository InquiryRepository => inquiryRepository;

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
