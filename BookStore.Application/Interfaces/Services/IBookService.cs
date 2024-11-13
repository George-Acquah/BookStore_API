using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<RepositoryResponse<string>> UploadBookAsync(AddBookDto bookDto);
        //Task<RepositoryResponse<IPaginationMetaDto<IBook>>> UpdateBookAsync(Guid bookId);
        //Task<RepositoryResponse<IBook>> GetBookByIdAsync(string bookId);
        //Task<RepositoryResponse<IBook>> GetBookByTitleAsync(string title);
    }
}
