
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IResult> CreateUser([FromBody] CreateUserDTO dto)
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
        public async Task<IResult> UpdateUser(int id,[FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updated = await _userService.UpdateUser(id,dto);
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
        public async Task<IResult> GetUser([FromRoute] int id)
        {
            try
            {
                var User = await _userService.GetUserById(id);
                return Results.Ok(new { User });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
    }
}