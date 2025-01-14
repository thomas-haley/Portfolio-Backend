using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentListController(IUnitOfWork unitOfWork, ICustomLogger logger, IMapper mapper): ControllerBase
{
    // [Authorize]
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

    // [Authorize]
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

    // [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<bool>> SetContentListByIDAsync(int id, ContentListDTO contentList){
        bool results = await unitOfWork.ContentListRepository.UpdateContentList(id, contentList);
        if(results)
        {
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    // [Authorize]
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
        AppContentList? results = await unitOfWork.ContentListRepository.GetContentListByIDAsync(id);
        if(results != null){
            return Ok(mapper.Map<ContentListDTO>(results));
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



    

}