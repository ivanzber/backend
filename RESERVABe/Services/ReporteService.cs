using iTextSharp.text;
using iTextSharp.text.pdf;
using RESERVABe.Data;
using RESERVABe.Models;

namespace RESERVABe.Services
{
    public class ReporteService
    {
        private readonly ReservaRepository _reservaRepository;

        public ReporteService(ReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public byte[] GenerarReporteReservasActivas()
        {
            List<Reserva> reservasActivas = _reservaRepository.ObtenerReservasActivas();
            MemoryStream memoryStream = new MemoryStream();
            Document documento = new Document();
            PdfWriter.GetInstance(documento, memoryStream);
            documento.Open();

            Paragraph titulo = new Paragraph("Reporte de Historial de Reservas ");
            titulo.Alignment = Element.ALIGN_CENTER;
            documento.Add(titulo);
            documento.Add(new Paragraph("La siguiente tabla presenta el registro histórico de reservas junto con los nombres de usuario correspondientes, lo que permite un seguimiento detallado a lo largo del tiempo para mantener un control preciso:\n"));

            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 100;
            tabla.DefaultCell.Padding = 3;
            tabla.AddCell("ID Reserva");
            tabla.AddCell("Usuario");
            tabla.AddCell("Cancha");
            tabla.AddCell("Hora Inicio");
            tabla.AddCell("Hora Fin");
            tabla.AddCell("Fecha");

            foreach (Reserva reserva in reservasActivas)
            {
                tabla.AddCell(reserva.idReserva.ToString());
                tabla.AddCell(reserva.nombreUsuario.ToString());
                tabla.AddCell(reserva.nombreCancha.ToString());
                tabla.AddCell(reserva.horaInicio.ToString(@"hh\:mm"));
                tabla.AddCell(reserva.horaFin.ToString(@"hh\:mm"));
                tabla.AddCell(reserva.fecha.ToString("yyyy/MM/dd"));
            }

            documento.Add(tabla);
            documento.Close();

            return memoryStream.ToArray();
        }
    }
}