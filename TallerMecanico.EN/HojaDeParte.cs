using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class HojaDeParte
    {
        public int Id_hoja { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Reparacion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public int? Mecanico_Responsable_id { get; set; }
        public int? Vehiculo_id { get; set; }        // ← este falta
        public DateTime Fecha_Creacion { get; set; }

        // Campos extra para vistas
        public string NombreMecanico { get; set; } = string.Empty;
        public string ModeloVehiculo { get; set; } = string.Empty;
        public string PlacaVehiculo { get; set; } = string.Empty;
    }
}
