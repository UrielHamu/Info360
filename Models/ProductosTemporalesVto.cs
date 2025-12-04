namespace Info360.Models;
public class ProductosTemporalesVto
{
    public string Nombre;
    public string Categoria;
    public DateTime FechaVencimiento;
    public int Cantidad;
    public string Foto;
    public int PrecioInicial;
    public string Local;
    public int PrecioConDescuento;
    public int IdLocalesProductosVto;

        public ProductosTemporalesVto(Productos producto, LocalesProductosInicial localesProductosInicial, LocalesProductosVto localesProductosVto){
        Nombre=producto.Nombre;
        Categoria= BD.TraerCategoria(producto.IdCategoria);
        FechaVencimiento=localesProductosVto.FechaVencimiento;
        Cantidad=localesProductosVto.Cantidad;
        Foto=producto.Foto;
        PrecioInicial=localesProductosInicial.PrecioInicial;
        Local=BD.TraerLocal(localesProductosInicial.IdLocal);
        PrecioConDescuento=SacarPrecioConDto(PrecioInicial, FechaVencimiento);
        IdLocalesProductosVto=localesProductosVto.Id;
    }

public int SacarPrecioConDto(int precioInicial, DateTime fechaVencimiento)
{
    int diasRestantes = (fechaVencimiento - DateTime.Now).Days;

    if (diasRestantes < 0)
        return 0;

    else if (diasRestantes <= 7)
    {
        return (int)(precioInicial * 0.5);
    }
    else if (diasRestantes <= 14)
    {
        return (int)(precioInicial * 0.7);
    }
    else if (diasRestantes <= 30)
    {
        return (int)(precioInicial * 0.9);
    }
    else{
        return precioInicial;
    }
}

}