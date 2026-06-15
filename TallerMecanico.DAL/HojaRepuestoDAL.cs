using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class HojaRepuestoDAL
    {
        // Agregar un repuesto a la hoja y descontar stock
        public static int AgregarRepuestoAHoja(HojaRepuesto hr)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // Verificar si hay suficiente stock
                    string sqlVerificar = "SELECT Stock FROM REPUESTOS WHERE Id_repuesto=@idRepuesto";
                    MySqlCommand cmdVerificar = new MySqlCommand(sqlVerificar, con, tx);
                    cmdVerificar.Parameters.AddWithValue("@idRepuesto", hr.Id_repuesto);

                    int stockActual = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (stockActual < hr.Cantidad_usada)
                    {
                        tx.Rollback();
                        return 0; // Inventario insuficiente
                    }

                    // Insertar en HOJA_REPUESTO
                    string sqlInsert = "INSERT INTO HOJA_REPUESTO(Id_hoja, Id_repuesto, Cantidad_usada) VALUES(@idHoja, @idRepuesto, @cantidad)";
                    MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, con, tx);
                    cmdInsert.Parameters.AddWithValue("@idHoja", hr.Id_hoja);
                    cmdInsert.Parameters.AddWithValue("@idRepuesto", hr.Id_repuesto);
                    cmdInsert.Parameters.AddWithValue("@cantidad", hr.Cantidad_usada);
                    resultado = cmdInsert.ExecuteNonQuery();

                    // Descontar Stock
                    string sqlStock = "UPDATE REPUESTOS SET Stock = Stock - @cantidad WHERE Id_repuesto=@idRepuesto";
                    MySqlCommand cmdStock = new MySqlCommand(sqlStock, con, tx);
                    cmdStock.Parameters.AddWithValue("@cantidad", hr.Cantidad_usada);
                    cmdStock.Parameters.AddWithValue("@idRepuesto", hr.Id_repuesto);
                    cmdStock.ExecuteNonQuery();

                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    resultado = 0;
                }

                con.Close();
            }

            return resultado;
        }

        // Eliminar un repuesto de la hoja y devolver el stock
        public static int EliminarRepuestoDeHoja(int idHojaRepuesto)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // Obtener datos antes de eliminar
                    string sqlGet = "SELECT Id_repuesto, Cantidad_usada FROM HOJA_REPUESTO WHERE Id_hoja_repuesto=@id";
                    MySqlCommand cmdGet = new MySqlCommand(sqlGet, con, tx);
                    cmdGet.Parameters.AddWithValue("@id", idHojaRepuesto);
                    IDataReader reader = cmdGet.ExecuteReader();

                    int idRepuesto = 0;
                    int cantidad = 0;
                    if (reader.Read())
                    {
                        idRepuesto = Convert.ToInt32(reader["Id_repuesto"]);
                        cantidad = Convert.ToInt32(reader["Cantidad_usada"]);
                    }
                    reader.Close();

                    // Devolver el stock
                    string sqlStock = "UPDATE REPUESTOS SET Stock = Stock + @cantidad WHERE Id_repuesto=@idRepuesto";
                    MySqlCommand cmdStock = new MySqlCommand(sqlStock, con, tx);
                    cmdStock.Parameters.AddWithValue("@cantidad", cantidad);
                    cmdStock.Parameters.AddWithValue("@idRepuesto", idRepuesto);
                    cmdStock.ExecuteNonQuery();

                    // Eliminar el registro
                    string sqlDelete = "DELETE FROM HOJA_REPUESTO WHERE Id_hoja_repuesto=@id";
                    MySqlCommand cmdDelete = new MySqlCommand(sqlDelete, con, tx);
                    cmdDelete.Parameters.AddWithValue("@id", idHojaRepuesto);
                    resultado = cmdDelete.ExecuteNonQuery();

                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    resultado = 0;
                }

                con.Close();
            }
            return resultado;
        }

        // Obtener repuestos usados en una hoja
        public static List<HojaRepuesto> ObtenerRepuestosPorHoja(int idHoja)
        {
            List<HojaRepuesto> lista = new List<HojaRepuesto>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT hr.*, r.Descripcion as DescripcionRepuesto, r.Precio_Uni as PrecioUnitario, r.Stock as StockDisponible
                               FROM HOJA_REPUESTO hr
                               INNER JOIN REPUESTOS r ON hr.Id_repuesto = r.Id_repuesto
                               WHERE hr.Id_hoja=@idHoja";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idHoja", idHoja);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new HojaRepuesto
                    {
                        Id_hoja_repuesto = Convert.ToInt32(reader["Id_hoja_repuesto"]),
                        Id_hoja = Convert.ToInt32(reader["Id_hoja"]),
                        Id_repuesto = Convert.ToInt32(reader["Id_repuesto"]),
                        Cantidad_usada = Convert.ToInt32(reader["Cantidad_usada"]),
                        DescripcionRepuesto = reader["DescripcionRepuesto"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        StockDisponible = Convert.ToInt32(reader["StockDisponible"])
                    });
                }
                con.Close();
            }
            return lista;
        }
    }
}