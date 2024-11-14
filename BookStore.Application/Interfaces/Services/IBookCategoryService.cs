using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces.Services
{
    public interface IBookCategoryService
    {
        Task<RepositoryResponse<string>> AddBookCategoryAsync(string categoryName);
        Task<RepositoryResponse<IPaginationMetaDto<string>>> GetAllBookCategoriesAsync(int pageNumber, int pageSize);
        Task<RepositoryResponse<string>> GetBookCategoryByIdAsync(string bookCategoryId);
        Task<RepositoryResponse<string>> GetBookCategoryByNameAsync(string categoryName);
    }
}
