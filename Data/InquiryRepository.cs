using System;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Attributes;
using Portfolio_Backend.Data;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public class InquiryRepository(DataContext context, ICustomLogger logger, IMapper mapper): IInquiryRepository
{
    public void Update(AppInquiry inquiry){
        context.Entry(inquiry).State = EntityState.Modified;
    }


    public async Task<string> CreateInquiryAsync(InquiryDTO inquiryDTO){
        DateTime uTimeNow = DateTime.Now.ToUniversalTime();
        inquiryDTO.CreatedDate = uTimeNow;
        inquiryDTO.Seen = false;
        AppInquiry newInquiry = mapper.Map<AppInquiry>(inquiryDTO);

        await context.Inquiry.AddAsync(newInquiry);


        return "complete";
    }

    public async Task<string> UpdateInquiryByIDAsync(int id, InquiryDTO inquiryDTO){

        //Find inquiry with matching ID to DTO
        AppInquiry? updateInquiry = await context.Inquiry.FirstAsync(inq => inq.Id == id);

        if(updateInquiry == null)
        {
            return "not-found";
        }

        //If DTO and db are identical, nothing to update
        AppInquiry inqClone = mapper.Map<AppInquiry>(inquiryDTO);
        if(inqClone == updateInquiry){
            return "bad-request";
        }

        //Loop over entity for UpdateAllow props to update
        foreach(PropertyInfo prop in typeof(AppInquiry).GetProperties())
        {
            if(prop.GetCustomAttribute(typeof(UpdateAllowAttribute)) != null)
            {
                prop.SetValue(updateInquiry, prop.GetValue(inqClone));
            }
        }

        return "complete";
    }


    public async Task<string> RemoveInquiryByIDAsync(int id)
    {
        AppInquiry? rmvInquiry = await context.Inquiry.SingleAsync(inq => inq.Id == id);   

        if(rmvInquiry == null){
            return "not-found";
        }

        context.Inquiry.Remove(rmvInquiry);

        return "complete";
    }
    
    public async Task<List<InquiryDTO>> GetAllInquiryListAsync()
    {
        List<AppInquiry> inqList = await context.Inquiry.ToListAsync();

        List<InquiryDTO> dtoList = [];


        foreach(AppInquiry inq in inqList)
        {
            dtoList.Add(mapper.Map<InquiryDTO>(inq));
        }


        return dtoList;
    }


    public async Task<InquiryDTO?> GetInquiryByIDAsync(int id)
    {
        AppInquiry dbInquiry = await context.Inquiry.SingleAsync(inq => inq.Id == id);

        if(dbInquiry == null){
            return null;
        }

        return mapper.Map<InquiryDTO>(dbInquiry);
    }

}