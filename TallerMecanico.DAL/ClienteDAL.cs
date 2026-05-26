using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public static class ClienteDAL
    {
        public static int AgregarCliente(Cliente cliente)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "INSERT INTO CLIENTE(Nombre, Direccion, Telefono) VALUES(@nombre, @direccion, @telefono)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int ModificarCliente(Cliente cliente)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "UPDATE CLIENTE SET Nombre=@nombre, Direccion=@direccion, Telefono=@telefono WHERE Id_Cliente=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@id", cliente.Id_Cliente);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static int EliminarCliente(int id)
        {
            int resultado = 0;
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "DELETE FROM CLIENTE WHERE Id_Cliente=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                resultado = cmd.ExecuteNonQuery();
                con.Close();
            }
            return resultado;
        }

        public static List<Cliente> MostrarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CLIENTE";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    });
                }
                con.Close();
            }
            return clientes;
        }

        public static Cliente ObtenerClientePorId(int id)
        {
            Cliente cliente = new Cliente();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CLIENTE WHERE Id_Cliente=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente.Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]);
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Direccion = reader["Direccion"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                }
                con.Close();
            }
            return cliente;
        }

        public static List<Cliente> BuscarCliente(string texto)
        {
            List<Cliente> clientes = new List<Cliente>();
            using (MySqlConnection con = Conexion.conectar())
            {
                con.Open();
                string sql = "SELECT * FROM CLIENTE WHERE Nombre LIKE @texto OR Direccion LIKE @texto OR Telefono LIKE @texto";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    });
                }
                con.Close();
            }
            return clientes;
        }
    }
}