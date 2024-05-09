using Microsoft.AspNetCore.Mvc;
using RESERVABe.Services;
using Microsoft.AspNetCore.Authorization;

namespace RESERVABe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ReporteController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("reservas-activas")]
        public IActionResult ObtenerReporteReservasActivas()
        {
            try
            {
                byte[] pdf = _reporteService.GenerarReporteReservasActivas();
                return File(pdf, "application/pdf", "reporte_reservas_activas.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el reporte: {ex.Message}");
            }
        }
    }
}