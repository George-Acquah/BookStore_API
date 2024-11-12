using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain;
using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<RepositoryResponse<IPaginationMetaDto<ISafeUser>>> GetAllUsersAsync(int page, int pageSize, Expression<Func<User, bool>>? filter = null);
        Task<RepositoryResponse<ILoginResponseDto>> LoginUserAsync(LoginDto loginDto);
        Task<RepositoryResponse<string>> RegisterUserAsync(RegisterDto registerDto, ERolesEnum rolesEnum);
    }
}
