using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace TP07Perel_Kreserman_Hamu.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    public IActionResult IniciarSesion(){
        return View("IniciarSesion");
    }
    
    [HttpPost]
    public IActionResult RecibirInicioDeSesion(string Username, string Password)
{
    if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
    {
        Usuario usuario = BD.traerUsuario(Username);

        if (usuario != null)
        {
            HttpContext.Session.SetString("user", Objeto.ObjectToString(usuario));
            return RedirectToAction("HomeCliente", "Cliente");
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }
    else
    {
        return RedirectToAction("IniciarSesion", "Account");
    }
}


    public IActionResult cerrarSesion(){
        HttpContext.Session.SetString("id", 0.ToString());
        return View("loginForm");
    }
    public IActionResult registrarse(){
        return View("registrarse");
    }

    public IActionResult recibirRegistro(string username, string password, string nombre, string apellido, string foto){
        if(username!=null && password!=null && nombre!=null && apellido!=null && foto!=null){
            Usuario usuario=new Usuario(username, password, nombre, apellido, foto, DateTime.Now);
            BD.registrar(usuario);
            return View("loginForm");
        } else{
            return View("registrarse");
        }
    }
}
}