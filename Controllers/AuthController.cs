using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO;
using MyWebApi.Application.Interfaces;
using MyWebApi.Infrastructure.Models;

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

        [HttpPost("profile"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            //Unauthorize
            try
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = User.FindFirstValue(ClaimTypes.Email);
                var roleClaims = User.Claims
                                            .Where(c => c.Type == ClaimTypes.Role)
                                            .Select(c => c.Value)
                                            .ToList();
                return Ok(new
                {
                    id,
                    email,
                    roleClaims
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var result = _authService.RefreshToken(accessToken);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
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
            var authResult = await _authService.SignIn(dto);
            return Ok(new
            {
                user = authResult.Item1,
                authResult.Item2
            });
        }
    }
}