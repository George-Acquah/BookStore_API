using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Services
{
    public class BookCategoryService(IBookCategoryRepository bookCategoryRepository): IBookCategoryService
    {
        private readonly IBookCategoryRepository _bookCategoryRepository = bookCategoryRepository;

        public async Task<RepositoryResponse<string>> AddBookCategoryAsync(string categoryName)
        {
            // Check if the category already exists to prevent duplicates
            var existingCategory = await _bookCategoryRepository.GetBookCategoryByNameAsync(categoryName);

            if (existingCategory.Success && existingCategory.Data != null)
            {
                return RepositoryResponse<string>.FailureResult("Category already exists.");
            }

            // Create the new BookCategory
            var bookCategory = new BookCategory
            {
                Id = Guid.NewGuid(),
                Name = categoryName
            };
            return await _bookCategoryRepository.AddBookCategoryAsync(bookCategory);
        }
        public async Task<RepositoryResponse<IPaginationMetaDto<string>>> GetAllBookCategoriesAsync(int pageNumber, int pageSize)
        {
            return await _bookCategoryRepository.GetAllBookCategoriesAsync(pageNumber, pageSize);
        }
        public async Task<RepositoryResponse<string>> GetBookCategoryByIdAsync(string bookCategoryId)
        {
            var categoryIdGuid = new Guid(bookCategoryId);
            var bookCategory = await _bookCategoryRepository.GetBookCategoryByIdAsync(categoryIdGuid);

            if(!bookCategory.Success && bookCategory.ErrorMessage != null)
            {
                return RepositoryResponse<string>.FailureResult(bookCategory.ErrorMessage);
            }

            return RepositoryResponse<string>.SuccessResult(bookCategory?.Data?.Name);
        }
        public async Task<RepositoryResponse<string>> GetBookCategoryByNameAsync(string categoryName)
        {
            var bookCategory = await _bookCategoryRepository.GetBookCategoryByNameAsync(categoryName);

            if (!bookCategory.Success && bookCategory.ErrorMessage != null)
            {
                return RepositoryResponse<string>.FailureResult(bookCategory.ErrorMessage);
            }

            return RepositoryResponse<string>.SuccessResult(bookCategory?.Data?.Name);
        }
    }
}
