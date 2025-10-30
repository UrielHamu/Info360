namespace Info360.Models
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    public static class BD
    {
        private static string _connectionString =
            "Server=A-PHZ2-CIDI-33;Database=FIFO;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuarios Login(string NombreUsuario, string Contraseña)
        {
            Usuarios usuario = new Usuarios();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE [NombreUsuario] = @NombreUsuario AND [Contraseña] = @Contraseña";
                usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario, Contraseña });
            }
            return usuario;
        }

        public static bool sePuedeRegistrar(Usuarios usuario)
        {
            bool sePudo;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                Usuarios existente = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario = usuario.NombreUsuario });
                sePudo = (existente == null);
            }
            return sePudo;
        }

        public static bool registrar(Usuarios usuario)
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

        public static Usuarios traerUsuario(string NombreUsuario)
        {
            Usuarios usuario = new Usuarios();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE [NombreUsuario] = @NombreUsuario";
                usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario });
            }
            return usuario;
        }
      public static void CargarProductos(Productos producto)
      {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Productos (Nombre, Foto, Categoria)
                                     VALUES (@NombreUsuario, @Contraseña, @IdCategoria)";
                    connection.Execute(query, new
                    {
                        Nombre = producto.Nombre,
                        Contraseña = producto.Foto,
                        IdCategoria = producto.IdCategoria
                    });
                }
      }
   public static void EliminarProductos(int idProducto)
    {
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"DELETE FROM Productos WHERE IdProducto = @IdProducto";
        connection.Execute(query, new { IdProducto = idProducto });
    }
    }
public static string BuscarLocal(int id)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT Nombre FROM Locales WHERE Id = @Id";
        string nombreLocal = connection.QueryFirstOrDefault<string>(query, new { Id = id });
        return nombreLocal;
    }
}
    public static void ModificarProductos(Productos producto)
    {
          using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"UPDATE Productos SET Nombre = @nombreProducto, Foto = @fotoProducto, Categoria = @IdCategoriaProducto WHERE IdProducto = @IdProducto";
        connection.Execute(query, new { IdProducto = producto.Id, nombreProducto = producto.Nombre, fotoProducto = producto.Foto, categoriaProducto = producto.IdCategoria});
    }

    }
   public static List<Productos> VerProductosCategoria()
    {
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Productos ORDER BY IdCategoria";
        List<Productos> list = connection.Query<Productos>(query).ToList();
        return list;
    }
    }
    public static List<Productos> VerProductosMayorAMenor()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            SELECT *
            FROM Productos 
            INNER JOIN LocalesProductosVto  ON Producto.Id = LocalesProductoVto.IdProducto
            ORDER BY porcentajeDescuento DESC";

        return connection.Query<Productos>(query).ToList();
    }
}

    public static List<Productos> VerProductosMenorAMayor()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            SELECT *
            FROM Productos 
            INNER JOIN LocalesProductosVto  ON Producto.Id = LocalesProductoVto.IdProducto
            ORDER BY porcentajeDescuento ASC";

        return connection.Query<Productos>(query).ToList();
    }
}
    public static List<Productos> verProductosMiLocal(int Id)
{
       using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            SELECT *
            FROM Productos 
            INNER JOIN LocalesProductosVto  ON Producto.Id = LocalesProductoVto.IdProducto
            WHERE LocalesProductosVto.IdLocal = @Id";

        return connection.Query<Productos>(query).ToList();
    }

}
}}
