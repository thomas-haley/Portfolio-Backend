using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Data;

public class ContentListRepository(DataContext context) : IContentListRepository
{
    public void Update(AppContentList channel){
        context.Entry(channel).State = EntityState.Modified;
    }
}