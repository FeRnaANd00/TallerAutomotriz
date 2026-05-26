using System;
using System.Collections.Generic;
using System.Text;

namespace TallerMecanico.EN
{
    public class Factura
    {
        public int Id_factura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public int Cliente_Id { get; set; }
        public int? Id_Hoja { get; set; }
    }
}
