using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces
{
    public interface IBookCategoryRepository
    {
        Task<RepositoryResponse<string>> AddBookCategoryAsync(BookCategory bookCategory);
        Task<RepositoryResponse<IPaginationMetaDto<BookCategory>>> GetAllBookCategoriesAsync(int pageNumber, int pageSize, Expression<Func<BookCategory, bool>>? filter = null);
        Task<RepositoryResponse<IBookCategory>> GetBookCategoryByIdAsync(Guid bookCategoryId);
        Task<RepositoryResponse<IBookCategory>> GetBookCategoryByNameAsync(string categoryName);
    }
}
