using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Portfolio_Backend.Data;

public class ContentListContentRepository(DataContext context, ICustomLogger logger, IMapper mapper) : IContentListContentRepository
{
    public void Update(AppContentListContent channel){
        context.Entry(channel).State = EntityState.Modified;
    }

    public async Task<string> CreateRecordAsync(ContentListContentDTO clcDTO){
        //Load content and content list records
        AppContent? content = await context.Content.FirstOrDefaultAsync<AppContent>(x => x.Id == clcDTO.Content.Id);
        AppContentList? contentList = await context.ContentList.FirstOrDefaultAsync<AppContentList>(x => (x.Id == clcDTO.ContentList.Id) || (x.Tag == clcDTO.ContentList.Tag));

        //Check if content is already bound to content list
        AppContentListContent? boundContent = await context.ContentListContent.FirstOrDefaultAsync<AppContentListContent>(clc => clc.content.Id == clcDTO.Content.Id && clc.content_list.Id == clcDTO.ContentList.Id);
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

    public async Task<string> UpdateRecordAsync(ContentListContentDTO clcDTO)
    {

        //Load record to get db refs to ContentList and Content
        AppContentListContent? boundContent = await context.ContentListContent.Include(clc => clc.content).Include(clc => clc.content_list).FirstOrDefaultAsync<AppContentListContent>(clc => clc.Id == clcDTO.Id);
        if(boundContent == null){
            return "not-found";
        }

        //Check that connection of content_list/content doesn't already exist in database
        AppContentListContent? matchingBinding = await context.ContentListContent.Include(clc => clc.content).Include(clc => clc.content_list).FirstOrDefaultAsync<AppContentListContent>(clc => clc.content.Id == clcDTO.Content.Id && clc.content_list.Id == clcDTO.ContentList.Id && clc.Id != clcDTO.Id);   

        if(matchingBinding != null){
            return "bad-request";
        }

        logger.LogToTerminal(boundContent.content, 4);
        //If no updates to record are required, return bad request
        if(boundContent.content_list.Id == clcDTO.ContentList.Id && boundContent.content.Id == clcDTO.Content.Id && boundContent.order == clcDTO.Order)
        {
            return "bad-request";
        }

        //Update content list reference if changed
        if(boundContent.content_list.Id != clcDTO.ContentList.Id)
        {
            //Load new content list to bind content to
            AppContentList? updatedList = await context.ContentList.FirstOrDefaultAsync<AppContentList>(cl => cl.Id == clcDTO.ContentList.Id);
            if(updatedList == null){
                return "not-found";
            }

            boundContent.content_list = updatedList;
        }

        //Update content reference if changed
        if(boundContent.content.Id != clcDTO.Content.Id)
        {
            //Load new content
            AppContent? updatedContent = await context.Content.FirstOrDefaultAsync<AppContent>(content => content.Id == clcDTO.Content.Id);
            if(updatedContent == null){
                return "not-found";
            }

            boundContent.content = updatedContent;
        }        

        boundContent.order = clcDTO.Order ?? 0;

        return "complete";

    }


        public async Task<string> DeleteRecordAsync(ContentListContentDTO clcDTO)
        {
            //Load record to get db refs to ContentList and Content
            AppContentListContent? boundContent = await context.ContentListContent.Include(clc => clc.content).Include(clc => clc.content_list).FirstOrDefaultAsync<AppContentListContent>(clc => clc.content.Id == clcDTO.Content.Id && clc.content_list.Tag == clcDTO.ContentList.Tag);
            if(boundContent == null){
                return "not-found";
            }


            context.ContentListContent.Remove(boundContent);

            return "complete";
        }

        public async Task<ContentDTO[]?> LoadRecordsForListAsync(ContentListDTO clDTO)
        {   
            AppContent[]? boundContent = await context.ContentListContent.Include(clc => clc.content).Include(clc => clc.content_list).Where(clc => clc.content_list.Id == clDTO.Id).OrderBy(clc => clc.order).Select(cl => cl.content).ToArrayAsync();

            List<ContentDTO> contentDTOs = [];

            foreach(AppContent bound in boundContent)
            {
                    contentDTOs.Add(mapper.Map<ContentDTO>(bound));
            }
            return contentDTOs.ToArray();
        }
}