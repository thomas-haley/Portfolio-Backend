using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio_Backend.Data;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentListController(IUnitOfWork unitOfWork, ICustomLogger logger, IMapper mapper): BaseController(unitOfWork)
{
    [Authorize]
    [HttpPut("create")]
    public async Task<ActionResult<bool>> CreateContentList(ContentListDTO contentList)
    {
        bool results = await unitOfWork.ContentListRepository.CreateContentList(contentList);
        logger.LogToTerminal("test 2", 4);
        logger.LogToTerminal(results, 4);
        if(results){
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [Authorize]
    [HttpPatch("{id:int}/{field}")]
    public async Task<ActionResult<bool>> SetContentListFieldByIDAsync(int id, string field){
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        bool results = await unitOfWork.ContentListRepository.SetContentListFieldByIDAsync(id, field, body);
        
        if(results)
        {
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<bool>> SetContentListByIDAsync(int id, ContentListDTO contentList){
        bool results = await unitOfWork.ContentListRepository.UpdateContentList(id, contentList);
        if(results)
        {
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> RemoveContentListByIDAsync(int id)
    {
        string result = await unitOfWork.ContentListRepository.RemoveContentListByIDAsync(id);
        //Save Changes if result is complete
        if(result == "complete"){
            await unitOfWork.Complete();
        }
        return Ok(result == "complete");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContentListDTO?>> GetContentListByIDAsync(int id){
        ContentListDTO? results = await unitOfWork.ContentListRepository.GetContentListByIDAsync(id);
        if(results != null){
            results.Content = await unitOfWork.ContentListContentRepository.LoadRecordsForListAsync(results);
            return Ok(results);
        }
        return NotFound();
    }

    [HttpGet("{id:int}/{field}")]
    public async Task<ActionResult<Object?>> GetContentListFieldByIDAsync(int id, string field){
        Object? results = await unitOfWork.ContentListRepository.GetContentListFieldByIDAsync(id, field);
        if(results != null){
            return Ok(results);
        }
        return NotFound();
    }
    [Authorize]
    [HttpPut("content/add-content")]
    public async Task<ActionResult> PutListContent(ContentListContentDTO clcDTO)
    {
        string result = await unitOfWork.ContentListContentRepository.CreateRecordAsync(clcDTO);
        return await this.FinializeResult(result);
    }
    [Authorize]
    [HttpPatch("content/patch-content")]
    public async Task<ActionResult> PatchListContent(ContentListContentDTO clcDTO)
    {
        string result = await unitOfWork.ContentListContentRepository.UpdateRecordAsync(clcDTO);
        return await this.FinializeResult(result);
    }
    [Authorize]
    [HttpDelete("content/delete-content")]
    public async Task<ActionResult> DeleteListContent(ContentListContentDTO clcDTO)
    {
        string result = await unitOfWork.ContentListContentRepository.DeleteRecordAsync(clcDTO);
        return await this.FinializeResult(result);
    }


    [HttpGet("content/{id:int}")]
    public async Task<ActionResult<ContentListDTO?>> GetListContent(int id)
    {
        //Load content list container
        ContentListDTO? contentListDTO = await unitOfWork.ContentListRepository.GetContentListByIDAsync(id);
        if(contentListDTO == null){
            return NotFound();
        }

        //Fill content list container with bound content
        contentListDTO.Content = await unitOfWork.ContentListContentRepository.LoadRecordsForListAsync(contentListDTO);
        if(contentListDTO.Content == null){
            return NotFound();
        }
        return Ok(contentListDTO);
    }

}