using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeSerivce _roomTypeService;

        public RoomTypeController(IRoomTypeSerivce roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        //crud generate here
        [HttpPost]
        public async Task<IResult> CreateRoomType([FromBody] CreateRoomTypeDTO dto)
        {
            try
            {
                var created = await _roomTypeService.CreateRoomTypeAsync(dto);
                if (created == null) return Results.BadRequest();
                return Results.Ok(new { created });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
        }
    }
}