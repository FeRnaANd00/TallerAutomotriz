using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class HojaRepuesto
    {
        public int Id_hoja_repuesto { get; set; }
        public int Id_hoja { get; set; }
        public int Id_repuesto { get; set; }
        public int Cantidad_usada { get; set; }

        // Campos extra para vistas
        public string DescripcionRepuesto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
    }
}