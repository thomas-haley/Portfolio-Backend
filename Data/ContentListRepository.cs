using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.DTOs;
using AutoMapper;
using System.Reflection;

namespace Portfolio_Backend.Data;

public class ContentListRepository(DataContext context, IMapper mapper) : IContentListRepository
{
    public void Update(AppContentList channel){
        context.Entry(channel).State = EntityState.Modified;
    }



    public async Task<bool> CreateContentList(ContentListDTO contentList)
    {
        AppContentList appContentList = mapper.Map<AppContentList>(contentList);

        var entity = await context.ContentList.AddAsync(appContentList);
        
        return true;
    }

    public async Task<bool> UpdateContentList(int id, ContentListDTO content)
    {
        AppContentList? contentListToUpdate = await context.ContentList.FindAsync(id);
        if(contentListToUpdate == null){
            return false;
        }

        contentListToUpdate.UpdateFromDTO(content, mapper);

        return true;
    }

    public async Task<string> RemoveContentListByIDAsync(int id)
    {
        AppContentList? contentList = await context.ContentList.FindAsync(id);
        if(contentList == null){
            return "notfound";
        }

        context.ContentList.Remove(contentList);
        return "complete";
    }
    

    public async Task<ContentListDTO?> GetContentListByIDAsync(int id){
        AppContentList? contentList = await context.ContentList.FindAsync(id);
        if(contentList == null){
            return null;
        }
        return mapper.Map<ContentListDTO>(contentList);
    }

    public async Task<Object?> GetContentListFieldByIDAsync(int id, string field)
    {
        AppContentList? contentList = await context.ContentList.FindAsync(id);
        PropertyInfo? searchField = typeof(AppContentList).GetProperty(field);
        if(searchField == null || contentList == null){
            return null;
        }
        return searchField.GetValue(contentList);
    }

    public async Task<bool> SetContentListFieldByIDAsync(int id, string field, object value){

        AppContentList? contentList = await context.ContentList.FindAsync(id);
        PropertyInfo? setField = typeof(AppContentList).GetProperty(field);

        if(contentList == null || setField == null)
        {
            return false;
        }

        Type fieldType = Nullable.GetUnderlyingType(setField.PropertyType) ?? setField.PropertyType;
        
        object? castedValue = Convert.ChangeType(value, fieldType);

        setField.SetValue(contentList,  castedValue);
        return true;
    }

}