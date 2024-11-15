using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.DB;
using BookStore.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository(BookStoreAPIDbContext dbContext, ILogger<BookRepository> logger): IBooksRepository
    {
        private readonly BookStoreAPIDbContext _dbContext = dbContext;
        private readonly ILogger<BookRepository> _logger = logger;

        public async Task<RepositoryResponse<IBookResponseDto>> GetBookByIdAsync(string bookId)
        {
            try
            {
                var book = await _dbContext.Books.Include(u => u.Categories).Include(u => u.AddedBy).Select(r => new BookResponseDto
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
                    Categories = r.Categories != null ? r.Categories.Select(c => c.Name).ToList() : new List<string>()
                }).Cast<IBookResponseDto>().FirstOrDefaultAsync(u => u.Id == bookId);
                return book != null
                    ? RepositoryResponse<IBookResponseDto>.SuccessResult(book)
                    : RepositoryResponse<IBookResponseDto>.FailureResult("Book not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book by ID.");
                return RepositoryResponse<IBookResponseDto>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<Book>> GetBookByIdRawAsync(Guid bookId)
        {
            try
            {
              var result = await _dbContext.Books
            .Include(b => b.Categories)
            .FirstOrDefaultAsync(b => b.Id == bookId);

                return RepositoryResponse<Book>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book by ID.");
                return RepositoryResponse<Book>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IBookResponseDto>> GetBookByTitleAsync(string title)
        {
            try
            {
                var book = await _dbContext.Books.Include(u => u.Categories).Include(u => u.AddedBy).Select(r => new BookResponseDto
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
                    Categories = r.Categories != null ? r.Categories.Select(c => c.Name).ToList() : new List<string>()
                }).Cast<IBookResponseDto>().FirstOrDefaultAsync(u => u.Title == title);
                return book != null
                    ? RepositoryResponse<IBookResponseDto>.SuccessResult(book)
                    : RepositoryResponse<IBookResponseDto>.FailureResult($"Book with title as {title} does not exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Book by title.");
                return RepositoryResponse<IBookResponseDto>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> SaveBookAsync(Book book)
        {
            try
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<string>.SuccessResult($"Book with title {book.Title} added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new book.");
                return RepositoryResponse<string>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> UpdateBookAsync(Book book)
        {
            try
            {
                _dbContext.Books.Update(book);
                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<string>.SuccessResult($"Book with title {book.Title} has been updated successfully");
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
                var query = _dbContext.Books
                                .Include(b => b.Categories)
                                .Include(b => b.AddedBy)
                                .AsQueryable();


                if (filter != null)
                {
                    query = query.Where(filter);
                }

                var projectedQuery = query.Select(r => new BookResponseDto
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
                    Categories = r.Categories != null ? r.Categories.Select(c => c.Name).ToList() : new List<string>()
                }).Cast<IBookResponseDto>();

                var paginatedBooks= await PaginationHelper.GetPaginatedResultAsync(projectedQuery, pageNumber, pageSize);

                return RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.SuccessResult(paginatedBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books.");
                return RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetBooksByCategoryAsync(int pageNumber, int pageSize, Expression<Func<Book, bool>> filter)
        {
            try
            {
                var query = _dbContext.Books
                                .Include(b => b.Categories)
                                .Include(b => b.AddedBy)
                                .AsQueryable();

                    query = query.Where(filter);

                var projectedQuery = query.Select(r => new BookResponseDto
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
                    Categories = r.Categories != null ? r.Categories.Select(c => c.Name).ToList() : new List<string>()
                }).Cast<IBookResponseDto>();

                var paginatedBooks = await PaginationHelper.GetPaginatedResultAsync(projectedQuery, pageNumber, pageSize);

                return paginatedBooks.TotalItems > 0
                    ? RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.SuccessResult(paginatedBooks)
                    : RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.FailureResult("No book with this category was found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books with this category.");
                return RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>.FailureResult(ex.Message);
            }
        }
    }
}
