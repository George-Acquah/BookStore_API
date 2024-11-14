using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController(IBookCategoryService bookCategoryService) : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService = bookCategoryService;

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        {
            var result = await _bookCategoryService.AddBookCategoryAsync(categoryDto.CategoryName.ToUpper());

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Category added successfully",
                result.Data));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _bookCategoryService.GetAllBookCategoriesAsync(pageNumber, pageSize);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Category added successfully",
                result.Data));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(string categoryId)
        {
            var result = await _bookCategoryService.GetBookCategoryByIdAsync(categoryId);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Category added successfully",
                result.Data));
        }

        [HttpGet("category-name")]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string categoryName)
        {
            var result = await _bookCategoryService.GetBookCategoryByNameAsync(categoryName.Trim('"').ToUpper());

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Category added successfully",
                result.Data));
        }
    }
}
