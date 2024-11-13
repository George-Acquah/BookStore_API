using BookStore.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Interfaces
{
    public interface IFilesRepository
    {
        Task<IFileResponseDto> SaveFileAsync(IFormFile formFile, string destinationPath);
    }
}
