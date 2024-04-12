// Data/CanchaRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RESERVABe.Models;

namespace RESERVABe.Data
{
    public class CanchaRepository
    {
        private readonly string connectionString = "Data Source=DESKTOP-I1DNC4S;Initial Catalog=DBRESERVA;Integrated Security=True";

        public void RegistrarCancha(Cancha cancha)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_registrar_cancha", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombreCancha", cancha.nombreCancha);
                command.Parameters.AddWithValue("@idTipo", cancha.idTipo);
                command.Parameters.AddWithValue("@ubicacion", cancha.ubicacion);
                command.Parameters.AddWithValue("@descripcion", cancha.descripcion);
                command.Parameters.AddWithValue("@precio", cancha.precio);
                command.ExecuteNonQuery();
            }
        }

        public Cancha ObtenerCancha(int id)
        {
            Cancha cancha = new Cancha();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener_cancha", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idCancha", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cancha.idCancha = reader.GetInt32(0);
                        cancha.nombreCancha = reader.GetString(1);
                        cancha.idTipo = reader.GetInt32(2);
                        cancha.ubicacion = reader.GetString(3);
                        cancha.descripcion = reader.GetString(4);
                        cancha.precio = reader.GetDecimal(5);
                    }
                }
            }
            return cancha;
        }

        public void ModificarCancha(int id, Cancha cancha)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_modificar_cancha", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idCancha", id);
                command.Parameters.AddWithValue("@nombreCancha", cancha.nombreCancha);
                command.Parameters.AddWithValue("@idTipo", cancha.idTipo);
                command.Parameters.AddWithValue("@ubicacion", cancha.ubicacion);
                command.Parameters.AddWithValue("@descripcion", cancha.descripcion);
                command.Parameters.AddWithValue("@precio", cancha.precio);
                command.ExecuteNonQuery();
            }
        }

        public List<Cancha> ListarCanchas()
        {
            List<Cancha> canchas = new List<Cancha>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_listar_canchas", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cancha cancha = new Cancha();
                        cancha.idCancha = reader.GetInt32(0);
                        cancha.nombreCancha = reader.GetString(1);
                        cancha.idTipo = reader.GetInt32(2);
                        cancha.ubicacion = reader.GetString(3);
                        cancha.descripcion = reader.GetString(4);
                        cancha.precio = reader.GetDecimal(5);
                        canchas.Add(cancha);
                    }
                }
            }
            return canchas;
        }

        public void EliminarCancha(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_eliminar_cancha", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idCancha", id);
                command.ExecuteNonQuery();
            }
        }
    }
}