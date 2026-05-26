using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Vehiculo
    {
        public int Id_vehiculo { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public DateTime Fecha_Entre { get; set; }
        public string Hora_Entre { get; set; }
        public int Cliente_Id { get; set; }
        public int? Mecanico_responsable_id { get; set; }
    }
}
