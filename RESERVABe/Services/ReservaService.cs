using RESERVABe.Data;
using RESERVABe.Models;

namespace RESERVABe.Services
{
    public class ReservaService
    {
        private readonly ReservaRepository _reservaRepository;

        public ReservaService()
        {
            _reservaRepository = new ReservaRepository();
        }

        public void RegistrarReserva(Reserva reserva)
        {
            _reservaRepository.RegistrarReserva(reserva);
        }

        public Reserva ObtenerReserva(int id)
        {
            return _reservaRepository.ObtenerReserva(id);
        }

        public void ModificarReserva(int id, Reserva reserva)
        {
            _reservaRepository.ModificarReserva(id, reserva);
        }

        public List<Reserva> ListarReservas()
        {
            return _reservaRepository.ListarReservas();
        }

        public void EliminarReserva(int id)
        {
            _reservaRepository.EliminarReserva(id);
        }
    }
}