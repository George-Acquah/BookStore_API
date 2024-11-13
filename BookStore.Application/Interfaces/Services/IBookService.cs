using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<RepositoryResponse<IPaginationMetaDto<Book>>> GetAllBooksAsync(int page, int pageSize);
        Task<RepositoryResponse<string>> UploadBookAsync(Book book);
        //Task<RepositoryResponse<IPaginationMetaDto<IBook>>> UpdateBookAsync(Guid bookId);
        Task<RepositoryResponse<IBook>> GetBookByIdAsync(string bookId);
        Task<RepositoryResponse<IBook>> GetBookByTitleAsync(string title);
    }
}
