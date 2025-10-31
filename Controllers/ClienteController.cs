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
    public IActionResult VerEmpresas(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        ViewBag.Locales = BD.MostrarLocales();
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerEmpresas");
    }        
    
    public IActionResult VerDescuentos(int filtro){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        if (filtro == 1 )
        {
            ViewBag.Productos = BD.VerProductosCategoria();
        }
         else if (filtro == 2 )
        {
            ViewBag.Productos = BD.VerProductosMenorAMayor();
        }
        else
        {
           ViewBag.Productos = BD.VerProductosMayorAMenor();
        }
        ViewBag.Cliente=cliente;
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerVencimiento");
    }         
    public IActionResult Configuracion(){
        Clientes cliente = Objeto.StringToObject<Clientes> (HttpContext.Session.GetString("user"));
        
        HttpContext.Session.SetString("user", Objeto.ObjectToString(cliente));
        return View("VerVencimiento");
    }         

}