using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces
{
    public interface IBooksRepository
    {
        Task<RepositoryResponse<IPaginationMetaDto<Book>>> GetAllBooksAsync(int page, int pageSize, Expression<Func<Book, bool>>? filter = null);
        Task<RepositoryResponse<string>> SaveBookAsync(Book book);
        //Task<RepositoryResponse<IPaginationMetaDto<IBook>>> UpdateBookAsync(Guid bookId);
        Task<RepositoryResponse<IBook>> GetBookByIdAsync(Guid bookId);
        Task<RepositoryResponse<IBook>> GetBookByTitleAsync(string title);
    }
}
