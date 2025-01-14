using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio_Backend.Data;

public class ContentListContentRepository(DataContext context, ICustomLogger logger) : IContentListContentRepository
{
    public void Update(AppContentListContent channel){
        context.Entry(channel).State = EntityState.Modified;
    }

    public async Task<string> CreateRecordAsync(ContentListContentDTO clcDTO){
        //Load content and content list records
        logger.LogToTerminal("getting content", 4);
        AppContent? content = await context.Content.FirstOrDefaultAsync<AppContent>(x => x.Id == clcDTO.Content.Id);
        logger.LogToTerminal("getting content list", 4);
        AppContentList? contentList = await context.ContentList.FirstOrDefaultAsync<AppContentList>(x => x.Id == clcDTO.ContentList.Id);

        //Check if content is already bound to content list
        AppContentListContent? boundContent = await context.ContentListContent.FirstOrDefaultAsync<AppContentListContent>(clc => clc.content.Id == clcDTO.Content.Id && clc.content_list.Id == clcDTO.ContentList.Id);
        logger.LogToTerminal("getting bound content", 4);
        //Cancel operation if either content or list are null, or if record already exists
        if(content == null || contentList == null || boundContent != null)
        {
            if(boundContent != null){
                return "bad-request";
            }
            return "not-found";
        }

        AppContentListContent contentRef = new AppContentListContent {
            content = content,
            content_list = contentList,
            order = clcDTO.Order ?? 0
        };

        await context.ContentListContent.AddAsync(contentRef);

        return "complete";
    }  
}