using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class DueñoController : Controller
{
    private readonly ILogger<DueñoController> _logger;

    public IActionResult HomeDueño(){
        return View("AgregarProducto", "Dueño");
    }
       public IActionResult AgregarProducto(Productos producto){
        Productos productoSesion = Objeto.StringToObject<Productos>(HttpContext.Session.GetString("user"));
        BD.CargarProductos(producto);
        ViewBag.categorias=BD.TraerCategorias();
        HttpContext.Session.SetString("user", Objeto.ObjectToString(productoSesion));
        return View("AgregarProducto");
    }  
      public IActionResult EliminarProducto(int Id){
        BD.EliminarProductos(Id);          

        return View("EliminarProducto");
    }  
    public IActionResult ModificarProducto(Productos producto)
{
    Productos productoSesion = Objeto.StringToObject<Productos>(HttpContext.Session.GetString("user"));
    BD.ModificarProductos(producto);
    HttpContext.Session.SetString("user", Objeto.ObjectToString(productoSesion));
    return View("ModificarProducto");
}
     public IActionResult VerProductosMiLocal(){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        ViewBag.ProductosMiLocal = BD.verProductosMiLocal(dueño.IdLocal);   
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("VerProductosMiLocal");
    }  
}