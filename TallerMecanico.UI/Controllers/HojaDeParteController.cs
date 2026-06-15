using Microsoft.AspNetCore.Mvc;
using TallerMecanico.EN;
using TallerMecanico.BL;
using Microsoft.AspNetCore.Mvc.Rendering;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;

namespace TallerMecanico.UI.Controllers
{
    public class HojaDeParteController : Controller
    {
        public IActionResult GenerarReporte()
        {
            var hojas = HojaDeParteBL.MostrarHojasDeParte();

            var fuenteNormal = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            var fuenteBold = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Reporte de Hojas de Parte")
                .SetFontSize(18)
                .SetFont(fuenteBold)
                .SetTextAlignment(TextAlignment.CENTER));

            document.Add(new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}")
                .SetFontSize(10)
                .SetFont(fuenteNormal)
                .SetTextAlignment(TextAlignment.CENTER));

            document.Add(new Paragraph("\n"));

            Table tabla = new Table(UnitValue.CreatePercentArray(
    new float[] { 1, 4, 1, 3, 2, 2 }))
    .UseAllAvailableWidth();

            string[] encabezados = { "ID", "Concepto", "Cantidad", "Mecánico", "Vehiculo", "Estado" };
            foreach (var enc in encabezados)
            {
                tabla.AddHeaderCell(new Cell()
                    .Add(new Paragraph(enc).SetFont(fuenteBold))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetFontColor(ColorConstants.WHITE)
                    .SetTextAlignment(TextAlignment.CENTER));
            }

            bool filaPar = false;
            foreach (var h in hojas)
            {
                var color = filaPar ? ColorConstants.LIGHT_GRAY : ColorConstants.WHITE;
                tabla.AddCell(new Cell().Add(new Paragraph(h.Id_hoja.ToString()).SetFont(fuenteNormal)).SetBackgroundColor(color).SetTextAlignment(TextAlignment.CENTER));
                tabla.AddCell(new Cell().Add(new Paragraph(h.Concepto ?? "").SetFont(fuenteNormal)).SetBackgroundColor(color));
                tabla.AddCell(new Cell().Add(new Paragraph(h.Cantidad.ToString()).SetFont(fuenteNormal)).SetBackgroundColor(color).SetTextAlignment(TextAlignment.CENTER));
                tabla.AddCell(new Cell().Add(new Paragraph(h.NombreMecanico ?? "Sin asignar").SetFont(fuenteNormal)).SetBackgroundColor(color));
                tabla.AddCell(new Cell().Add(new Paragraph(h.PlacaVehiculo ?? "").SetFont(fuenteNormal)).SetBackgroundColor(color));
                tabla.AddCell(new Cell().Add(new Paragraph(h.Estado.ToString()).SetFont(fuenteNormal)).SetBackgroundColor(color));
                filaPar = !filaPar;
            }

            document.Add(tabla);
            document.Close();

            Response.Headers["Content-Disposition"] = "inline; filename=ReporteHojasDeParte.pdf";
            return File(stream.ToArray(), "application/pdf");
        }

        // LISTAR / BUSCAR
        public IActionResult Index(string buscar)
        {
            List<HojaDeParte> hojas;
            if (!string.IsNullOrEmpty(buscar))
                hojas = HojaDeParteBL.BuscarHojaDeParte(buscar);
            else
                hojas = HojaDeParteBL.MostrarHojasDeParte();

            ViewBag.Buscar = buscar;
            return View(hojas);
        }

        // AGREGAR - GET
        public IActionResult Agregar()
        {
            ViewBag.Mecanicos = new SelectList(MecanicoBL.MostrarMecanicos(), "Id_mecanico", "Nombre");
            ViewBag.Vehiculos = new SelectList(VehiculoBL.MostrarVehiculos(), "Id_vehiculo", "Placa");
            return View();
        }

        // AGREGAR - POST
        [HttpPost]
        public IActionResult Agregar(HojaDeParte hoja)
        {
            HojaDeParteBL.AgregarHojaDeParte(hoja);
            return RedirectToAction("Index");
        }

        // EDITAR - GET
        public IActionResult Editar(int id)
        {
            HojaDeParte hoja = HojaDeParteBL.ObtenerHojaPorId(id);
            ViewBag.Mecanicos = new SelectList(MecanicoBL.MostrarMecanicos(), "Id_mecanico", "Nombre", hoja.Mecanico_Responsable_id);
            ViewBag.Vehiculos = new SelectList(VehiculoBL.MostrarVehiculos(), "Id_vehiculo", "Placa", hoja.Vehiculo_id);
            ViewBag.Repuestos = RepuestoBL.MostrarRepuestos();
            ViewBag.RepuestosUsados = HojaRepuestoBL.ObtenerRepuestosPorHoja(id);
            return View(hoja);
        }

        // EDITAR - POST
        [HttpPost]
        public IActionResult Editar(HojaDeParte hoja)
        {
            HojaDeParteBL.ModificarHojaDeParte(hoja);
            return RedirectToAction("Index");
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            HojaDeParteBL.EliminarHojaDeParte(id);
            return RedirectToAction("Index");
        }

        //AGREGAR REPUESTO
        // AGREGAR REPUESTO A LA HOJA
        [HttpPost]
        public IActionResult AgregarRepuesto(int idHoja, int idRepuesto, int cantidad)
        {
            HojaRepuesto hr = new HojaRepuesto
            {
                Id_hoja = idHoja,
                Id_repuesto = idRepuesto,
                Cantidad_usada = cantidad
            };

            int resultado = HojaRepuestoBL.AgregarRepuestoAHoja(hr);

            if (resultado == 0)
            {
                TempData["Error"] = "Inventario insuficiente para agregar este repuesto.";
            }

            return RedirectToAction("Editar", new { id = idHoja });
        }

        // QUITAR REPUESTO DE LA HOJA
        public IActionResult QuitarRepuesto(int idHojaRepuesto, int idHoja)
        {
            HojaRepuestoBL.EliminarRepuestoDeHoja(idHojaRepuesto);

            return RedirectToAction("Editar", new { id = idHoja });
        }
    }
}