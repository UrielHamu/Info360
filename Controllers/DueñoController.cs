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
        HttpContext.Session.SetString("user", Objeto.ObjectToString(productoSesion));
        return View("AgregarProducto");
        // aca tiene que ser un formulario que ingrese todo los detalles del producto a agregar

    }  
      public IActionResult EliminarProducto(int Id){
        BD.EliminarProductos(Id);          

        return View("EliminarProducto");
        // este metodo recibe el id del producto y lo elimina.
    }  
    public IActionResult ModificarProducto(Productos producto)
{
    Productos productoSesion = Objeto.StringToObject<Productos>(HttpContext.Session.GetString("user"));
    BD.ModificarProductos(producto);
    HttpContext.Session.SetString("user", Objeto.ObjectToString(productoSesion));
    return View("ModificarProducto");
     // aca tiene que ser un formulario que ingrese todo los detalles del producto a modificar, aunque no quiera cambiar algo de un producto, lo va a tener que hacer igual

}
     public IActionResult VerProductosMiLocal(){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        ViewBag.ProductosMiLocal = BD.verProductosMiLocal(dueño.IdLocal);   
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("VerProductosMiLocal");
    }  
}