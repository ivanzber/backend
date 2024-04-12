namespace RESERVABe.Models
{
    public class Reserva
    {
        public int idReserva { get; set; } 
        public int idUsuario { get; set; }
        public int idCancha { get; set; }
        public DateTime horaInicio { get; set; } 
        public DateTime horaFin { get; set; } 
        public DateTime fecha { get; set; }

    }
}
