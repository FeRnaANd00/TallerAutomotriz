using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class FacturaDAL
    {
        // GENERAR FACTURA COMPLETA (factura + detalles + descuento de stock + cambio de estado)
        public static int GenerarFactura(Factura factura, List<FacturaDetalle> detalles, int idHoja)
        {
            int idFacturaGenerada = 0;

            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // 1. Insertar la FACTURA
                    string sqlFactura = @"INSERT INTO FACTURA(Subtotal, Impuestos, Total, Estado, Cliente_Id, Id_Hoja) 
                                           VALUES(@subtotal, @impuestos, @total, @estado, @cliente, @idHoja)";
                    MySqlCommand cmdFactura = new MySqlCommand(sqlFactura, con, tx);
                    cmdFactura.Parameters.AddWithValue("@subtotal", factura.Subtotal);
                    cmdFactura.Parameters.AddWithValue("@impuestos", factura.Impuestos);
                    cmdFactura.Parameters.AddWithValue("@total", factura.Total);
                    cmdFactura.Parameters.AddWithValue("@estado", factura.Estado);
                    cmdFactura.Parameters.AddWithValue("@cliente", factura.Cliente_Id);
                    cmdFactura.Parameters.AddWithValue("@idHoja", idHoja);
                    cmdFactura.ExecuteNonQuery();

                    // Obtener el ID de la factura recién creada
                    cmdFactura.CommandText = "SELECT LAST_INSERT_ID()";
                    idFacturaGenerada = Convert.ToInt32(cmdFactura.ExecuteScalar());

                    // 2. Insertar cada DETALLE
                    foreach (var detalle in detalles)
                    {
                        string sqlDetalle = @"INSERT INTO FACTURA_DETALLE(Id_factura, Id_servicio, Descripcion, Precio) 
                                               VALUES(@idFactura, @idServicio, @descripcion, @precio)";
                        MySqlCommand cmdDetalle = new MySqlCommand(sqlDetalle, con, tx);
                        cmdDetalle.Parameters.AddWithValue("@idFactura", idFacturaGenerada);
                        cmdDetalle.Parameters.AddWithValue("@idServicio", detalle.Id_servicio.HasValue ? detalle.Id_servicio : DBNull.Value);
                        cmdDetalle.Parameters.AddWithValue("@descripcion", detalle.Descripcion);
                        cmdDetalle.Parameters.AddWithValue("@precio", detalle.Precio);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    // 3. Descontar STOCK de los repuestos usados en esa hoja
                    string sqlRepuestos = "SELECT Id_repuesto FROM REPUESTOS WHERE Hoja_de_parte_id=@idHoja";
                    MySqlCommand cmdRepuestos = new MySqlCommand(sqlRepuestos, con, tx);
                    cmdRepuestos.Parameters.AddWithValue("@idHoja", idHoja);

                    List<int> idsRepuestos = new List<int>();
                    IDataReader readerRep = cmdRepuestos.ExecuteReader();
                    while (readerRep.Read())
                    {
                        idsRepuestos.Add(Convert.ToInt32(readerRep["Id_repuesto"]));
                    }
                    readerRep.Close();

                    foreach (var idRepuesto in idsRepuestos)
                    {
                        string sqlDescuento = "UPDATE REPUESTOS SET Stock = Stock - 1 WHERE Id_repuesto=@id AND Stock > 0";
                        MySqlCommand cmdDescuento = new MySqlCommand(sqlDescuento, con, tx);
                        cmdDescuento.Parameters.AddWithValue("@id", idRepuesto);
                        cmdDescuento.ExecuteNonQuery();
                    }

                    // 4. Cambiar ESTADO de la HOJA_DE_PARTE a "Facturada"
                    string sqlEstado = "UPDATE HOJA_DE_PARTE SET Estado='Facturada' WHERE Id_hoja=@idHoja";
                    MySqlCommand cmdEstado = new MySqlCommand(sqlEstado, con, tx);
                    cmdEstado.Parameters.AddWithValue("@idHoja", idHoja);
                    cmdEstado.ExecuteNonQuery();

                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    idFacturaGenerada = 0;
                }

                con.Close();
            }

            return idFacturaGenerada;
        }

        // OBTENER FACTURA POR ID (con datos de cliente)
        public static Factura ObtenerFacturaPorId(int id)
        {
            Factura factura = new Factura();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT f.*, c.Nombre as NombreCliente
                               FROM FACTURA f
                               INNER JOIN CLIENTE c ON f.Cliente_Id = c.Id_Cliente
                               WHERE f.Id_factura=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    factura.Id_factura = Convert.ToInt32(reader["Id_factura"]);
                    factura.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    factura.Subtotal = Convert.ToDecimal(reader["Subtotal"]);
                    factura.Impuestos = Convert.ToDecimal(reader["Impuestos"]);
                    factura.Total = Convert.ToDecimal(reader["Total"]);
                    factura.Estado = reader["Estado"].ToString();
                    factura.Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]);
                    factura.Id_Hoja = reader["Id_Hoja"] == DBNull.Value ? null : Convert.ToInt32(reader["Id_Hoja"]);
                    factura.NombreCliente = reader["NombreCliente"].ToString();
                }
                con.Close();
            }
            return factura;
        }

        // OBTENER FACTURA POR ID DE HOJA (para botón "Ver Factura")
        public static Factura ObtenerFacturaPorHoja(int idHoja)
        {
            Factura factura = new Factura();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT f.*, c.Nombre as NombreCliente
                               FROM FACTURA f
                               INNER JOIN CLIENTE c ON f.Cliente_Id = c.Id_Cliente
                               WHERE f.Id_Hoja=@idHoja";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idHoja", idHoja);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    factura.Id_factura = Convert.ToInt32(reader["Id_factura"]);
                    factura.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    factura.Subtotal = Convert.ToDecimal(reader["Subtotal"]);
                    factura.Impuestos = Convert.ToDecimal(reader["Impuestos"]);
                    factura.Total = Convert.ToDecimal(reader["Total"]);
                    factura.Estado = reader["Estado"].ToString();
                    factura.Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]);
                    factura.Id_Hoja = reader["Id_Hoja"] == DBNull.Value ? null : Convert.ToInt32(reader["Id_Hoja"]);
                    factura.NombreCliente = reader["NombreCliente"].ToString();
                }
                con.Close();
            }
            return factura;
        }

        // LISTAR TODAS LAS FACTURAS
        public static List<Factura> MostrarFacturas()
        {
            List<Factura> facturas = new List<Factura>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = @"SELECT f.*, c.Nombre as NombreCliente
                               FROM FACTURA f
                               INNER JOIN CLIENTE c ON f.Cliente_Id = c.Id_Cliente
                               ORDER BY f.Fecha DESC";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    facturas.Add(new Factura
                    {
                        Id_factura = Convert.ToInt32(reader["Id_factura"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                        Impuestos = Convert.ToDecimal(reader["Impuestos"]),
                        Total = Convert.ToDecimal(reader["Total"]),
                        Estado = reader["Estado"].ToString(),
                        Cliente_Id = Convert.ToInt32(reader["Cliente_Id"]),
                        Id_Hoja = reader["Id_Hoja"] == DBNull.Value ? null : Convert.ToInt32(reader["Id_Hoja"]),
                        NombreCliente = reader["NombreCliente"].ToString()
                    });
                }
                con.Close();
            }
            return facturas;
        }
    }
}