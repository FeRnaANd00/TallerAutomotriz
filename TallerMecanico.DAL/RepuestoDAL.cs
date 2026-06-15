using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class RepuestoDAL
    {
        public static int AgregarRepuesto(Repuesto repuesto)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO REPUESTOS(Descripcion, Costo_Uni, Precio_Uni, Imp_parcial, Stock) VALUES(@descripcion, @costo, @precio, @imp, @stock)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@descripcion", repuesto.Descripcion);
                cmd.Parameters.AddWithValue("@costo", repuesto.Costo_Uni);
                cmd.Parameters.AddWithValue("@precio", repuesto.Precio_Uni);
                cmd.Parameters.AddWithValue("@imp", repuesto.Imp_parcial);
                cmd.Parameters.AddWithValue("@stock", repuesto.Stock);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarRepuesto(Repuesto repuesto)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "UPDATE REPUESTOS SET Descripcion=@descripcion, Costo_Uni=@costo, Precio_Uni=@precio, Imp_parcial=@imp, Stock=@stock WHERE Id_repuesto=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@descripcion", repuesto.Descripcion);
                cmd.Parameters.AddWithValue("@costo", repuesto.Costo_Uni);
                cmd.Parameters.AddWithValue("@precio", repuesto.Precio_Uni);
                cmd.Parameters.AddWithValue("@imp", repuesto.Imp_parcial);
                cmd.Parameters.AddWithValue("@stock", repuesto.Stock);
                cmd.Parameters.AddWithValue("@id", repuesto.Id_repuesto);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarRepuesto(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM REPUESTOS WHERE Id_repuesto=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<Repuesto> MostrarRepuestos()
        {
            List<Repuesto> repuestos = new List<Repuesto>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM REPUESTOS ORDER BY Descripcion";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    repuestos.Add(new Repuesto
                    {
                        Id_repuesto = Convert.ToInt32(reader["Id_repuesto"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        Costo_Uni = Convert.ToDecimal(reader["Costo_Uni"]),
                        Precio_Uni = Convert.ToDecimal(reader["Precio_Uni"]),
                        Imp_parcial = Convert.ToDecimal(reader["Imp_parcial"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        Hoja_de_parte_id = reader["Hoja_de_parte_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Hoja_de_parte_id"])
                    });
                }
                con.Close();
            }
            return repuestos;
        }

        public static Repuesto ObtenerRepuestoPorId(int id)
        {
            Repuesto repuesto = new Repuesto();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM REPUESTOS WHERE Id_repuesto=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    repuesto.Id_repuesto = Convert.ToInt32(reader["Id_repuesto"]);
                    repuesto.Descripcion = reader["Descripcion"].ToString();
                    repuesto.Costo_Uni = Convert.ToDecimal(reader["Costo_Uni"]);
                    repuesto.Precio_Uni = Convert.ToDecimal(reader["Precio_Uni"]);
                    repuesto.Imp_parcial = Convert.ToDecimal(reader["Imp_parcial"]);
                    repuesto.Stock = Convert.ToInt32(reader["Stock"]);
                    repuesto.Hoja_de_parte_id = reader["Hoja_de_parte_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Hoja_de_parte_id"]);
                }
                con.Close();
            }
            return repuesto;
        }

        public static List<Repuesto> BuscarRepuesto(string texto)
        {
            List<Repuesto> repuestos = new List<Repuesto>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM REPUESTOS WHERE Descripcion LIKE @texto ORDER BY Descripcion";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    repuestos.Add(new Repuesto
                    {
                        Id_repuesto = Convert.ToInt32(reader["Id_repuesto"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        Costo_Uni = Convert.ToDecimal(reader["Costo_Uni"]),
                        Precio_Uni = Convert.ToDecimal(reader["Precio_Uni"]),
                        Imp_parcial = Convert.ToDecimal(reader["Imp_parcial"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        Hoja_de_parte_id = reader["Hoja_de_parte_id"] == DBNull.Value ? null : Convert.ToInt32(reader["Hoja_de_parte_id"])
                    });
                }
                con.Close();
            }
            return repuestos;
        }
    }
}