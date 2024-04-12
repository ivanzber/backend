namespace RESERVABe.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string correo { get; set; }
        public int idRol { get; set; }
        public string clave { get; set; }
    }
}
