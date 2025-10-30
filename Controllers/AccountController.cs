using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    public IActionResult IniciarSesion(){
        return View("IniciarSesion");
    }
    
    [HttpPost]
    public IActionResult RecibirInicioDeSesion(string Username, string Password)
    {
        Usuarios usuario = BD.traerUsuario(Username);

        if (usuario == null||usuario.Contraseña!=Password)
        {
            return RedirectToAction("IniciarSesion", "Account");
        }
        else if(usuario.Rol=="Cliente")
        {
            HttpContext.Session.SetString("user", Objeto.ObjectToString(usuario));
            return RedirectToAction("HomeCliente", "Cliente");
        } else if(usuario.Rol == "Dueño") {
            HttpContext.Session.SetString("user", Objeto.ObjectToString(usuario));
            return RedirectToAction("HomeDueño", "Dueño");
        }
        return RedirectToAction("IniciarSesion", "Account");
    } 

    public IActionResult cerrarSesion(){
        HttpContext.Session.SetString("id", 0.ToString());
        return View("IniciarSesion");
    }
    public IActionResult registrarseCliente(){
        return View("RegistrarseCliente");
    }

    public IActionResult recibirRegistroCliente(string username, string password, string mail, string rol, int idProvincia){
        if(username!=null && password!=null && mail!=null && rol!=null && idProvincia!=null){
            Usuarios usuario=new Usuarios(username, password, mail, rol, DateTime.Now);
            int idUsuario = BD.registrar(usuario);
            Clientes cliente=new Clientes(idProvincia, idUsuario);
            BD.crearCliente(cliente);
            HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
            return RedirectToAction("HomeCliente", "Cliente");
        } else{
            return View("RegistrarseCliente");
        }
    }    
    
    public IActionResult recibirRegistroDueño(string username, string password, string mail, string rol, int idLocal){
        if(username!=null && password!=null && mail!=null && rol!=null && idLocal!=null){
            Usuarios usuario=new Usuarios(username, password, mail, rol, DateTime.Now);
            int idUsuario = BD.registrar(usuario);
            Dueños dueño=new Dueños(idLocal, idUsuario);
            BD.crearDueño(dueño);
            HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
            return RedirectToAction("HomeDueño", "Dueño");
        } else{
            return View("RegistrarseDueño");
        }
    }
}
