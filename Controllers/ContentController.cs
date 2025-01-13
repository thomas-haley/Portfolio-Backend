using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController(IUnitOfWork unitOfWork, ICustomLogger logger): ControllerBase
{
    [HttpPut("create")]
    public async Task<ActionResult<bool>> CreateContent(ContentDTO content)
    {
        logger.LogToTerminal("test", 4);
        bool results = await unitOfWork.ContentRepository.CreateContent(content);
        logger.LogToTerminal("test 2", 4);
        logger.LogToTerminal(results, 4);
        if(results){
            await unitOfWork.Complete();
        }
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppContent?>> GetContentByIDAsync(int id){
        AppContent? results = await unitOfWork.ContentRepository.GetContentByIDAsync(id);
        return Ok(results);
    }

    [HttpGet("{id:int}/{field}")]
    public async Task<ActionResult<Object?>> GetContenFieldByIDAsync(int id, string field){
        Object? results = await unitOfWork.ContentRepository.GetContentFieldByIDAsync(id, field);
        return Ok(results);
    }

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

    

}