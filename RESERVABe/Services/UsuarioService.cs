using RESERVABe.Data;
using RESERVABe.Models;

namespace RESERVABe.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            _usuarioRepository.RegistrarUsuario(usuario);
        }

        public Usuario ObtenerUsuario(int id)
        {
            return _usuarioRepository.ObtenerUsuario(id);
        }

        public void ModificarUsuario(int id, Usuario usuario)
        {
            _usuarioRepository.ModificarUsuario(id, usuario);
        }

        public List<Usuario> ListarUsuarios()
        {
            return _usuarioRepository.ListarUsuarios();
        }

        public void EliminarUsuario(int id)
        {
            _usuarioRepository.EliminarUsuario(id);
        }
    }
}