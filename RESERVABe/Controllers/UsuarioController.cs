using Microsoft.AspNetCore.Mvc;
using RESERVABe.Models;
using RESERVABe.Services;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController()
    {
        _usuarioService = new UsuarioService();
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario usuario)
    {
        _usuarioService.RegistrarUsuario(usuario);
        return Ok("Usuario registrado exitosamente.");
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerUsuario(int id)
    {
        Usuario usuario = _usuarioService.ObtenerUsuario(id);
        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public IActionResult ModificarUsuario(int id, Usuario usuario)
    {
        _usuarioService.ModificarUsuario(id, usuario);
        return Ok("Usuario modificado exitosamente.");
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        List<Usuario> usuarios = _usuarioService.ListarUsuarios();
        return Ok(usuarios);
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarUsuario(int id)
    {
        _usuarioService.EliminarUsuario(id);
        return Ok("Usuario eliminado exitosamente.");
    }
}
