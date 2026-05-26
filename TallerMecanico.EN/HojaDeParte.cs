using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class HojaDeParte
    {
        public int Id_hoja { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public string Reparacion { get; set; }
        public int? Mecanico_Responsable_id { get; set; }
    }
}
