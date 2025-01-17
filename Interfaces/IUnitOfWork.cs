using System;

namespace Portfolio_Backend.Interfaces;

public interface IUnitOfWork
{
    IContentRepository ContentRepository {get;}
    IContentListRepository ContentListRepository {get;}
    IContentListContentRepository ContentListContentRepository {get;}
    public IUserRepository UserRepository {get;}
    Task<bool> Complete();
    bool HasChanges();
}
