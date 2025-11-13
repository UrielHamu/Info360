namespace Info360.Models;


public class Carrito{
    public List<int> productos;
    public Carrito(){
        productos = new List<int>();
    }
    public void agregar(int idProducto){
        productos.Add(idProducto);
    }
}