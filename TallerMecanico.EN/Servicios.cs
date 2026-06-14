using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Servicio
    {
        public int Id_servicio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
