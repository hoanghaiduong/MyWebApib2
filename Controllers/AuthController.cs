using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AuthDTO dto)
        {
            var user = await _authService.SignUp(dto);
            return Ok(new { user });
        }
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthDTO dto)
        {
            var user = await _authService.SignIn(dto);
            return Ok(new { user });
        }
    }
}