using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Portfolio_Backend.Controllers;
public class BaseController(IUnitOfWork unitOfWork): ControllerBase
{


    public async Task<ActionResult> FinializeResult(string result)
    {
        switch (result){
            case ("complete"):
                await unitOfWork.Complete();
                return Ok();
            case ("not-found"):
                return NotFound();
            case ("bad-request"):
                return BadRequest();
            default:
                return Problem();
        }
    }

}