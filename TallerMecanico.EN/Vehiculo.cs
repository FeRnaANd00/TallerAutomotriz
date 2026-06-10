using System;

namespace TallerMecanico.EN
{
    public class Vehiculo
    {
        public int Id_vehiculo { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public DateTime Fecha_Entre { get; set; }
        public string Hora_Entre { get; set; } = string.Empty;
        public DateTime? Fecha_Salida { get; set; }
        public string Hora_Salida { get; set; } = string.Empty;
        public int Cliente_Id { get; set; }
        public int? Mecanico_Id { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        // Campos extra para mostrar en vistas
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreMecanico { get; set; } = string.Empty;
    }
}