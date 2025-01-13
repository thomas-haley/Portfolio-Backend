using System;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentListContentRepository
{
    void Update(AppContentListContent content);
}