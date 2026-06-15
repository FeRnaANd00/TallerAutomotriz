using Microsoft.AspNetCore.Mvc;
using TallerMecanico.EN;
using TallerMecanico.BL;
using System.Linq;

namespace TallerMecanico.UI.Controllers
{
    public class RepuestoController : Controller
    {
        // LISTAR / BUSCAR - Inventario
        public IActionResult Index(string buscar)
        {
            List<Repuesto> repuestos;
            if (!string.IsNullOrEmpty(buscar))
                repuestos = RepuestoBL.BuscarRepuesto(buscar);
            else
                repuestos = RepuestoBL.MostrarRepuestos();

            ViewBag.Buscar = buscar;
            return View(repuestos);
        }

        // AGREGAR - GET
        public IActionResult Agregar()
        {
            return View();
        }

        // AGREGAR - POST
        [HttpPost]
        public IActionResult Agregar(Repuesto repuesto)
        {
            RepuestoBL.AgregarRepuesto(repuesto);
            return RedirectToAction("Index");
        }

        // EDITAR - GET
        public IActionResult Editar(int id)
        {
            Repuesto repuesto = RepuestoBL.ObtenerRepuestoPorId(id);
            return View(repuesto);
        }

        // EDITAR - POST
        [HttpPost]
        public IActionResult Editar(Repuesto repuesto)
        {
            RepuestoBL.ModificarRepuesto(repuesto);
            return RedirectToAction("Index");
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            RepuestoBL.EliminarRepuesto(id);
            return RedirectToAction("Index");
        }
    }
}