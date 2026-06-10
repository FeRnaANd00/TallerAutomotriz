using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class VehiculoDAL
    {
        public static int AgregarVehiculo(Vehiculo vehiculo)
        {
            int resultado = 0;

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();

                string sql = @"INSERT INTO VEHICULO
                              (Modelo, Color, Placa,
                               Fecha_Entre, Hora_Entre,
                               Fecha_Salida, Hora_Salida,
                               Cliente_Id, Mecanico_Id)
                              VALUES
                              (@modelo,@color,@placa,
                               @fechaEntre,@horaEntre,
                               @fechaSalida,@horaSalida,
                               @cliente,@mecanico)";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@fechaEntre", vehiculo.Fecha_Entre);
                cmd.Parameters.AddWithValue("@horaEntre", vehiculo.Hora_Entre);
                cmd.Parameters.AddWithValue("@fechaSalida", vehiculo.Fecha_Salida.HasValue ? vehiculo.Fecha_Salida : DBNull.Value);
                cmd.Parameters.AddWithValue("@horaSalida", vehiculo.Hora_Salida);
                cmd.Parameters.AddWithValue("@cliente", vehiculo.Cliente_Id);
                cmd.Parameters.AddWithValue("@mecanico", vehiculo.Mecanico_Id.HasValue ? vehiculo.Mecanico_Id : DBNull.Value);

                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }

            return resultado;
        }

        public static int ModificarVehiculo(Vehiculo vehiculo)
        {
            int resultado = 0;

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();

                string sql = @"UPDATE VEHICULO SET
                              Modelo=@modelo,
                              Color=@color,
                              Placa=@placa,
                              Fecha_Entre=@fechaEntre,
                              Hora_Entre=@horaEntre,
                              Fecha_Salida=@fechaSalida,
                              Hora_Salida=@horaSalida,
                              Cliente_Id=@cliente,
                              Mecanico_Id=@mecanico
                              WHERE Id_vehiculo=@id";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@fechaEntre", vehiculo.Fecha_Entre);
                cmd.Parameters.AddWithValue("@horaEntre", vehiculo.Hora_Entre);
                cmd.Parameters.AddWithValue("@fechaSalida", vehiculo.Fecha_Salida.HasValue ? vehiculo.Fecha_Salida : DBNull.Value);
                cmd.Parameters.AddWithValue("@horaSalida", vehiculo.Hora_Salida);
                cmd.Parameters.AddWithValue("@cliente", vehiculo.Cliente_Id);
                cmd.Parameters.AddWithValue("@mecanico", vehiculo.Mecanico_Id.HasValue ? vehiculo.Mecanico_Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id", vehiculo.Id_vehiculo);

                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }

            return resultado;
        }

        public static int EliminarVehiculo(int id)
        {
            int resultado = 0;

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM VEHICULO WHERE Id_vehiculo=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }

            return resultado;
        }

        public static List<Vehiculo> MostrarVehiculos()
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();

                string sql = @"SELECT V.*,
                                      C.Nombre AS NombreCliente,
                                      M.Nombre AS NombreMecanico
                               FROM VEHICULO V
                               INNER JOIN CLIENTE C ON V.Cliente_Id = C.Id_Cliente
                               LEFT JOIN MECANICOS M ON V.Mecanico_Id = M.Id_mecanico";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Vehiculo
                    {
                        Id_vehiculo = Convert.ToInt32(reader["Id_vehiculo"]),
                        Modelo = reader["Modelo"].ToString(),
                        Color = reader["Color"].ToString(),
                        Placa = reader["Placa"].ToString(),
                        Fecha_Entre = Convert.ToDateTime(reader["Fecha_Entre"]),
                        Hora_Entre = reader["Hora_Entre"].ToString(),
                        Fecha_Salida = reader["Fecha_Salida"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Salida"]),
                        Hora_Salida = reader["Hora_Salida"].ToString(),
                        Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]),
                        Mecanico_Id = reader["Mecanico_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Id"]),
                        NombreCliente = reader["NombreCliente"].ToString(),
                        NombreMecanico = reader["NombreMecanico"] == DBNull.Value ? "Sin asignar" : reader["NombreMecanico"].ToString()
                    });
                }

                con.Close();
            }

            return lista;
        }

        public static Vehiculo ObtenerVehiculoPorId(int id)
        {
            Vehiculo vehiculo = new Vehiculo();

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();

                string sql = "SELECT * FROM VEHICULO WHERE Id_vehiculo=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    vehiculo.Id_vehiculo = Convert.ToInt32(reader["Id_vehiculo"]);
                    vehiculo.Modelo = reader["Modelo"].ToString();
                    vehiculo.Color = reader["Color"].ToString();
                    vehiculo.Placa = reader["Placa"].ToString();
                    vehiculo.Fecha_Entre = Convert.ToDateTime(reader["Fecha_Entre"]);
                    vehiculo.Hora_Entre = reader["Hora_Entre"].ToString();
                    vehiculo.Fecha_Salida = reader["Fecha_Salida"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Salida"]);
                    vehiculo.Hora_Salida = reader["Hora_Salida"].ToString();
                    vehiculo.Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]);
                    vehiculo.Mecanico_Id = reader["Mecanico_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Id"]);
                }

                con.Close();
            }

            return vehiculo;
        }

        public static List<Vehiculo> BuscarVehiculo(string texto)
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();

                string sql = @"SELECT V.*,
                                      C.Nombre AS NombreCliente,
                                      M.Nombre AS NombreMecanico
                               FROM VEHICULO V
                               INNER JOIN CLIENTE C ON V.Cliente_Id = C.Id_Cliente
                               LEFT JOIN MECANICOS M ON V.Mecanico_Id = M.Id_mecanico
                               WHERE V.Modelo LIKE @texto
                                  OR V.Color  LIKE @texto
                                  OR V.Placa  LIKE @texto";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Vehiculo
                    {
                        Id_vehiculo = Convert.ToInt32(reader["Id_vehiculo"]),
                        Modelo = reader["Modelo"].ToString(),
                        Color = reader["Color"].ToString(),
                        Placa = reader["Placa"].ToString(),
                        Fecha_Entre = Convert.ToDateTime(reader["Fecha_Entre"]),
                        Hora_Entre = reader["Hora_Entre"].ToString(),
                        Fecha_Salida = reader["Fecha_Salida"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Salida"]),
                        Hora_Salida = reader["Hora_Salida"].ToString(),
                        Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]),
                        Mecanico_Id = reader["Mecanico_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Id"]),
                        NombreCliente = reader["NombreCliente"].ToString(),
                        NombreMecanico = reader["NombreMecanico"] == DBNull.Value ? "Sin asignar" : reader["NombreMecanico"].ToString()
                    });
                }

                con.Close();
            }

            return lista;
        }
    }
}