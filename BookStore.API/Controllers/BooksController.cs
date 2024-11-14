using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _bookService.GetAllBooksAsync(pageNumber, pageSize);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Books retrieved successfully",
                result.Data));
        }

        [HttpPost("add-book")]
        [Authorize]
        public async Task<IActionResult> UploadABook(AddBookDto bookDto)
        {
            var useridClaim = User.FindFirst("UserId")?.Value;

            if (useridClaim == null || !Guid.TryParse(useridClaim, out Guid userId))
            {
                return Unauthorized(new BaseAPIResponse<string>(StatusCodes.Status401Unauthorized, "User ID claim is missing or invalid", null));
            }
            var result = await _bookService.UploadBookAsync(bookDto, userId);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Books retrieved successfully",
                result.Data));
        }
    }
}
