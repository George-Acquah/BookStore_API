using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Domain;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<RepositoryResponse<List<ISafeUser>>> GetAllUsersAsync();
        Task<RepositoryResponse<ILoginResponseDto>> LoginUserAsync(LoginDto loginDto);
        Task<RepositoryResponse<string>> RegisterUserAsync(RegisterDto registerDto, ERolesEnum rolesEnum);
    }
}
