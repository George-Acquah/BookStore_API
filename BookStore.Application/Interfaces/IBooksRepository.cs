using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces
{
    public interface IBooksRepository
    {
        Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int page, int pageSize, Expression<Func<Book, bool>>? filter = null);
        Task<RepositoryResponse<string>> SaveBookAsync(Book book);
        //Task<RepositoryResponse<IPaginationMetaDto<IBook>>> UpdateBookAsync(Guid bookId);
        Task<RepositoryResponse<IBookResponseDto>> GetBookByIdAsync(string bookId);

        Task<RepositoryResponse<Book>> GetBookByIdRawAsync(Guid bookId);
        Task<RepositoryResponse<IBookResponseDto>> GetBookByTitleAsync(string title);
        Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetBooksByCategoryAsync(int pageNumber, int pageSize, Expression<Func<Book, bool>> filter);
    }
}
