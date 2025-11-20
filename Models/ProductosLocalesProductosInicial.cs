namespace Info360.Models;
public class ProductosLocalesProductosInicial
{
    public int IdProducto;
    public string Nombre;
    public string Foto;
    public string Categoria;
    public int PrecioInicial;
    public int IdLocalesProductosInicial;

    public ProductosLocalesProductosInicial(Productos producto, LocalesProductosInicial ProductoInicial){
        IdProducto=producto.Id;
        Nombre=producto.Nombre;
        Foto=producto.Foto;
        Categoria=BD.TraerCategoria(producto.IdCategoria);
        PrecioInicial=ProductoInicial.PrecioInicial;
        IdLocalesProductosInicial=ProductoInicial.Id;
    }
}