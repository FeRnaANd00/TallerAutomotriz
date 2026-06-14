using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class FacturaDetalle
    {
        public int Id_detalle { get; set; }
        public int Id_factura { get; set; }
        public int? Id_servicio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }
}