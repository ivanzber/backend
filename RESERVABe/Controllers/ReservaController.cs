// Controllers/ReservaController.cs
using Microsoft.AspNetCore.Mvc;
using RESERVABe.Models;
using RESERVABe.Services;
using Microsoft.AspNetCore.Authorization;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReservaController : ControllerBase
{
    private readonly ReservaService _reservaService;

    public ReservaController()
    {
        _reservaService = new ReservaService();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult RegistrarReserva(Reserva reserva)
    {
        _reservaService.RegistrarReserva(reserva);
        return Ok("Reserva registrada exitosamente.");
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerReserva(int id)
    {
        Reserva reserva = _reservaService.ObtenerReserva(id);
        return Ok(reserva);
    }

    [HttpPut("{id}")]
    public IActionResult ModificarReserva(int id, Reserva reserva)
    {
        _reservaService.ModificarReserva(id, reserva);
        return Ok("Reserva modificada exitosamente.");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ListarReservas()
    {
        List<Reserva> reservas = _reservaService.ListarReservas();
        return Ok(reservas);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public IActionResult EliminarReserva(int id)
    {
        _reservaService.EliminarReserva(id);
        return Ok("Reserva eliminada exitosamente.");
    }
}