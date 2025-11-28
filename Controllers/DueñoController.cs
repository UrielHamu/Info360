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
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
                int IdLocal= int.Parse(HttpContext.Session.GetString("IdLocal"));

        ViewBag.Productos=BD.TraerProductosMiLocal(IdLocal);//Debe usar ProductosLocalesProductosInicial
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("AgregarProducto");
    }  
    public IActionResult AgregarProductoForm (int Id){
        ViewBag.Id=Id;
        ViewBag.producto=BD.TraerProductosLocalesProductosInicial(Id); //debe traer este ProductosLocalesProductosInicial
        return View("FormAgregarProducto");
    }
    
    [HttpPost]
    public IActionResult RecibirAgregarProducto(int cantidad, DateTime Fecha, int IdProducto){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
                int IdLocal= int.Parse(HttpContext.Session.GetString("IdLocal"));
        LocalesProductosInicial producto=BD.TraerLocalesProductosInicial(IdProducto, IdLocal);
        BD.CrearProductos(cantidad, Fecha, IdLocal, producto.IdProducto);
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return RedirectToAction("AgregarProducto", "Dueño");
    }

        public IActionResult EliminarProducto(){
            Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
                    int IdLocal= int.Parse(HttpContext.Session.GetString("IdLocal"));
            ViewBag.productos = BD.verProductosMiLocalVto(IdLocal);//Debe usar el model ProductosTemporalesVto
    HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
    return View("ModificarProducto");
}

    public IActionResult VerProductoAModificar(int Id){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        int IdLocal= int.Parse(HttpContext.Session.GetString("IdLocal"));
        ViewBag.producto = BD.VerProductoAModificar(Id, IdLocal);//Recibe el id del LocalesProductosVto y debe usar el model ProductosTemporales
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("FormularioModificar");
    }

    [HttpPost]
    public IActionResult RecibirModificarProducto(int Id, int NuevoPrecio)
    {
        BD.ModificarProducto(Id, NuevoPrecio);//Recibe el id de LocalesProductosInicial, el nuevo PrecioInicial
        return RedirectToAction("ModificarProducto", "Dueño");
    }
     public IActionResult VerProductosMiLocal(){
        Dueños dueño = Objeto.StringToObject<Dueños> (HttpContext.Session.GetString("user"));
        int IdLocal= int.Parse(HttpContext.Session.GetString("IdLocal"));
        List<ProductosTemporalesVto> lista = BD.verProductosMiLocalVto(IdLocal);//debe usar el model ProductosTemporalesVto
        ViewBag.ProductosMiLocal = lista;
        
        HttpContext.Session.SetString("user", Objeto.ObjectToString(dueño));
        return View("VerProductosMiLocal");
    }  
}