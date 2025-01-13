using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Data;

public class ContentListContentRepository(DataContext context) : IContentListContentRepository
{
    public void Update(AppContentListContent channel){
        context.Entry(channel).State = EntityState.Modified;
    }
}