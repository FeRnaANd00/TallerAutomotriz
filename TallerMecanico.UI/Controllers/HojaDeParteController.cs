using Microsoft.AspNetCore.Mvc;
using TallerMecanico.EN;
using TallerMecanico.BL;

namespace TallerMecanico.UI.Controllers
{
    public class HojaDeParteController : Controller
    {
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
    }
}