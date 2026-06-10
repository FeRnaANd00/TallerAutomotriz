using Microsoft.AspNetCore.Mvc;
using TallerMecanico.EN;
using TallerMecanico.BL;

namespace TallerMecanico.UI.Controllers
{
    public class ClienteController : Controller
    {
        // LISTAR / BUSCAR
        public IActionResult Index(string buscar)
        {
            List<Cliente> clientes;
            if (!string.IsNullOrEmpty(buscar))
                clientes = ClienteBL.BuscarCliente(buscar);
            else
                clientes = ClienteBL.MostrarClientes();

            ViewBag.Buscar = buscar;
            return View(clientes);
        }

        // AGREGAR - GET
        public IActionResult Agregar(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // AGREGAR - POST
        [HttpPost]
        public IActionResult Agregar(Cliente cliente, string returnUrl = null)
        {
            ClienteBL.AgregarCliente(cliente);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        // EDITAR - GET
        public IActionResult Editar(int id)
        {
            Cliente cliente = ClienteBL.ObtenerClientePorId(id);
            return View(cliente);
        }

        // EDITAR - POST
        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            ClienteBL.ModificarCliente(cliente);
            return RedirectToAction("Index");
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            ClienteBL.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }
}