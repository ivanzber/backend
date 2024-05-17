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
            Document documento = new Document(PageSize.A4, 25, 25, 25, 25); 
            PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
            documento.Open();

            
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DarkGray);
            Paragraph titulo = new Paragraph("Reporte de Historial de Reservas", fuenteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            documento.Add(titulo);

            
            Font fuenteParrafo = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.Black);
            Paragraph introduccion = new Paragraph("La siguiente tabla presenta el registro histórico de reservas junto con los nombres de usuario correspondientes, lo que permite un seguimiento detallado a lo largo del tiempo para mantener un control preciso:\n", fuenteParrafo);
            introduccion.Alignment = Element.ALIGN_JUSTIFIED;
            documento.Add(introduccion);
            documento.Add(new Paragraph(" ")); 

            
            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 100;
            tabla.DefaultCell.Padding = 6;
            tabla.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.DefaultCell.BackgroundColor = new BaseColor(240, 240, 240); 

            // Encabezados de la tabla
            Font fuenteEncabezados = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.Black);
            PdfPCell celdaIdReserva = new PdfPCell(new Phrase("ID Reserva", fuenteEncabezados));
            celdaIdReserva.BackgroundColor = new BaseColor(51, 153, 255); 
            tabla.AddCell(celdaIdReserva);
            tabla.AddCell(new Phrase("Usuario", fuenteEncabezados)); 
            tabla.AddCell(new Phrase("Cancha", fuenteEncabezados));
            tabla.AddCell(new Phrase("Hora Inicio", fuenteEncabezados));
            tabla.AddCell(new Phrase("Hora Fin", fuenteEncabezados));
            tabla.AddCell(new Phrase("Fecha", fuenteEncabezados));

            // Datos de las reservas
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