using BookStore.Application.Common;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public class RoleService(IRolesRepository rolesRepository): IRoleService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        public async Task<RepositoryResponse<string>> AddRoleAsync(string stringRole)
        {
            if (Enum.TryParse(stringRole, true, out ERolesEnum roleResult))
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleResult
                };
                return await _rolesRepository.AddRoleAsync(role);
            }
            else
            {
                return RepositoryResponse<string>.FailureResult("Invalid string value provided");
            }

        }


    }
}
