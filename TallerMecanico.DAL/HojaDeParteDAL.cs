using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class HojaDeParteDAL
    {
        public static int AgregarHojaDeParte(HojaDeParte hoja)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"INSERT INTO HOJA_DE_PARTE(Concepto, Cantidad, Reparacion, Mecanico_Responsable_id, Vehiculo_id, Estado) 
                               VALUES(@concepto, @cantidad, @reparacion, @mecanico, @vehiculo, 'Pendiente')";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@concepto", hoja.Concepto);
                cmd.Parameters.AddWithValue("@cantidad", hoja.Cantidad);
                cmd.Parameters.AddWithValue("@reparacion", hoja.Reparacion);
                cmd.Parameters.AddWithValue("@mecanico", hoja.Mecanico_Responsable_id.HasValue ? hoja.Mecanico_Responsable_id : DBNull.Value);
                cmd.Parameters.AddWithValue("@vehiculo", hoja.Vehiculo_id.HasValue ? hoja.Vehiculo_id : DBNull.Value);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarHojaDeParte(HojaDeParte hoja)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"UPDATE HOJA_DE_PARTE SET Concepto=@concepto, Cantidad=@cantidad, Reparacion=@reparacion, 
                               Mecanico_Responsable_id=@mecanico, Vehiculo_id=@vehiculo WHERE Id_hoja=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@concepto", hoja.Concepto);
                cmd.Parameters.AddWithValue("@cantidad", hoja.Cantidad);
                cmd.Parameters.AddWithValue("@reparacion", hoja.Reparacion);
                cmd.Parameters.AddWithValue("@mecanico", hoja.Mecanico_Responsable_id.HasValue ? hoja.Mecanico_Responsable_id : DBNull.Value);
                cmd.Parameters.AddWithValue("@vehiculo", hoja.Vehiculo_id.HasValue ? hoja.Vehiculo_id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id", hoja.Id_hoja);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarHojaDeParte(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM HOJA_DE_PARTE WHERE Id_hoja=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<HojaDeParte> MostrarHojasDeParte()
        {
            List<HojaDeParte> hojas = new List<HojaDeParte>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT h.*, m.Nombre as NombreMecanico, v.Placa as PlacaVehiculo, v.Modelo as ModeloVehiculo
                               FROM HOJA_DE_PARTE h
                               LEFT JOIN MECANICOS m ON h.Mecanico_Responsable_id = m.Id_mecanico
                               LEFT JOIN VEHICULO v ON h.Vehiculo_id = v.Id_vehiculo
                               ORDER BY h.Id_hoja DESC";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hojas.Add(new HojaDeParte
                    {
                        Id_hoja = Convert.ToInt32(reader["Id_hoja"]),
                        Concepto = reader["Concepto"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Reparacion = reader["Reparacion"].ToString(),
                        Estado = reader["Estado"] == DBNull.Value ? "Pendiente" : reader["Estado"].ToString(),
                        Mecanico_Responsable_id = reader["Mecanico_Responsable_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Responsable_id"]),
                        Vehiculo_id = reader["Vehiculo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Vehiculo_id"]),
                        NombreMecanico = reader["NombreMecanico"] == DBNull.Value ? "Sin asignar" : reader["NombreMecanico"].ToString(),
                        PlacaVehiculo = reader["PlacaVehiculo"] == DBNull.Value ? "" : reader["PlacaVehiculo"].ToString(),
                        ModeloVehiculo = reader["ModeloVehiculo"] == DBNull.Value ? "" : reader["ModeloVehiculo"].ToString()
                    });
                }
                con.Close();
            }
            return hojas;
        }

        public static HojaDeParte ObtenerHojaPorId(int id)
        {
            HojaDeParte hoja = new HojaDeParte();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT h.*, m.Nombre as NombreMecanico, v.Placa as PlacaVehiculo, v.Modelo as ModeloVehiculo
                               FROM HOJA_DE_PARTE h
                               LEFT JOIN MECANICOS m ON h.Mecanico_Responsable_id = m.Id_mecanico
                               LEFT JOIN VEHICULO v ON h.Vehiculo_id = v.Id_vehiculo
                               WHERE h.Id_hoja=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hoja.Id_hoja = Convert.ToInt32(reader["Id_hoja"]);
                    hoja.Concepto = reader["Concepto"].ToString();
                    hoja.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                    hoja.Reparacion = reader["Reparacion"].ToString();
                    hoja.Estado = reader["Estado"] == DBNull.Value ? "Pendiente" : reader["Estado"].ToString();
                    hoja.Mecanico_Responsable_id = reader["Mecanico_Responsable_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Responsable_id"]);
                    hoja.Vehiculo_id = reader["Vehiculo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Vehiculo_id"]);
                    hoja.NombreMecanico = reader["NombreMecanico"] == DBNull.Value ? "Sin asignar" : reader["NombreMecanico"].ToString();
                    hoja.PlacaVehiculo = reader["PlacaVehiculo"] == DBNull.Value ? "" : reader["PlacaVehiculo"].ToString();
                    hoja.ModeloVehiculo = reader["ModeloVehiculo"] == DBNull.Value ? "" : reader["ModeloVehiculo"].ToString();
                }
                con.Close();
            }
            return hoja;
        }

        public static List<HojaDeParte> BuscarHojaDeParte(string texto)
        {
            List<HojaDeParte> hojas = new List<HojaDeParte>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT h.*, m.Nombre as NombreMecanico, v.Placa as PlacaVehiculo, v.Modelo as ModeloVehiculo
                               FROM HOJA_DE_PARTE h
                               LEFT JOIN MECANICOS m ON h.Mecanico_Responsable_id = m.Id_mecanico
                               LEFT JOIN VEHICULO v ON h.Vehiculo_id = v.Id_vehiculo
                               WHERE h.Concepto LIKE @texto OR h.Reparacion LIKE @texto OR v.Placa LIKE @texto
                               ORDER BY h.Id_hoja DESC";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hojas.Add(new HojaDeParte
                    {
                        Id_hoja = Convert.ToInt32(reader["Id_hoja"]),
                        Concepto = reader["Concepto"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Reparacion = reader["Reparacion"].ToString(),
                        Estado = reader["Estado"] == DBNull.Value ? "Pendiente" : reader["Estado"].ToString(),
                        Mecanico_Responsable_id = reader["Mecanico_Responsable_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Mecanico_Responsable_id"]),
                        Vehiculo_id = reader["Vehiculo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Vehiculo_id"]),
                        NombreMecanico = reader["NombreMecanico"] == DBNull.Value ? "Sin asignar" : reader["NombreMecanico"].ToString(),
                        PlacaVehiculo = reader["PlacaVehiculo"] == DBNull.Value ? "" : reader["PlacaVehiculo"].ToString(),
                        ModeloVehiculo = reader["ModeloVehiculo"] == DBNull.Value ? "" : reader["ModeloVehiculo"].ToString()
                    });
                }
                con.Close();
            }
            return hojas;
        }
    }
}