using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class ServicioDAL
    {
        public static int AgregarServicio(Servicio servicio)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO SERVICIO(Nombre, Precio, Descripcion) VALUES(@nombre, @precio, @descripcion)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@precio", servicio.Precio);
                cmd.Parameters.AddWithValue("@descripcion", servicio.Descripcion);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarServicio(Servicio servicio)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "UPDATE SERVICIO SET Nombre=@nombre, Precio=@precio, Descripcion=@descripcion WHERE Id_servicio=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@precio", servicio.Precio);
                cmd.Parameters.AddWithValue("@descripcion", servicio.Descripcion);
                cmd.Parameters.AddWithValue("@id", servicio.Id_servicio);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarServicio(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM SERVICIO WHERE Id_servicio=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<Servicio> MostrarServicios()
        {
            List<Servicio> servicios = new List<Servicio>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM SERVICIO";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    servicios.Add(new Servicio
                    {
                        Id_servicio = Convert.ToInt32(reader["Id_servicio"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? "" : reader["Descripcion"].ToString()
                    });
                }
                con.Close();
            }
            return servicios;
        }

        public static Servicio ObtenerServicioPorId(int id)
        {
            Servicio servicio = new Servicio();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM SERVICIO WHERE Id_servicio=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    servicio.Id_servicio = Convert.ToInt32(reader["Id_servicio"]);
                    servicio.Nombre = reader["Nombre"].ToString();
                    servicio.Precio = Convert.ToDecimal(reader["Precio"]);
                    servicio.Descripcion = reader["Descripcion"] == DBNull.Value ? "" : reader["Descripcion"].ToString();
                }
                con.Close();
            }
            return servicio;
        }

        public static List<Servicio> BuscarServicio(string texto)
        {
            List<Servicio> servicios = new List<Servicio>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM SERVICIO WHERE Nombre LIKE @texto";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    servicios.Add(new Servicio
                    {
                        Id_servicio = Convert.ToInt32(reader["Id_servicio"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? "" : reader["Descripcion"].ToString()
                    });
                }
                con.Close();
            }
            return servicios;
        }
    }
}