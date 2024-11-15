using BookStore.Application.Common;


namespace BookStore.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RepositoryResponse<string>> AddRoleAsync(string role);
    }
}
