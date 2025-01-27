using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController(IUnitOfWork unitOfWork, ICustomLogger logger, IMapper mapper): ControllerBase
{
    [Authorize]
    [HttpPut("create")]
    public async Task<ActionResult<bool>> CreateContent(ContentDTO content)
    {
        AppContent results = await unitOfWork.ContentRepository.CreateContent(content);
        if(results != null){
            await unitOfWork.Complete();
        }
        return Ok(mapper.Map<ContentDTO>(results));
    }
    [Authorize]
    [HttpPatch("{id:int}/{field}")]
    public async Task<ActionResult<bool>> SetContentFieldByIDAsync(int id, string field){
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        bool results = await unitOfWork.ContentRepository.SetContentFieldByIDAsync(id, field, body);
        
        if(results)
        {
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<bool>> SetContentByIDAsync(int id, ContentDTO content){
        bool results = await unitOfWork.ContentRepository.UpdateContent(id, content);
        if(results)
        {
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> RemoveContentByIDAsync(int id)
    {
        string result = await unitOfWork.ContentRepository.RemoveContentByIDAsync(id);
        //Save Changes if result is complete
        if(result == "complete"){
            await unitOfWork.Complete();
        }
        return Ok(result == "complete");
    }

    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult<List<ContentDTO>>> GetAllContentAsync(){
        List<AppContent> results = await unitOfWork.ContentRepository.GetAllContent();

        List<ContentDTO> dtoList = [];

        foreach (AppContent content in results){
            dtoList.Add(mapper.Map<ContentDTO>(content));
        }
        
        return Ok(dtoList);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContentDTO?>> GetContentByIDAsync(int id){
        AppContent? results = await unitOfWork.ContentRepository.GetContentByIDAsync(id);
        if(results != null){
            return Ok(mapper.Map<ContentDTO>(results));
        }
        return NotFound();
    }

    [HttpGet("{id:int}/{field}")]
    public async Task<ActionResult<Object?>> GetContentFieldByIDAsync(int id, string field){
        Object? results = await unitOfWork.ContentRepository.GetContentFieldByIDAsync(id, field);
        if(results != null){
            return Ok(results);
        }
        return NotFound();
    }



    

}