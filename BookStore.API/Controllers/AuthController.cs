using BookStore.Application.Common;
using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, [FromQuery] string? roleQuery)
        {
            if (!Enum.TryParse<ERolesEnum>(roleQuery?.Trim('"'), true, out ERolesEnum userRoleEnum))
            {
                userRoleEnum = ERolesEnum.USER;
            }

            var result = await _userRepository.RegisterUserAsync(registerDto, userRoleEnum);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Registration successful",
                null));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userRepository.LoginUserAsync(loginDto);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            var accessTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(120)
            };

            Response.Cookies.Append("access_token", result.Data!.Tokens.AccessToken, accessTokenOptions);

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Login successful",
                result.Data));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers( [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userRepository.GetAllUsersAsync(pageNumber, pageSize);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Users retrieved successfully",
                result.Data));


        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetSingleUser(string userId)
        {
            var userGuid = Guid.Parse(userId);

            var result = await _userRepository.GetSingleUserAsync(userGuid);

            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "User retrieved successfully",
                result.Data));
        }
    }
}
