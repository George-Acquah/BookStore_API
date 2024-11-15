using BookStore.API.Helpers;
using BookStore.Application.Common;
using BookStore.Application.Interfaces;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController(IRolesRepository rolesRepository) : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var userRole = ClaimsHelper.GetUserRoleFromClaims(User);
            if (userRole == ERolesEnum.USER.ToString())
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status401Unauthorized,
                    "Users cannot view roles",
                    null));
            }
            var result = await _rolesRepository.GetAllRolesAsync();
            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "Fetched all roles",
                result.Data));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRoles()
        {
            var userRole = ClaimsHelper.GetUserRoleFromClaims(User);
            if (userRole != ERolesEnum.SUPER_ADMIN.ToString())
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status401Unauthorized,
                    "Contact you super admin to add this role",
                    null));
            }
            var result = await _rolesRepository.AddRolesAsync();
            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                "You have successfully added the blogs",
                result.Data));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteRoles()
        {
            var userRole = ClaimsHelper.GetUserRoleFromClaims(User);
            if (userRole != ERolesEnum.SUPER_ADMIN.ToString())
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status401Unauthorized,
                    "Contact you super admin to delete this role",
                    null));
            }
            var result = await _rolesRepository.DeleteAllRolesAsync();
            if (!result.Success)
            {
                return BadRequest(new BaseAPIResponse<string>(
                    StatusCodes.Status400BadRequest,
                    result.ErrorMessage,
                    null));
            }

            return Ok(new BaseAPIResponse<object>(
                StatusCodes.Status200OK,
                result.Data,
                null));
        }
    }
}
