using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using TallerMecanico.BL;
using TallerMecanico.EN;

namespace TallerMecanico.UI.Controllers
{
    public class MecanicoController : Controller
    {
        // LISTAR
        public IActionResult Index()
        {
            var lista =
                MecanicoBL.MostrarMecanicos();

            return View(lista);
        }

        /// FORMULARIO AGREGAR
        public IActionResult Agregar(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Cargos = new SelectList(CargoBL.MostrarCargos(), "Id_cargo", "Nombre");
            return View();
        }

        // GUARDAR AGREGAR
        [HttpPost]
        public IActionResult Agregar(Mecanico mecanico, string returnUrl = null)
        {
            if (mecanico.Cargo_id == null)
            {
                ViewBag.Error = "Debes seleccionar un Cargo o crear uno nuevo.";
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Cargos = new SelectList(CargoBL.MostrarCargos(), "Id_cargo", "Nombre");
                return View(mecanico);
            }
            MecanicoBL.AgregarMecanico(mecanico);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        // MOSTRAR EDITAR
        public IActionResult Editar(int id)
        {
            Mecanico mecanico = MecanicoBL.ObtenerMecanicoPorId(id);
            ViewBag.Cargos = new SelectList(CargoBL.MostrarCargos(), "Id_cargo", "Nombre", mecanico.Cargo_id);
            return View(mecanico);
        }

        // GUARDAR EDITAR
        [HttpPost]
        public IActionResult Editar(Mecanico mecanico)
        {
            MecanicoBL.ModificarMecanico(mecanico);
            return RedirectToAction("Index");
        }

        // MOSTRAR ELIMINAR
        public IActionResult Eliminar(int id)
        {
            var mecanico =
                MecanicoBL.ObtenerMecanicoPorId(id);

            return View(mecanico);
        }

        // CONFIRMAR ELIMINAR
        [HttpPost]
        public IActionResult Eliminar(Mecanico mecanico)
        {
            MecanicoBL.EliminarMecanico(mecanico.Id_mecanico);

            return RedirectToAction("Index");
        }
    }
}
