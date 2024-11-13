using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.DB;
using BookStore.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;


namespace BookStore.Infrastructure.Repositories
{
    public class UserRepository(BookStoreAPIDbContext dbContext, ILogger<UserRepository> logger, CryptographicHelpers cryptographyHelpers) : IUserRepository
    {
        private readonly BookStoreAPIDbContext _dbContext = dbContext;
        private readonly ILogger<UserRepository> _logger = logger;
        private readonly CryptographicHelpers _cryptographyHelpers = cryptographyHelpers;

        public async Task<RepositoryResponse<IPaginationMetaDto<ISafeUser>>> GetAllUsersAsync(int pageNumber, int pageSize, Expression<Func<User, bool>>? filter = null)
        {
            try
            {
                var query = _dbContext.Users.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                var projectedQuery = query.Include(u => u.Role).Select(r => new SafeUserDto
                {
                    Id = r.Id,
                    Email = r.Email,
                    UserName = r.UserName,
                    UserRole = r.Role != null ? r.Role.Name.ToString() : "",
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).Cast<ISafeUser>();

                var paginatedUsers = await PaginationHelper.GetPaginatedResultAsync(projectedQuery, pageNumber, pageSize);

                return RepositoryResponse<IPaginationMetaDto<ISafeUser>>.SuccessResult(paginatedUsers);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<IPaginationMetaDto<ISafeUser>>.FailureResult(ex.Message);
            }
        }
        public async Task<RepositoryResponse<string>> RegisterUserAsync(RegisterDto registerDto, ERolesEnum rolesEnum)
        {
            try
            {
                // Check if email already exists
                if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
                {
                    return RepositoryResponse<string>.FailureResult("Email already exists");
                }

                // Get the role ID based on the provided rolesEnum
                var role = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == rolesEnum);
                if (role == null) 
                {
                    return RepositoryResponse<string>.FailureResult("Role not found");
                }

                // Create a new user
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = registerDto.UserName,
                    RoleId = role.Id,
                    Email = registerDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return RepositoryResponse<string>.SuccessResult("User registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering a new user.");
                return RepositoryResponse<string>.FailureResult("An error occurred while registering the user.");
            }
        }

        public async Task<RepositoryResponse<ISafeUser>> GetSingleUserAsync(Guid userId)
        {
            try
            {
                var user = await _dbContext.Users.Include(u => u.Role).Select(r => new SafeUserDto
                {
                    Id = r.Id,
                    Email = r.Email,
                    UserName = r.UserName,
                    UserRole = r.Role != null ? r.Role.Name.ToString() : "",
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).Cast<ISafeUser>().FirstAsync(ru => ru.Id == userId);

                return RepositoryResponse<ISafeUser>.SuccessResult(user);
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RepositoryResponse<ISafeUser>.FailureResult(ex.Message);
            }
        }

        public async Task<RepositoryResponse<ILoginResponseDto>> LoginUserAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    return RepositoryResponse<ILoginResponseDto>.FailureResult("Invalid credentials.");
                }

                var accessToken = _cryptographyHelpers.GenerateToken(user);

                return RepositoryResponse<ILoginResponseDto>.SuccessResult(new LoginResponseDto
                {
                    Tokens = new Tokens
                    { AccessToken = accessToken, RefreshToken = "" },
                    UserName = user.UserName,
                    UserRole = user?.Role?.Name.ToString() ?? ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering a new user.");
                return RepositoryResponse<ILoginResponseDto>.FailureResult("An error occurred while registering the user.");
            }
        }
    }
}
