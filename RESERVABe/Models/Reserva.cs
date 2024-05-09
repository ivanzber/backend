namespace RESERVABe.Models
{
        public class Reserva
        {
        public int idReserva { get; set; }
        public int idUsuario { get; set; }
        public int idCancha { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
        public DateTime fecha { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreCancha { get; set; }
    }
    }

