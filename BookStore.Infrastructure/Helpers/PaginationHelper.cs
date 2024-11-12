using BookStore.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Helpers
{
    public class PaginationHelper
    {
        public static async Task<PaginationMetaDto<T>> GetPaginatedResultAsync<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return new PaginationMetaDto<T>
            {
                Items = items,
                TotalItems = totalRecords,
                CurrentPage = pageNumber,
                ItemsPerPage = pageSize,
                TotalPages = totalPages
            };
        }
    }
}
