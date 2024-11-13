using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Services
{
    public class FileService: IFileService
    {
        private readonly string _saveFileLocation;
        private readonly IFilesRepository _filesRepository;

        public FileService(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
            _saveFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BookStoreFiles"); ;

            if(!Directory.Exists(_saveFileLocation))
            {
                Directory.CreateDirectory(_saveFileLocation);
            }
        }

        public async Task<IRepositoryResponse<IFileResponseDto>> UploadFileAsync(IFormFile file)
        {
            try
            {

            var result = await _filesRepository.SaveFileAsync(file, _saveFileLocation);

            return RepositoryResponse<IFileResponseDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return RepositoryResponse<IFileResponseDto>.FailureResult(ex.Message);
            }
        }
    }
}
