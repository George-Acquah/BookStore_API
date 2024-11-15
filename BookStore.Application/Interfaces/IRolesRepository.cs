using BookStore.Application.Common;
using BookStore.Domain;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IRolesRepository
    {
        Task<RepositoryResponse<List<string>>> GetAllRolesAsync();
        //Task<RepositoryResponse<List<string>>> GetRoleAsync();

        Task<RepositoryResponse<List<string>>> AddRolesAsync();
        Task<RepositoryResponse<string>> AddRoleAsync(Role role);

        Task<RepositoryResponse<string>> DeleteAllRolesAsync();
    }
}
