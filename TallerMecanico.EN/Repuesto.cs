using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Repuesto
    {
        public int Id_repuesto { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo_Uni { get; set; }
        public decimal Precio_Uni { get; set; }
        public decimal Imp_parcial { get; set; }
        public int Stock { get; set; }
        public int? Hoja_de_parte_id { get; set; }
    }
}
