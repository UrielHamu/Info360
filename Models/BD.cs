namespace TP10.Models
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    public static class BD
    {
        private static string _connectionString =
            "Server=A-PHZ2-CIDI-33;Database=FIFO;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario Login(string NombreUsuario, string Contraseña)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE [NombreUsuario] = @NombreUsuario AND [Contraseña] = @Contraseña";
                usuario = connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario, Contraseña });
            }
            return usuario;
        }

        public static bool sePuedeRegistrar(Usuario usuario)
        {
            bool sePudo;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                Usuario existente = connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = usuario.NombreUsuario });
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
                    string query = @"INSERT INTO Usuarios (NombreUsuario, Contraseña, FechaRegistro)
                                     VALUES (@NombreUsuario, @Contraseña, @FechaRegistro)";
                    connection.Execute(query, new
                    {
                        NombreUsuario = usuario.NombreUsuario,
                        Contraseña = usuario.Contraseña,
                        FechaRegistro = usuario.FechaRegistro
                    });
                }
            }
            return sePudo;
        }

        public static string traerUsuario(string NombreUsuario)
{
    string username;
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT NombreUsuario FROM Usuarios WHERE [NombreUsuario] = @NombreUsuario";
        username = connection.QueryFirstOrDefault<string>(query, new { NombreUsuario });
    }
    return username;
}

    }
}