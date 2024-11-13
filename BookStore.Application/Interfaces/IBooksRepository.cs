using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IBooksRepository
    {
        Task<RepositoryResponse<IPaginationMetaDto<IBook>>> GetAllBooksAsync(int page, int pageSize, Expression<Func<User, bool>>? filter = null);
    }
}
