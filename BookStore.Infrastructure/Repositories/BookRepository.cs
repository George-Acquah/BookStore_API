using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.DB;
using BookStore.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Linq;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository(BookStoreAPIDbContext dbContext, ILogger<BookRepository> logger): IBooksRepository
    {
        private readonly BookStoreAPIDbContext _dbContext = dbContext;
        private readonly ILogger<BookRepository> _logger = logger;

        public async Task<RepositoryResponse<IBook>> GetBookByIdAsync(Guid bookId)
        {
            try
            {
                var book = await _dbContext.Books.Include(u => u.Categories).FirstOrDefaultAsync(u => u.Id == bookId);
                return book != null
                    ? RepositoryResponse<IBook>.SuccessResult(book)
                    : RepositoryResponse<IBook>.FailureResult("Book not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book by ID.");
                return RepositoryResponse<IBook>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IBook>> GetBookByTitleAsync(string title)
        {
            try
            {
                var book = await _dbContext.Books.Include(u => u.Categories).FirstOrDefaultAsync(u => u.Title == title);
                return book != null
                    ? RepositoryResponse<IBook>.SuccessResult(book)
                    : RepositoryResponse<IBook>.FailureResult("Book not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Book by title.");
                return RepositoryResponse<IBook>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> SaveBookAsync(Book book)
        {
            try
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<string>.SuccessResult("Book added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new book.");
                return RepositoryResponse<string>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int pageNumber, int pageSize, Expression<Func<Book, bool>>? filter = null)
        {
            try
            {
                var query = _dbContext.Books.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                var projectedQuery = query.Include(b => b.Categories).Select(r => new BookResponseDto
                {
                    Id = r.Id.ToString(),
                    Title = r.Title,
                    AddedBy = r.AddedBy != null ? r.AddedBy.UserName : "",
                    Author = r.Author,
                    Description = r.Description,
                    Price = r.Price,
                    BookFilePath = r.FilePath,
                    BookImgUrl = r.BookImgUrl,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Categories = (List<string>)(r.Categories != null ? r.Categories.Select(c => c.Name) : new List<string>()),
            }).Cast<IBookResponseDto>();

                var paginatedBooks= await PaginationHelper.GetPaginatedResultAsync(projectedQuery, pageNumber, pageSize);

                return RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.SuccessResult(paginatedBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users.");
                return RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.FailureResult(ex.Message);
            }
        }
    }
}
