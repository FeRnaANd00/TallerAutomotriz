using Microsoft.AspNetCore.Mvc;
using TallerMecanico.EN;
using TallerMecanico.BL;

namespace TallerMecanico.UI.Controllers
{
    public class FacturaController : Controller
    {
        // LISTAR TODAS LAS FACTURAS
        public IActionResult Index()
        {
            var facturas = FacturaBL.MostrarFacturas();
            return View(facturas);
        }

        // MOSTRAR FORMULARIO DE FACTURAR (desde HojaDeParte)
        public IActionResult Facturar(int idHoja)
        {
            var hoja = HojaDeParteBL.ObtenerHojaPorId(idHoja);

            if (hoja.Estado == "Facturada")
                return RedirectToAction("VerFactura", new { idHoja = idHoja });

            ViewBag.Hoja = hoja;
            ViewBag.Servicios = ServicioBL.MostrarServicios();
            ViewBag.Clientes = ClienteBL.MostrarClientes();

            return View();
        }

        // PROCESAR LA FACTURA
        [HttpPost]
        public IActionResult Facturar(int idHoja, int Cliente_Id,
            List<int> serviciosSeleccionados,
            List<string> descripcionesPersonalizadas,
            List<decimal> preciosPersonalizados,
            decimal porcentajeImpuesto)
        {
            List<FacturaDetalle> detalles = new List<FacturaDetalle>();
            decimal subtotal = 0;

            // Agregar servicios del catálogo seleccionados
            if (serviciosSeleccionados != null)
            {
                foreach (var idServicio in serviciosSeleccionados)
                {
                    var servicio = ServicioBL.ObtenerServicioPorId(idServicio);
                    detalles.Add(new FacturaDetalle
                    {
                        Id_servicio = servicio.Id_servicio,
                        Descripcion = servicio.Nombre,
                        Precio = servicio.Precio
                    });
                    subtotal += servicio.Precio;
                }
            }

            // Agregar servicios personalizados
            if (descripcionesPersonalizadas != null)
            {
                for (int i = 0; i < descripcionesPersonalizadas.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(descripcionesPersonalizadas[i]) && preciosPersonalizados[i] > 0)
                    {
                        detalles.Add(new FacturaDetalle
                        {
                            Id_servicio = null,
                            Descripcion = descripcionesPersonalizadas[i],
                            Precio = preciosPersonalizados[i]
                        });
                        subtotal += preciosPersonalizados[i];
                    }
                }
            }

            decimal impuestos = subtotal * (porcentajeImpuesto / 100);
            decimal total = subtotal + impuestos;

            Factura factura = new Factura
            {
                Subtotal = subtotal,
                Impuestos = impuestos,
                Total = total,
                Estado = "Pendiente",
                Cliente_Id = Cliente_Id
            };

            int idFactura = FacturaBL.GenerarFactura(factura, detalles, idHoja);

            if (idFactura > 0)
                return RedirectToAction("VerFactura", new { idHoja = idHoja });

            ViewBag.Error = "Hubo un error al generar la factura.";
            var hoja = HojaDeParteBL.ObtenerHojaPorId(idHoja);
            ViewBag.Hoja = hoja;
            ViewBag.Servicios = ServicioBL.MostrarServicios();
            return View();
        }

        // VER FACTURA YA GENERADA
        public IActionResult VerFactura(int idHoja)
        {
            var factura = FacturaBL.ObtenerFacturaPorHoja(idHoja);

            if (factura.Id_factura == 0)
                return RedirectToAction("Index", "HojaDeParte");

            return View(factura);
        }
    }
}