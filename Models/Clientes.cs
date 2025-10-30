namespace Info360.Models;
public class Clientes
{
    public int Id;
    public int IdProvincia;
    public int IdUsuario;
    public Clientes(int idProvincia, int idUsuario){
        IdProvincia=idProvincia;
        IdUsuario=idUsuario;
    }
    public Clientes(){}
}
