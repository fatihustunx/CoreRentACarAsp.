using Business.Abstracts;
using Entities.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authSerice)
        {
            _authService = authSerice;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var result = _authService.Register(userForRegisterDto);

            if(result.Success)
            {
                var accessToken = _authService.CreateAccessToken(result.Data);

                return Ok(accessToken);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _authService.Login(userForLoginDto);

            if(result.Success)
            {
                var accessToken = _authService.CreateAccessToken(result.Data);

                return Ok(accessToken);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}