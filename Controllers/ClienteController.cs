using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;

    public IActionResult HomeCliente(){
        return RedirectToAction("VerDescuentos","Cliente");
    }             
    public IActionResult VerDescuentos(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        ViewBag.Productos = BD.TraerProductos();
        ViewBag.cliente=cliente;
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerDescuentos");
    }        
    
    public IActionResult VerEmpresas(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        ViewBag.Locales=
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerEmpresas");
    }        
    
    public IActionResult VerVencimiento(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerVencimiento");
    }         
    public IActionResult Configuracion(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerVencimiento");
    }         

}