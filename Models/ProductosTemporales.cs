namespace Info360.Models;
public class ProductosTemporales
{
    public string Nombre;
    public string Categoria;
    public DateTime FechaVencimiento;
    public int Cantidad;
    public string Foto;
    public int PrecioInicial;
    public string Local;
    public int PrecioConDescuento;
    public int IdLocalesProductosInicial;

    public ProductosTemporales(Productos producto, LocalesProductosInicial localesProductosInicial, LocalesProductosVto localesProductosVto){
        Nombre=producto.Nombre;
        Categoria= BD.TraerCategoria(producto.IdCategoria);
        FechaVencimiento=localesProductosVto.FechaVencimiento;
        Cantidad=localesProductosVto.Cantidad;
        Foto=producto.Foto;
        PrecioInicial=localesProductosInicial.PrecioInicial;
        Local=BD.TraerLocal(localesProductosInicial.IdLocal);
        PrecioConDescuento=localesProductosInicial.PrecioInicial * 10 / 20;//ACA HAY QUE HACER EL SISTEMA DE DESCUENTOS
        IdLocalesProductosInicial=localesProductosInicial.Id;
    }
}
