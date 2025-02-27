
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpPost]
        public async Task<IResult> CreateRoomType([FromBody] CreateRoomTypeDTO dto)
        {
            try
            {
                var created = await _roomTypeService.CreateRoomType(dto);
                if (created == null) return Results.BadRequest();
                return Results.Ok(new { created });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }


        [HttpPut]
        public async Task<IResult> UpdateRoomType([FromBody] UpdateRoomTypeDTO dto)
        {
            try
            {
                var updated = await _roomTypeService.UpdateRoomType(dto);
                if (updated == null) return Results.BadRequest();
                return Results.Ok(new { updated });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteRoomType([FromRoute] int id)
        {
            try
            {
                var deleted = await _roomTypeService.DeleteRoomType(id);
                if (deleted != -1) return Results.BadRequest();
                return Results.Ok(new { message = $"Xoá loại phòng với {id} thành công" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpGet]
        public async Task<IResult> GetRoomTypes()
        {
            try
            {
                var results = await _roomTypeService.GetRoomTypes();
                return Results.Ok(new { results });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetRoomType([FromRoute] int id)
        {
            try
            {
                var roomType = await _roomTypeService.GetRoomTypeById(id);
                return Results.Ok(new { roomType });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }

    }
}