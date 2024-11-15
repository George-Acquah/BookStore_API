using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<RepositoryResponse<string>> UploadBookAsync(AddBookDto bookDto, Guid addedById);
        //Task<RepositoryResponse<IPaginationMetaDto<string>>> UpdateBookAsync(Guid bookId);
        Task<RepositoryResponse<IBookResponseDto>> GetBookByIdAsync(string bookId);
        Task<RepositoryResponse<IBookResponseDto>> GetBookByTitleAsync(string title);
        Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetBooksByCategoryAsync(string category, int pageNumber, int pageSize);
    }
}
