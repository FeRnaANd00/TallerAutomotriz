using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using TallerMecanico.DAL;
using TallerMecanico.EN;

namespace TallerMecanico.BL
{
    public class UsuarioBL
    {
        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static Usuario Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("El usuario y la contraseña son obligatorios.");

            string passwordHash = HashPassword(password);
            Usuario usuario = UsuarioDAL.Login(username, passwordHash);

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos, o cuenta inactiva.");

            return usuario;
        }

        public static bool UsernameExiste(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("El username no puede estar vacio.");

            return UsuarioDAL.UsernameExiste(username);
        }
    }
}