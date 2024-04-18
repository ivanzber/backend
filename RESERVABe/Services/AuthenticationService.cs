using RESERVABe.Data;
using RESERVABe.Models;

namespace RESERVABe.Services
{
    public class AuthenticationService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public AuthenticationService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool Login(string correo, string clave)
        {
            // Obtener el usuario por el correo electrónico
            Usuario usuario = _usuarioRepository.ObtenerUsuarioPorCorreo(correo);

            if (usuario == null)
            {
                // El usuario no existe
                return false;
            }

            // Verificar la contraseña
            return ContraseñaHasher.VerifyPassword(clave, usuario.clave);
        }
    }
    }

