using System;
using Portfolio_Backend.DTOs;
using Portfolio_Backend.Entities;

namespace Portfolio_Backend.Interfaces;

public interface IInquiryRepository
{
    void Update(AppInquiry inquiry);

    public Task<string> CreateInquiryAsync(InquiryDTO inquiryDTO);

    public Task<string> UpdateInquiryByIDAsync(int id, InquiryDTO inquiryDTO);

    public Task<string> RemoveInquiryByIDAsync(int id);

    public Task<List<InquiryDTO>> GetAllInquiryListAsync();

    public Task<InquiryDTO?> GetInquiryByIDAsync(int id);
}