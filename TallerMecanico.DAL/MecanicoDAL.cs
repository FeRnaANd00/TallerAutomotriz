using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class MecanicoDAL
    {
        public static int AgregarMecanico(Mecanico mecanico)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO MECANICOS(Nombre, Direccion, Telefono, Pago_Hora, Cargo_id) VALUES(@nombre, @direccion, @telefono, @pago, @cargo)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", mecanico.Nombre);
                cmd.Parameters.AddWithValue("@direccion", mecanico.Direccion);
                cmd.Parameters.AddWithValue("@telefono", mecanico.Telefono);
                cmd.Parameters.AddWithValue("@pago", mecanico.Pago_Hora);
                cmd.Parameters.AddWithValue("@cargo", mecanico.Cargo_id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarMecanico(Mecanico mecanico)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "UPDATE MECANICOS SET Nombre=@nombre, Direccion=@direccion, Telefono=@telefono, Pago_Hora=@pago, Cargo_id=@cargo WHERE Id_mecanico=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", mecanico.Nombre);
                cmd.Parameters.AddWithValue("@direccion", mecanico.Direccion);
                cmd.Parameters.AddWithValue("@telefono", mecanico.Telefono);
                cmd.Parameters.AddWithValue("@pago", mecanico.Pago_Hora);
                cmd.Parameters.AddWithValue("@cargo", mecanico.Cargo_id);
                cmd.Parameters.AddWithValue("@id", mecanico.Id_mecanico);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarMecanico(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM MECANICOS WHERE Id_mecanico=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<Mecanico> MostrarMecanicos()
        {
            List<Mecanico> mecanicos = new List<Mecanico>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT m.*, c.Nombre as NombreCargo 
                       FROM MECANICOS m
                       LEFT JOIN CARGO c ON m.Cargo_id = c.Id_cargo";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mecanicos.Add(new Mecanico
                    {
                        Id_mecanico = Convert.ToInt32(reader["Id_mecanico"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Pago_Hora = Convert.ToDecimal(reader["Pago_Hora"]),
                        Cargo_id = reader["Cargo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Cargo_id"]),
                        NombreCargo = reader["NombreCargo"] == DBNull.Value ? "Sin cargo" : reader["NombreCargo"].ToString()
                    });
                }
                con.Close();
            }
            return mecanicos;
        }

        public static Mecanico ObtenerMecanicoPorId(int id)
        {
            Mecanico mecanico = new Mecanico();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT m.*, c.Nombre as NombreCargo 
                       FROM MECANICOS m
                       LEFT JOIN CARGO c ON m.Cargo_id = c.Id_cargo
                       WHERE m.Id_mecanico=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    mecanico.Id_mecanico = Convert.ToInt32(reader["Id_mecanico"]);
                    mecanico.Nombre = reader["Nombre"].ToString();
                    mecanico.Direccion = reader["Direccion"].ToString();
                    mecanico.Telefono = reader["Telefono"].ToString();
                    mecanico.Pago_Hora = Convert.ToDecimal(reader["Pago_Hora"]);
                    mecanico.Cargo_id = reader["Cargo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Cargo_id"]);
                    mecanico.NombreCargo = reader["NombreCargo"] == DBNull.Value ? "Sin cargo" : reader["NombreCargo"].ToString();
                }
                con.Close();
            }
            return mecanico;
        }

        public static List<Mecanico> BuscarMecanico(string texto)
        {
            List<Mecanico> mecanicos = new List<Mecanico>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT m.*, c.Nombre as NombreCargo 
                       FROM MECANICOS m
                       LEFT JOIN CARGO c ON m.Cargo_id = c.Id_cargo
                       WHERE m.Nombre LIKE @texto 
                       OR m.Telefono LIKE @texto 
                       OR c.Nombre LIKE @texto";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mecanicos.Add(new Mecanico
                    {
                        Id_mecanico = Convert.ToInt32(reader["Id_mecanico"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Pago_Hora = Convert.ToDecimal(reader["Pago_Hora"]),
                        Cargo_id = reader["Cargo_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Cargo_id"]),
                        NombreCargo = reader["NombreCargo"] == DBNull.Value ? "Sin cargo" : reader["NombreCargo"].ToString()
                    });
                }
                con.Close();
            }
            return mecanicos;
        }
    }
}