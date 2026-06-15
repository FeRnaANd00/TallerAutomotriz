using Microsoft.AspNetCore.Mvc;
using TallerMecanico.BL;
using TallerMecanico.EN;
using System;

namespace TallerMecanico.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login
        public IActionResult Index()
        {
            // Si ya hay sesion activa, redirigir
            if (HttpContext.Session.GetString("Rol") != null)
                return RedirectSegunRol(HttpContext.Session.GetString("Rol"));

            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string username, string password)
        {
            try
            {
                Usuario usuario = UsuarioBL.Login(username, password);

                // Guardar en sesion
                HttpContext.Session.SetInt32("Id_usuario", usuario.Id_usuario);
                HttpContext.Session.SetString("Nombre", usuario.Nombre);
                HttpContext.Session.SetString("Username", usuario.Username);
                HttpContext.Session.SetString("Rol", usuario.Rol);

                return RedirectSegunRol(usuario.Rol);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // Cerrar sesion
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return RedirectToAction("Index", "Login");
        }

        private IActionResult RedirectSegunRol(string rol)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}