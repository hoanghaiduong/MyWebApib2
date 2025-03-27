
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRolesDTO dto)
        {
            var result = await _userService.AssignRoleToUser(dto);
            return Ok(result);
        }
        [HttpPost("assign-roles")]
        public async Task<IActionResult> AssignRolesToUser([FromBody] RolesToUser dto)
        {
            var result = await _userService.AssignRolesToUser(dto);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IResult> CreateUser([FromForm] CreateUserDTO dto)
        {
            try
            {
                var created = await _userService.CreateUser(dto);
                if (created == null) return Results.BadRequest();
                return Results.Ok(new { created });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IResult> UpdateUser(int id, [FromForm] UpdateUserDTO dto)
        {
            try
            {
                var updated = await _userService.UpdateUser(id, dto);
                if (updated == null) return Results.BadRequest();
                return Results.Ok(new { updated });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var deleted = await _userService.DeleteUser(id);
                if (deleted != -1) return Results.BadRequest();
                return Results.Ok(new { message = $"Xoá loại phòng với {id} thành công" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpGet]
        public async Task<IResult> GetUsers()
        {
            try
            {
                var results = await _userService.GetUsers();
                return Results.Ok(new { results });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetUser([FromRoute] int id, [FromQuery] int depth = 0)
        {
            try
            {
                var User = await _userService.GetUserById(id, depth);
                return Results.Ok(new { User });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
    }
}