using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class FacturaDetalleDAL
    {
        // Se usa internamente al crear una factura
        public static int AgregarDetalle(FacturaDetalle detalle)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO FACTURA_DETALLE(Id_factura, Id_servicio, Descripcion, Precio) VALUES(@idFactura, @idServicio, @descripcion, @precio)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idFactura", detalle.Id_factura);
                cmd.Parameters.AddWithValue("@idServicio", detalle.Id_servicio.HasValue ? detalle.Id_servicio : DBNull.Value);
                cmd.Parameters.AddWithValue("@descripcion", detalle.Descripcion);
                cmd.Parameters.AddWithValue("@precio", detalle.Precio);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        // Para mostrar el detalle de una factura (solo lectura)
        public static List<FacturaDetalle> ObtenerDetallesPorFactura(int idFactura)
        {
            List<FacturaDetalle> detalles = new List<FacturaDetalle>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM FACTURA_DETALLE WHERE Id_factura=@idFactura";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idFactura", idFactura);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    detalles.Add(new FacturaDetalle
                    {
                        Id_detalle = Convert.ToInt32(reader["Id_detalle"]),
                        Id_factura = Convert.ToInt32(reader["Id_factura"]),
                        Id_servicio = reader["Id_servicio"] == DBNull.Value ? null : Convert.ToInt32(reader["Id_servicio"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"])
                    });
                }
                con.Close();
            }
            return detalles;
        }
    }
}