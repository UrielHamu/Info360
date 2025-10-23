using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("index");
    }
    public IActionResult IniciarSesion()
    {
        return View("IniciarSesion");
    }    
    public IActionResult recibirInicioDeSesion()
    {
        return View("filtro");
    }    
    public IActionResult Registrarse()
    {
        return View("Registrarse");
    }
    public IActionResult recibirRegistro()
    {
        return View("filtro");
    }    

    public IActionResult recibirFiltro(bool empresario)
    {
        if(empresario){
        return View("Empresario1");
        }
        else{
        return View("Descuentos");
        }
    }    
    public IActionResult descuentos()
    {
        return View("Descuentos");
    }    public IActionResult Empresas()
    {
        return View("Empresas");
    }    public IActionResult Vencimiento()
    {
        return View("Vencimiento");
    }


    
}
