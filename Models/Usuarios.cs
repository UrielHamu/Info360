namespace Info360.Models;
public class Usuarios
{
    public int Id;
    public string Contraseña;
    public string NombreUsuario;
    public string Email;
    public DateTime FechaRegistro;
    public string Rol;

    public Usuarios(string username, string password, string mail, string rol, DateTime registro){
        NombreUsuario = username;
        Contraseña = password;
        Email=mail;
        Rol=rol;
        FechaRegistro=registro;
    }
    public Usuarios(){}
}
