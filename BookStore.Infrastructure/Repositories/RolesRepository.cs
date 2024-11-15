using BookStore.Application.Common;
using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BookStore.Infrastructure.Repositories
{
    public class RolesRepository(BookStoreAPIDbContext bookStoreAPIDbContext, ILogger<RolesRepository> logger): IRolesRepository
    {
        private readonly BookStoreAPIDbContext _dbContext = bookStoreAPIDbContext;
        private readonly ILogger _logger = logger;

        public async Task<RepositoryResponse<List<string>>> GetAllRolesAsync()
        {
            try
            {
                var result = await _dbContext.Roles.Select(r => r.Name.ToString()).ToListAsync();

                return RepositoryResponse<List<string>>.SuccessResult(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<List<string>>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<List<string>>> AddRolesAsync()
        {
            try
            {
                var dbRoles = await _dbContext.Roles.AnyAsync();

                if(dbRoles)
                {
                    return RepositoryResponse<List<string>>.FailureResult("You already have Roles in DB");
                }

                var rolesToAdd = Enum.GetNames(typeof(ERolesEnum)).ToList();
                
                foreach (var roleName in rolesToAdd) 
                    {
                        if (Enum.TryParse(roleName, out ERolesEnum roleEnum)) 
                            { 
                            var role = new Role 
                                { 
                                    Id = Guid.NewGuid(), 
                                    Name = roleEnum 
                                }; 
                            await _dbContext.Roles.AddAsync(role); }
                    }

                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<List<string>>.SuccessResult(rolesToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<List<string>>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> AddRoleAsync(Role role)
        {
            try
            {
                await _dbContext.AddAsync(role);
                await _dbContext.SaveChangesAsync();
                return RepositoryResponse<string>.SuccessResult("Added role successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<string>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<string>> DeleteAllRolesAsync()
        {
            try
            {
                var entities = await _dbContext.Roles.ToListAsync(); 
                _dbContext.Roles.RemoveRange(entities); 
                await _dbContext.SaveChangesAsync();

                return RepositoryResponse<string>.SuccessResult("You have successfully deleted all roles");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<string>.FailureResult(ex.Message);
            }
        }
    }
}
