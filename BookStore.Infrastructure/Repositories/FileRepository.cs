using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BookStore.Infrastructure.Repositories
{
    public class FileRepository: IFilesRepository
    {
        public async Task<IFileResponseDto> SaveFileAsync(IFormFile file, string destinationPath)
        {
            try
            {
                // Create unique file name to avoid conflicts
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var fullPath = Path.Combine(destinationPath, uniqueFileName);

                // Save the file to the local path
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new FileResponseDto
                {
                    Success = true,
                    FilePath = fullPath
                };
            }
            catch (Exception ex)
            {
                return new FileResponseDto
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
