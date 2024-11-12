using BookStore.Application.Common;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController(IRolesRepository rolesRepository) : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
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
        public async Task<IActionResult> AddRoles()
        {
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

        public async Task<IActionResult> DeleteRoles()
        {
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
