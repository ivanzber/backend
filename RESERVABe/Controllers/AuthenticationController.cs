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

        public AuthController(AuthenticationService authService)
        {
            _authService = authService;
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
                // Aquí podrías generar un token JWT u otra información de sesión
                return Ok("Usuario autenticado");
            }

            return Unauthorized("Credenciales no válidas");
        }
    }
}
