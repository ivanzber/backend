using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using RESERVABe.Models;

namespace RESERVABe.Data
{
    public class UsuarioRepository
    {
        private readonly string connectionString = "Data Source=DESKTOP-I1DNC4S;Initial Catalog=DBRESERVA;Integrated Security=True";

        public void RegistrarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_registrar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombreusuario", usuario.nombreUsuario);
                command.Parameters.AddWithValue("@apellidousuario", usuario.apellidoUsuario);
                command.Parameters.AddWithValue("@correo", usuario.correo);
                command.Parameters.AddWithValue("@idRol", usuario.idRol);
                command.Parameters.AddWithValue("@clave", ContraseñaHasher.HashPassword(usuario.clave));
                command.ExecuteNonQuery();
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idusuario", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuario.idUsuario = reader.GetInt32(0);
                        usuario.nombreUsuario = reader.GetString(1);
                        usuario.apellidoUsuario = reader.GetString(2);
                        usuario.correo = reader.GetString(3);
                        usuario.idRol = reader.GetInt32(4);
                        usuario.clave = reader.GetString(5);
                    }
                }
            }
            return usuario;
        }
        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM usuario WHERE correo = @correo", connection);
                command.Parameters.AddWithValue("@correo", correo);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuario.idUsuario = reader.GetInt32(0);
                        usuario.nombreUsuario = reader.GetString(1);
                        usuario.apellidoUsuario = reader.GetString(2);
                        usuario.correo = reader.GetString(3);
                        usuario.idRol = reader.GetInt32(4);
                        usuario.clave = reader.GetString(5);
                    }
                }
            }
            return usuario;
        }
        public void ModificarUsuario(int id, Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_modificar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idusuario", id);
                command.Parameters.AddWithValue("@nombreusuario", usuario.nombreUsuario);
                command.Parameters.AddWithValue("@apellidousuario", usuario.apellidoUsuario);
                command.Parameters.AddWithValue("@correo", usuario.correo);
                command.Parameters.AddWithValue("@idRol", usuario.idRol);
                command.Parameters.AddWithValue("@clave", usuario.clave);
                command.ExecuteNonQuery();
            }
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_listar", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.idUsuario = reader.GetInt32(0);
                        usuario.nombreUsuario = reader.GetString(1);
                        usuario.apellidoUsuario = reader.GetString(2);
                        usuario.correo = reader.GetString(3);
                        usuario.idRol = reader.GetInt32(4);
                        usuario.clave = reader.GetString(5);
                        usuarios.Add(usuario);
                    }
                }
            }
            return usuarios;
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_eliminar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idusuario", id);
                command.ExecuteNonQuery();
            }
        }
    }
}