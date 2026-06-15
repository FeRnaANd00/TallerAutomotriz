using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TallerMecanico.UI.Filters;
using TallerMecanico.UI.Models;

namespace TallerMecanico.UI.Controllers
{
    [AuthorizeRol("Admin", "Mecanico", "Recepcion")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
