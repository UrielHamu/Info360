using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class DueñoController : Controller
{
    private readonly ILogger<DueñoController> _logger;

    public IActionResult HomeDueño(){
        return RedirectToAction("VerProductosMiLocal", "Dueño");
    }
    public IActionResult AgregarProducto(){
        ViewBag.categorias=BD.TraerCategorias();
        return View("AgregarProducto");
    }  
    
    [HttpPost]
    public IActionResult RecibirAgregarProducto(int cantidad, int IdCategoria, DateTime Fecha, string Foto, string Nombre, int Precio){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        BD.CrearProductos(cantidad, IdCategoria, Fecha, Foto, Nombre, Precio, dueño.IdLocal);
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return RedirectToAction("AgregarProducto", "Dueño");
    }

        public IActionResult EliminarProducto(){
            Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
            ViewBag.productos = BD.verProductosMiLocal(dueño.IdLocal);
            HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
            return View("EliminarProducto");
        }

      public IActionResult GuardarEliminarProducto(int Id){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        BD.EliminarProductos(Id, dueño.IdLocal);          
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return RedirectToAction("EliminarProducto", "Dueño");
    }  
    public IActionResult ModificarProducto()
{
    Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
    ViewBag.productos = BD.verProductosMiLocal(dueño.IdLocal);
    HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
    return View("ModificarProducto");
}

    public IActionResult VerProductoAModificar(int Id){
        ViewBag.producto = BD.VerProductoAModificar(Id);
        return View("FormularioModificar");
    }

    [HttpPost]
    public IActionResult RecibirModificarProducto(int Id, string Nombre, string Foto, DateTime FechaVto, int IdCategoria)
    {
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        BD.ModificarProducto(Id, Nombre, Foto, FechaVto, IdCategoria, dueño.IdLocal);
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return RedirectToAction("ModificarProducto", "Dueño");
    }
     public IActionResult VerProductosMiLocal(){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        ViewBag.ProductosMiLocal = BD.verProductosMiLocal(dueño.IdLocal);   
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("VerProductosMiLocal");
    }  
}