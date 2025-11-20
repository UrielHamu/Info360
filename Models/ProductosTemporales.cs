namespace Info360.Models;
public class ProductosTemporales
{
    public string Nombre;
    public string Categoria;
    public DateTime FechaVencimiento;
    public int Cantidad;
    public string Foto;
    public int PrecioInicial;
    public int IdLocal;
    public int PrecioConDescuento;
    public int IdLocalesProductosInicial;

    public ProductosTemporales(Productos producto, LocalesProductosInicial localesProductosInicial, LocalesProductosVto localesProductosVto){
        Nombre=producto.Nombre;
        Categoria= BD.TraerCategoria(producto.IdCategoria);
        FechaVencimiento=localesProductosVto.FechaVencimiento;
        Cantidad=LocalesProductosVto.Cantidad;
        Foto=producto.Foto;
        PrecioInicial=localesProductosInicial.PrecioInicial;
        IdLocal=localesProductosInicial.IdLocal;
        PrecioConDescuento=localesProductosVto.PrecioConDescuento;
        IdLocalesProductosInicial=localesProductosInicial.Id;
    }
}
