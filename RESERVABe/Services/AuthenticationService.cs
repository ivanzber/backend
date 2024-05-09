using RESERVABe.Data;


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
          
            string contraseñaEncriptada = _usuarioRepository.ObtenerContraseñaPorCorreo(correo);

            if (contraseñaEncriptada == null)
            {
           
                return false;
            }

         
            string contraseñaDesencriptada = ContraseñaHasher.Decrypt(contraseñaEncriptada);

          
            return clave == contraseñaDesencriptada;
        }
    }
}
