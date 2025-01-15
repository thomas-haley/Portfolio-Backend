using Portfolio_Backend.Entities;
using Portfolio_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.Internal;
using Portfolio_Backend.DTOs;
using AutoMapper;
using System.Reflection;
namespace Portfolio_Backend.Data;

public class ContentRepository(DataContext context, ICustomLogger logger, IMapper mapper) : IContentRepository
{
    public void Update(AppContent channel){
        context.Entry(channel).State = EntityState.Modified;
    }

    public async Task<bool> CreateContent(ContentDTO content)
    {
        AppContent appContent = mapper.Map<AppContent>(content);

        var entity = await context.Content.AddAsync(appContent);
        logger.LogToTerminal(entity, 4);
        
        return true;
    }

    public async Task<bool> UpdateContent(int id, ContentDTO content)
    {
        AppContent? contentToUpdate = await context.Content.FindAsync(id);
        if(contentToUpdate == null){
            return false;
        }

        contentToUpdate.UpdateFromDTO(content, mapper);

        return true;
    }

    public async Task<string> RemoveContentByIDAsync(int id)
    {
        AppContent? content = await context.Content.FindAsync(id);
        if(content == null){
            return "notfound";
        }

        context.Content.Remove(content);
        return "complete";
    }
    

    public async Task<AppContent?> GetContentByIDAsync(int id){
        AppContent? content = await context.Content.FindAsync(id);
        return content;
    }

    public async Task<Object?> GetContentFieldByIDAsync(int id, string field)
    {
        AppContent? content = await context.Content.FindAsync(id);
        PropertyInfo? searchField = typeof(AppContent).GetProperty(field);
        if(searchField == null || content == null){
            return null;
        }
        return searchField.GetValue(content);
    }

    public async Task<bool> SetContentFieldByIDAsync(int id, string field, object value){

        AppContent? content = await context.Content.FindAsync(id);
        PropertyInfo? setField = typeof(AppContent).GetProperty(field);

        if(content == null || setField == null)
        {
            logger.LogToTerminal("Content or setField null", 4);
            return false;
        }

        Type fieldType = Nullable.GetUnderlyingType(setField.PropertyType) ?? setField.PropertyType;
        
        object? castedValue = Convert.ChangeType(value, fieldType);

        setField.SetValue(content,  castedValue);
        return true;
    }
}