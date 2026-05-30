using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
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

        // FORMULARIO
        public IActionResult Agregar()
        {
            return View();
        }

        // GUARDAR
        [HttpPost]
        public IActionResult Agregar(Mecanico mecanico)
        {
            MecanicoBL.AgregarMecanico(mecanico);

            return RedirectToAction("Index");
        }
        // MOSTRAR FORMULARIO EDITAR
        public IActionResult Editar(int id)
        {
            var mecanico =
                MecanicoBL.ObtenerMecanicoPorId(id);

            return View(mecanico);
        }

        // GUARDAR EDICION
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
