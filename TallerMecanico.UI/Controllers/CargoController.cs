using Microsoft.AspNetCore.Mvc;
using TallerMecanico.BL;
using TallerMecanico.EN;
using TallerMecanico.UI.Filters;

namespace TallerMecanico.UI.Controllers
{

    public class CargoController : Controller
    {
        [AuthorizeRol("Admin")]
        // LISTAR / BUSCAR
        public IActionResult Index(string buscar)
        {
            List<Cargo> cargos;
            if (!string.IsNullOrEmpty(buscar))
                cargos = CargoBL.BuscarCargo(buscar);
            else
                cargos = CargoBL.MostrarCargos();

            ViewBag.Buscar = buscar;
            return View(cargos);
        }

        // AGREGAR - GET
        public IActionResult Agregar(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // AGREGAR - POST
        [HttpPost]
        public IActionResult Agregar(Cargo cargo, string returnUrl = null)
        {
            CargoBL.AgregarCargo(cargo);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        // EDITAR - GET
        public IActionResult Editar(int id)
        {
            Cargo cargo = CargoBL.ObtenerCargoPorId(id);
            return View(cargo);
        }

        // EDITAR - POST
        [HttpPost]
        public IActionResult Editar(Cargo cargo)
        {
            CargoBL.ModificarCargo(cargo);
            return RedirectToAction("Index");
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            CargoBL.EliminarCargo(id);
            return RedirectToAction("Index");
        }
    }
}