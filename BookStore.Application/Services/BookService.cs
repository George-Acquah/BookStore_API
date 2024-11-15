using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Services
{
    public class BookService(IBooksRepository booksRepository, IBookCategoryRepository bookCategoryRepository): IBookService
    {
        private readonly IBooksRepository _bookRepository = booksRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository = bookCategoryRepository;

        public async Task<RepositoryResponse<string>> UploadBookAsync(AddBookDto bookDto, Guid addedById)
        {
            // Create a list to store BookCategory entities
            var bookCategories = new List<BookCategory>();

            // Retrieve and validate each category from the database
            foreach (var categoryName in bookDto.Categories)
            {
                var bookCategory = await _bookCategoryRepository.GetBookCategoryByNameAsync(categoryName);

                if (bookCategory.Success && bookCategory.Data != null)
                {
                    bookCategories.Add((BookCategory)bookCategory.Data);
                }
                else
                {
                    return RepositoryResponse<string>.FailureResult(bookCategory.ErrorMessage ?? "This category does not exist in the database");
                }
            }

            // Create the Book entity
            var book = new Book
            {
                Id = Guid.NewGuid(),
                AddedById = addedById,
                Author = bookDto.Author,
                Title = bookDto.Title,
                Description = bookDto.Description,
                BookImgUrl = bookDto.BookImgUrl,
                FilePath = bookDto.FilePath,
                Price = bookDto.Price,
                Categories = bookCategories
            };

            return await _bookRepository.SaveBookAsync(book);
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            return await _bookRepository.GetAllBooksAsync(pageNumber, pageSize);
        }

        public async Task<RepositoryResponse<IBookResponseDto>> GetBookByIdAsync(string bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }

        public async Task<RepositoryResponse<IBookResponseDto>> GetBookByTitleAsync(string title)
        {
            return await _bookRepository.GetBookByTitleAsync(title);
        }

        public async Task<RepositoryResponse<IPaginationMetaDto<IBookResponseDto>>> GetBooksByCategoryAsync(string category, int pageNumber, int pageSize)
        {
            Expression<Func<Book, bool>> filter = b => b.Categories!.Any(c => c.Name == category);
            return await _bookRepository.GetBooksByCategoryAsync(pageNumber, pageSize, filter);
        }
    }
}
