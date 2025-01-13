using System;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IContentListRepository
{
    void Update(AppContentList content);
}