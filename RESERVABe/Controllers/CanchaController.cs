// Controllers/CanchaController.cs
using Microsoft.AspNetCore.Mvc;
using RESERVABe.Models;
using RESERVABe.Services;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CanchaController : ControllerBase
{
    private readonly CanchaService _canchaService;

    public CanchaController()
    {
        _canchaService = new CanchaService();
    }

    [HttpPost]
    public IActionResult RegistrarCancha(Cancha cancha)
    {
        _canchaService.RegistrarCancha(cancha);
        return Ok("Cancha registrada exitosamente.");
    }

    [HttpGet("{id}")]
   
    public IActionResult ObtenerCancha(int id)
    {
        Cancha cancha = _canchaService.ObtenerCancha(id);
        return Ok(cancha);
    }

    [HttpPut("{id}")]
    public IActionResult ModificarCancha(int id, Cancha cancha)
    {
        _canchaService.ModificarCancha(id, cancha);
        return Ok("Cancha modificada exitosamente.");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ListarCanchas()
    {
        List<Cancha> canchas = _canchaService.ListarCanchas();
        return Ok(canchas);
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarCancha(int id)
    {
        _canchaService.EliminarCancha(id);
        return Ok("Cancha eliminada exitosamente.");
    }
}