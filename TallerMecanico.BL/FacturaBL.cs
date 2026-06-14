using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class FacturaBL
    {
        public static int GenerarFactura(Factura factura, List<FacturaDetalle> detalles, int idHoja)
        {
            return FacturaDAL.GenerarFactura(factura, detalles, idHoja);
        }

        public static Factura ObtenerFacturaPorId(int id)
        {
            Factura factura = FacturaDAL.ObtenerFacturaPorId(id);
            factura.Detalles = FacturaDetalleDAL.ObtenerDetallesPorFactura(id);
            return factura;
        }

        public static Factura ObtenerFacturaPorHoja(int idHoja)
        {
            Factura factura = FacturaDAL.ObtenerFacturaPorHoja(idHoja);
            if (factura.Id_factura > 0)
                factura.Detalles = FacturaDetalleDAL.ObtenerDetallesPorFactura(factura.Id_factura);
            return factura;
        }

        public static List<Factura> MostrarFacturas()
        {
            return FacturaDAL.MostrarFacturas();
        }
    }
}