namespace Info360.Models;
public class Dueños
{
    public int Id;
    public int IdLocal;
    public int IdUsuario;
    public Dueños(int idLocal, int idUsuario){
        IdLocal=idLocal;
        IdUsuario=idUsuario;
    }
    public Dueños(){}
}
