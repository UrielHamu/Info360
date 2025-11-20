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
        ViewBag.categorias=BD.TraerCategorias();//Debe traer los LocalesProductosInicial
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
            ViewBag.productos = BD.verProductosMiLocal(dueño.IdLocal);//Debe usar el model ProductosTemporales
            HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
            return View("EliminarProducto");
        }

      public IActionResult GuardarEliminarLocalesProductosInicial(int Id){
        BD.EliminarProductos(Id);          
        return RedirectToAction("EliminarProducto", "Dueño");
    }  
    public IActionResult ModificarProducto()
{
    Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
    ViewBag.productos = BD.verProductosMiLocal(dueño.IdLocal);//Debe usar el model ProductosTemporalesVto
    HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
    return View("ModificarProducto");
}

    public IActionResult VerProductoAModificar(int Id){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        ViewBag.producto = BD.VerProductoAModificar(Id, dueño.IdLocal);//Recibe el id del LocalesProductosVto y debe usar el model ProductosTemporales
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("FormularioModificar");
    }

    [HttpPost]
    public IActionResult RecibirModificarProducto(int Id, int NuevoPrecio)
    {
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        BD.ModificarProducto(Id, NuevoPrecio, dueño.IdLocal);//Recibe el id de LocalesProductosInicial, el nuevo PrecioInicial
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return RedirectToAction("ModificarProducto", "Dueño");
    }
     public IActionResult VerProductosMiLocal(){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        ViewBag.ProductosMiLocal = BD.verProductosMiLocal(dueño.IdLocal);//debe usar el model ProductosTemporalesVto
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("VerProductosMiLocal");
    }  
}