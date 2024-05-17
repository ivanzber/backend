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
                command.Parameters.AddWithValue("@fecha", reserva.fecha.ToString("d"));
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
                        reserva.horaInicio = reader.GetTimeSpan(3);
                        reserva.horaFin = reader.GetTimeSpan(4);
                        reserva.fecha = DateOnly.FromDateTime(reader.GetDateTime(5));
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
                command.Parameters.AddWithValue("@fecha", reserva.fecha.ToString("d"));
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
                        reserva.horaInicio = reader.GetTimeSpan(3);
                        reserva.horaFin = reader.GetTimeSpan(4);
                        reserva.fecha = DateOnly.FromDateTime(reader.GetDateTime(5));
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

        public List<Reserva> ObtenerReservasActivas()
        {
            List<Reserva> reservasActivas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT r.idReserva, u.nombreUsuario, c.nombreCancha, r.horaInicio, r.horaFin, r.fecha
                         FROM Reserva r
                         INNER JOIN Usuario u ON r.idUsuario = u.idUsuario
                         INNER JOIN Cancha c ON r.idCancha = c.idCancha";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reserva reserva = new Reserva();
                        reserva.idReserva = reader.GetInt32(0);
                        reserva.nombreUsuario = reader.GetString(1);
                        reserva.nombreCancha = reader.GetString(2);
                        reserva.horaInicio = reader.GetTimeSpan(3);
                        reserva.horaFin = reader.GetTimeSpan(4);
                        reserva.fecha = DateOnly.FromDateTime(reader.GetDateTime(5));

                        reservasActivas.Add(reserva);
                    }
                }
            }

            return reservasActivas;
        }
    }
}
