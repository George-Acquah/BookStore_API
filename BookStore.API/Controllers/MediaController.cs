using BookStore.Application.Common;
using BookStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController(IFileService fileService) : ControllerBase
    {
        private readonly IFileService _fileService = fileService;
        private const int MaxFilesAllowed = 3;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            Console.WriteLine(files);
            if (files == null || files.Count == 0)
            return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status500InternalServerError,
                    "File is empty or not provided.",
                    null));

            if (files.Count > MaxFilesAllowed)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    $"Maximum of {MaxFilesAllowed} files can be uploaded.",
                    null));
            }

            foreach (var file in files)
            {
                if (file.Name != "files")
                {
                    return BadRequest(new BaseAPIResponse<string>(
                        StatusCodes.Status400BadRequest,
                        "Check the name of each file in your Form Data. The key must be 'files'.",
                        null));
                }

                // Additional validation can go here (e.g., file size, file type)
                if (file.Length == 0)
                {
                    return BadRequest(new BaseAPIResponse<string>(
                        StatusCodes.Status400BadRequest,
                        "One of the provided files is empty.",
                        null));
                }
            }

            // Process each file asynchronously
            var uploadResults = new List<string>();
            foreach (var file in files)
            {
                var localResult = await _fileService.UploadFileAsync(file);

                if (!localResult.Success)
                {
                    return BadRequest(new BaseAPIResponse<string>(
                        StatusCodes.Status500InternalServerError,
                        localResult.ErrorMessage,
                        null));
                }

                // Collect the file paths of successfully uploaded files
                uploadResults.Add(localResult?.Data?.FilePath ?? "");
            }

            // Return success with the list of file paths
            return Ok(new BaseAPIResponse<List<string>>(
                StatusCodes.Status200OK,
                "Files uploaded successfully!",
                uploadResults));
        }



    }
}
