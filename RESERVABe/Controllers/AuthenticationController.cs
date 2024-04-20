using Microsoft.AspNetCore.Mvc;
using RESERVABe.Models;
using RESERVABe.Services;

namespace RESERVABe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = _authenticationService.Login(login.correo, login.clave);
                if (isAuthenticated)
                {
                    return Ok(new { message = "Inicio de sesión exitoso" });
                }
                else
                {
                    return Unauthorized(new { error = "Correo electrónico o contraseña inválidos" });
                }
            }
            else
            {
                return BadRequest(new { error = "Datos de entrada inválidos", details = ModelState });
            }
        }
    }
}
