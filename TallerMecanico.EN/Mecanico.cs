using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Mecanico
    {
        public int Id_mecanico { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal Pago_Hora { get; set; }
        public string Categoria { get; set; }
    }
}
