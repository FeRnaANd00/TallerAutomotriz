using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Usuario
    {
        public int Id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }
}
