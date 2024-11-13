using BookStore.Application.Common;
using BookStore.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<IRepositoryResponse<IFileResponseDto>> UploadFileAsync(IFormFile formFile);
    }
}
