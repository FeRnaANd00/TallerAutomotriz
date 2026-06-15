using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using TallerMecanico.EN;

namespace TallerMecanico.DAL
{
    public class UsuarioDAL
    {
        public static Usuario Login(string username, string password)
        {
            Usuario usuario = null;

            try
            {
                using (MySqlConnection con = Conexion.conectar())
                {
                    string query = @"SELECT Id_usuario, Nombre, Username, Email, Rol, Activo
                                     FROM Usuario
                                     WHERE Username = @Username
                                       AND Password = @Password
                                       AND Activo = 1";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // ya viene hasheada desde BL

                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id_usuario = Convert.ToInt32(reader["Id_usuario"]),
                            Nombre = reader["Nombre"].ToString(),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL Login: " + ex.Message);
            }

            return usuario;
        }

        public static bool UsernameExiste(string username)
        {
            try
            {
                using (MySqlConnection con = Conexion.conectar())
                {
                    string query = "SELECT COUNT(*) FROM Usuario WHERE Username = @Username";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL UsernameExiste: " + ex.Message);
            }
        }
    }
}