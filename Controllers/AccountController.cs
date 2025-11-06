using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public IActionResult IniciarSesion()
        {
            return View("IniciarSesion");
        }

        [HttpPost]
        public IActionResult RecibirInicioDeSesion(string nombreUsuario, string contraseña)
        {
            Usuarios usuario = BD.Login(nombreUsuario, contraseña);

            if (usuario == null)
            {
                return RedirectToAction("IniciarSesion", "Account");
            }

            HttpContext.Session.SetString("user", Objeto.ObjectToString(usuario));

            if (usuario.Rol == "Cliente")
                return RedirectToAction("HomeCliente", "Cliente");
            else if (usuario.Rol == "Dueño")
                return RedirectToAction("HomeDueño", "Dueño");

            return RedirectToAction("IniciarSesion", "Account");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("IniciarSesion", "Account");
        }

        public IActionResult RegistrarseCliente()
        {
            ViewBag.provincias=BD.TraerProvincias();
            return View("RegistrarseCliente");
        }

        [HttpPost]
        public IActionResult RecibirRegistroCliente(string username, string password, string mail, string rol, int idProvincia)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(rol))
            {
                Usuarios usuario = new Usuarios(username, password, mail, rol, DateTime.Now);
                int idUsuario = BD.registrar(usuario);

                if (idUsuario != -1)
                {
                    Clientes cliente = new Clientes(idProvincia, idUsuario);
                    BD.CrearCliente(cliente);
                    HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
                    return RedirectToAction("HomeCliente", "Cliente");
                }
            }

            return RedirectToAction("RegistrarseCliente", "Account");
        }
        public IActionResult RegistrarseDueño()
        {
            ViewBag.locales=BD.MostrarLocales();
            return View("RegistrarseDueño");
        }
        [HttpPost]
        public IActionResult RecibirRegistroDueño(string username, string password, string mail, string rol, int idLocal)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(rol))
            {
                Usuarios usuario = new Usuarios(username, password, mail, rol, DateTime.Now);
                int idUsuario = BD.registrar(usuario);

                if (idUsuario != -1)
                {
                    Dueños dueño = new Dueños(idLocal, idUsuario);
                    BD.CrearDueño(dueño);
                    HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
                    return RedirectToAction("HomeDueño", "Dueño");
                }
            }
            return RedirectToAction("RegistrarseDueño", "Account");
        }
    }
}