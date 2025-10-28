namespace TP07.Models;

using Microsoft.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString =
        "Server=localhost;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True;";

    public static Usuario Login(string username, string password)
    {
        Usuario usuario;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE [username] = @username AND [password] = @password";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { username, password });
        }
        return usuario;
    }

    public static bool sePuedeRegistrar(Usuario usuario)
    {
        bool sePudo;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE username = @username";
            Usuario existente = connection.QueryFirstOrDefault<Usuario>(query, new { username = usuario.username });
            sePudo = (existente == null);
        }
        return sePudo;
    }

    public static bool registrar(Usuario usuario)
    {
        bool sePudo = sePuedeRegistrar(usuario);
        if (sePudo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Usuarios (username, password, nombre, apellido, foto, ultimoLogin)
                                 VALUES (@username, @password, @nombre, @apellido, @foto, @ultimoLogin)";
                connection.Execute(query, new
                {
                    username = usuario.username,
                    password = usuario.password,
                    nombre = usuario.nombre,
                    apellido = usuario.apellido,
                    foto = usuario.foto,
                    ultimoLogin = usuario.ultimoLogin
                });
            }
        }
        return sePudo;
    }
    public static string traerDueño(string NombreUsuario)
    {
     {
        string username = "";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT NombreUsuario FROM Dueños WHERE [NombreUsuario] = @NombreUsuario ";
            username = connection.QueryFirstOrDefault<Dueños>(query, new { NombreUsuario });
        }
        return username;
    }
    }
        public static string traerCliente(string Nombre)
    {
     {
        string username = "";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Nombre FROM Clientes WHERE [Nombre] = @Nombre ";
            username = connection.QueryFirstOrDefault<Clientes>(query, new { Nombre });
        }
        return username;
    }
    }
}