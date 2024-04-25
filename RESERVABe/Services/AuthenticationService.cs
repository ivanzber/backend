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
            // Obtener la contraseña encriptada del usuario por el correo electrónico
            string contraseñaEncriptada = _usuarioRepository.ObtenerContraseñaPorCorreo(correo);

            if (contraseñaEncriptada == null)
            {
                // El usuario no existe
                return false;
            }

            // Desencriptar la contraseña obtenida de la base de datos
            string contraseñaDesencriptada = ContraseñaHasher.Decrypt(contraseñaEncriptada);

            // Verificar la contraseña
            return clave == contraseñaDesencriptada;
        }
    }
}
