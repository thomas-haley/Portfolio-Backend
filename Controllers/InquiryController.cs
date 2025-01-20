using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Entities;
using Portfolio_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Humanizer;
namespace Portfolio_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class InquiryController(IUnitOfWork unitOfWork, ICustomLogger logger, IMapper mapper): BaseController(unitOfWork)
{
    [HttpPut("create")]
    public async Task<ActionResult> CreateInquiry(InquiryDTO inquiry)
    {
        string results = await unitOfWork.InquiryRepository.CreateInquiryAsync(inquiry);
        return await this.FinializeResult(results);
    }


    [Authorize]
    [HttpPatch("update/{id:int}")]
    public async Task<ActionResult> UpdateInquiry(int id, InquiryDTO inquiry)
    {
        string results = await unitOfWork.InquiryRepository.UpdateInquiryByIDAsync(id, inquiry);
        return await this.FinializeResult(results);
    }



    [Authorize]
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteInquiry(int id)
    {
        string results = await unitOfWork.InquiryRepository.RemoveInquiryByIDAsync(id);
        return await this.FinializeResult(results);
    }


    [Authorize]
    [HttpGet("list")]
    public async Task<ActionResult<List<InquiryDTO>>> ListInquiry()
    {
        List<InquiryDTO> results = await unitOfWork.InquiryRepository.GetAllInquiryListAsync();
        return Ok(results);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<InquiryDTO>>> GetInquiryDetails(int id)
    {
        InquiryDTO? results = await unitOfWork.InquiryRepository.GetInquiryByIDAsync(id);
        if(results == null){
            return NotFound();
        }
        return Ok(results);
    }

}