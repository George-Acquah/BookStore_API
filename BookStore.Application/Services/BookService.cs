using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public class BookService(IBooksRepository booksRepository, IBookCategoryRepository bookCategoryRepository): IBookService
    {
        private readonly IBooksRepository _bookRepository = booksRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository = bookCategoryRepository;

        public async Task<RepositoryResponse<string>> UploadBookAsync(AddBookDto bookDto)
        {
            var bookCategoryIds = new List<Guid>();
            foreach(var category in bookDto.Categories)
            {
                var bookCategory = await _bookCategoryRepository.GetBookCategoryByNameAsync(category);

                if(bookCategory.Success && bookCategory.Data != null)
                {
                    bookCategoryIds.Add(bookCategory.Data.Id);
                }
                else
                {
                    return RepositoryResponse<string>.FailureResult(bookCategory.ErrorMessage ?? "This Category does not exist in the databse");
                }
            }

            var book = new Book
            {
                Id = new Guid(),
                AddedById = new Guid(),
                CategoryIds = bookCategoryIds,
                Author = bookDto.Author,
                Title = bookDto.Title,
                Description = bookDto.Description,
                BookImgUrl = bookDto.BookImgUrl,
                FilePath = bookDto.FilePath,
                Price = bookDto.Price
            };

            return await _bookRepository.SaveBookAsync(book);
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            return await _bookRepository.GetAllBooksAsync(pageNumber, pageSize);
        }
    }
}
