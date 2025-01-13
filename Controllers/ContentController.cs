using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController(IUnitOfWork unitOfWork): ControllerBase
{
    [HttpGet("test")]
    public async Task<ActionResult<AppContent?>> Test(){
        AppContent? results = await unitOfWork.ContentRepository.GetContentByIDAsync(2);
        return Ok(results);
    }


}