using Microsoft.AspNetCore.Mvc;
using Portfolio_Backend.Interfaces;
using System.Linq;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Portfolio_Backend.Controllers;
public class BaseController(IUnitOfWork unitOfWork): ControllerBase
{


    public async Task<ActionResult<GenericResponseDTO?>> FinializeResult(string result)
    {
        GenericResponseDTO responseDTO = new GenericResponseDTO();
        responseDTO.Message = result;
        switch (result){
            case ("complete"):
                await unitOfWork.Complete();
                return Ok(responseDTO);
            case ("not-found"):
                return NotFound(responseDTO);
            case ("bad-request"):
                return BadRequest(responseDTO);
            case ("conflict"):
                return Conflict(responseDTO);
            default:
                return Problem();
        }
    }

}