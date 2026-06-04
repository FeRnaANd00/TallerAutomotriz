using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class CargoDAL
    {
        public static int AgregarCargo(Cargo cargo)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO CARGO(Nombre) VALUES(@nombre)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", cargo.Nombre);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarCargo(Cargo cargo)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "UPDATE CARGO SET Nombre=@nombre WHERE Id_cargo=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", cargo.Nombre);
                cmd.Parameters.AddWithValue("@id", cargo.Id_cargo);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarCargo(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM CARGO WHERE Id_cargo=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<Cargo> MostrarCargos()
        {
            List<Cargo> cargos = new List<Cargo>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CARGO";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cargos.Add(new Cargo
                    {
                        Id_cargo = Convert.ToInt32(reader["Id_cargo"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
                con.Close();
            }
            return cargos;
        }

        public static Cargo ObtenerCargoPorId(int id)
        {
            Cargo cargo = new Cargo();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CARGO WHERE Id_cargo=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cargo.Id_cargo = Convert.ToInt32(reader["Id_cargo"]);
                    cargo.Nombre = reader["Nombre"].ToString();
                }
                con.Close();
            }
            return cargo;
        }

        public static List<Cargo> BuscarCargo(string texto)
        {
            List<Cargo> cargos = new List<Cargo>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CARGO WHERE Nombre LIKE @texto";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cargos.Add(new Cargo
                    {
                        Id_cargo = Convert.ToInt32(reader["Id_cargo"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
                con.Close();
            }
            return cargos;
        }
    }
}