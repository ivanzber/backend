// Data/ReservaRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RESERVABe.Models;

namespace RESERVABe.Data
{
    public class ReservaRepository
    {
        private readonly string connectionString = "Data Source=DESKTOP-I1DNC4S;Initial Catalog=DBRESERVA;Integrated Security=True";

        public void RegistrarReserva(Reserva reserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_registrar_reserva", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", reserva.idUsuario);
                command.Parameters.AddWithValue("@idCancha", reserva.idCancha);
                command.Parameters.AddWithValue("@horaInicio", reserva.horaInicio);
                command.Parameters.AddWithValue("@horaFin", reserva.horaFin);
                command.Parameters.AddWithValue("@fecha", reserva.fecha);
                command.ExecuteNonQuery();
            }
        }

        public Reserva ObtenerReserva(int id)
        {
            Reserva reserva = new Reserva();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener_reserva", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idReserva", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reserva.idReserva = reader.GetInt32(0);
                        reserva.idUsuario = reader.GetInt32(1);
                        reserva.idCancha = reader.GetInt32(2);
                        reserva.horaInicio = reader.GetDateTime(3);
                        reserva.horaFin = reader.GetDateTime(4);
                        reserva.fecha = reader.GetDateTime(5);
                    }
                }
            }
            return reserva;
        }

        public void ModificarReserva(int id, Reserva reserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_modificar_reserva", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idReserva", id);
                command.Parameters.AddWithValue("@idUsuario", reserva.idUsuario);
                command.Parameters.AddWithValue("@idCancha", reserva.idCancha);
                command.Parameters.AddWithValue("@horaInicio", reserva.horaInicio);
                command.Parameters.AddWithValue("@horaFin", reserva.horaFin);
                command.Parameters.AddWithValue("@fecha", reserva.fecha);
                command.ExecuteNonQuery();
            }
        }

        public List<Reserva> ListarReservas()
        {
            List<Reserva> reservas = new List<Reserva>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_listar_reservas", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reserva reserva = new Reserva();
                        reserva.idReserva = reader.GetInt32(0);
                        reserva.idUsuario = reader.GetInt32(1);
                        reserva.idCancha = reader.GetInt32(2);
                        reserva.horaInicio = reader.GetDateTime(3);
                        reserva.horaFin = reader.GetDateTime(4);
                        reserva.fecha = reader.GetDateTime(5);
                        reservas.Add(reserva);
                    }
                }
            }
            return reservas;
        }

        public void EliminarReserva(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_eliminar_reserva", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idReserva", id);
                command.ExecuteNonQuery();
            }
        }
    }
}