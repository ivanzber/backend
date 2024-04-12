using RESERVABe.Data;
using RESERVABe.Models;

namespace RESERVABe.Services
{
    public class CanchaService
    {
        private readonly CanchaRepository _canchaRepository;

        public CanchaService()
        {
            _canchaRepository = new CanchaRepository();
        }

        public void RegistrarCancha(Cancha cancha)
        {
            _canchaRepository.RegistrarCancha(cancha);
        }

        public Cancha ObtenerCancha(int id)
        {
            return _canchaRepository.ObtenerCancha(id);
        }

        public void ModificarCancha(int id, Cancha cancha)
        {
            _canchaRepository.ModificarCancha(id, cancha);
        }

        public List<Cancha> ListarCanchas()
        {
            return _canchaRepository.ListarCanchas();
        }

        public void EliminarCancha(int id)
        {
            _canchaRepository.EliminarCancha(id);
        }
    }
}