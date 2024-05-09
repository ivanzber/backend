using Microsoft.AspNetCore.Mvc;
using RESERVABe.Models;
using RESERVABe.Services;

namespace RESERVABe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authService;
        private readonly JwtSettingsDto _jwtSettings;

        public AuthController(AuthenticationService authService, JwtSettingsDto jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest("Los datos de inicio de sesión son nulos");
            }

            bool isAuthenticated = _authService.Login(login.correo, login.clave);
            if (isAuthenticated)
            {
                ResponseLogin responseLogin = new ResponseLogin();
                responseLogin = JwtServices.GenTokenKey(responseLogin, _jwtSettings);

                if (responseLogin == null)
                {
                    return Unauthorized("Error al generar el token JWT");
                }

                return Ok(responseLogin);
            }

            return Unauthorized("Usuario o clave no válidas");
        }
    }
}