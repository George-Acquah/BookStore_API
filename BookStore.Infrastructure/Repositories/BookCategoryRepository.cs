using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.DB;
using BookStore.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Repositories
{
    public class BookCategoryRepository(BookStoreAPIDbContext bookStoreAPIDbContext, ILogger<BookCategoryRepository> logger): IBookCategoryRepository
    {
        private readonly BookStoreAPIDbContext _dbContext = bookStoreAPIDbContext;
        private readonly ILogger<BookCategoryRepository> _logger = logger;

        public async Task<RepositoryResponse<IBookCategory>> GetBookCategoryByIdAsync(Guid bookCategoryId)
        {
            try
            {
                var bookCategory = await _dbContext.BookCategories.FirstOrDefaultAsync(u => u.Id == bookCategoryId);
                return bookCategory != null
                    ? RepositoryResponse<IBookCategory>.SuccessResult(bookCategory)
                    : RepositoryResponse<IBookCategory>.FailureResult("Book Category not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book category by ID.");
                return RepositoryResponse<IBookCategory>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IBookCategory>> GetBookCategoryByNameAsync(string categoryName)
        {
            try
            {
                var bookCategory = await _dbContext.BookCategories.FirstOrDefaultAsync(u => u.Name == categoryName);
                return bookCategory != null
                    ? RepositoryResponse<IBookCategory>.SuccessResult(bookCategory)
                    : RepositoryResponse<IBookCategory>.FailureResult("Book category not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Book category by title.");
                return RepositoryResponse<IBookCategory>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> AddBookCategoryAsync(BookCategory bookCategory)
        {
            try
            {
                await _dbContext.BookCategories.AddAsync(bookCategory);
                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<string>.SuccessResult("Book Category added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new book category.");
                return RepositoryResponse<string>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<string>>> GetAllBookCategoriesAsync(int pageNumber, int pageSize, Expression<Func<BookCategory, bool>>? filter = null)
        {
            try
            {
                var query = _dbContext.BookCategories.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                var projectedQuery = query.Select(c => c.Name);

                var paginatedBookCategories = await PaginationHelper.GetPaginatedResultAsync(projectedQuery, pageNumber, pageSize);

                return RepositoryResponse<IPaginationMetaDto<string>>.SuccessResult(paginatedBookCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book categories.");
                return RepositoryResponse<IPaginationMetaDto<string>>.FailureResult(ex.Message);
            }
        }
    }
}
