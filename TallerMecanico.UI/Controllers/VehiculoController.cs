using Microsoft.AspNetCore.Mvc;
using TallerMecanico.BL;
using TallerMecanico.EN;

namespace TallerMecanico.UI.Controllers
{
    public class VehiculoController : Controller
    {
        public IActionResult Index(string buscar)
        {
            List<Vehiculo> vehiculos;

            if (!string.IsNullOrEmpty(buscar))
                vehiculos = VehiculoBL.BuscarVehiculo(buscar);
            else
                vehiculos = VehiculoBL.MostrarVehiculos();

            ViewBag.Buscar = buscar;

            return View(vehiculos);
        }

        public IActionResult Agregar()
        {
            ViewBag.Clientes = ClienteBL.MostrarClientes();
            ViewBag.Mecanicos = MecanicoBL.MostrarMecanicos();

            return View(new Vehiculo
            {
                Fecha_Entre = DateTime.Today
            });
        }

        [HttpPost]
        public IActionResult Agregar(Vehiculo vehiculo)
        {
            VehiculoBL.AgregarVehiculo(vehiculo);

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            ViewBag.Clientes = ClienteBL.MostrarClientes();
            ViewBag.Mecanicos = MecanicoBL.MostrarMecanicos();

            Vehiculo vehiculo = VehiculoBL.ObtenerVehiculoPorId(id);

            return View(vehiculo);
        }

        [HttpPost]
        public IActionResult Editar(Vehiculo vehiculo)
        {
            VehiculoBL.ModificarVehiculo(vehiculo);

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            VehiculoBL.EliminarVehiculo(id);

            return RedirectToAction("Index");
        }
    }
}