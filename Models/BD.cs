namespace Info360.Models
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    public static class BD
    {
        private static string _connectionString =
            "Server=localhost;Database=FIFO;;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuarios Login(string NombreUsuario, string Contraseña)
        {
            Usuarios usuario = new Usuarios();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "Login";
                usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario, Contraseña }, commandType:System.Data.CommandType.StoredProcedure);
            }
            return usuario;
        }

        public static bool sePuedeRegistrar(Usuarios usuario)
        {
            bool sePudo;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SePuedeRegistrar";
                Usuarios existente = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario = usuario.NombreUsuario }, commandType:System.Data.CommandType.StoredProcedure);
                sePudo = (existente == null);
            }
            return sePudo;
        }
        public static int registrar(Usuarios usuario)
        {
            if (!sePuedeRegistrar(usuario)) return -1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "RegistrarUsuario";

                int idUsuario = connection.QuerySingle<int>(query, new
                {
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Email = usuario.Email,
                    FechaRegistro = usuario.FechaRegistro,
                    Rol = usuario.Rol
                }, commandType:System.Data.CommandType.StoredProcedure);

                return idUsuario;
            }
        }

       public static Usuarios traerUsuario(string NombreUsuario)
 { 
     Usuarios usuario = new Usuarios();
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "TraerUsuario";
        usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { NombreUsuario }, commandType:System.Data.CommandType.StoredProcedure);
        return usuario;
    }
}

     public static void CrearProductos(int cantidad, int idCategoria, DateTime fecha, string foto, string nombre, int precio, int idLocal)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
       int idProducto = connection.QuerySingle<int>(
            "CargarProductos",
            new { Nombre = nombre, Foto = foto, IdCategoria = idCategoria },
            commandType: System.Data.CommandType.StoredProcedure
        );

        connection.Execute(
            "CargarLocalesProductosVto",
            new { IdProducto = idProducto, IdLocal = idLocal, Cantidad = cantidad, FechaVencimiento = fecha },
            commandType: System.Data.CommandType.StoredProcedure
        );

          connection.Execute(
            "CargarLocalesProductosInicial",
            new { IdProducto = idProducto, IdLocal = idLocal, PrecioInicial = precio },
            commandType: System.Data.CommandType.StoredProcedure
        );
    }
}

            public static void EliminarProductos(int idProducto)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(
            "EliminarProductosVto",
            new { IdProducto = idProducto },
            commandType: System.Data.CommandType.StoredProcedure
        );

        connection.Execute(
            "EliminarProductosInicial",
            new { IdProducto = idProducto },
            commandType: System.Data.CommandType.StoredProcedure
        );
        connection.Execute( 
        "EliminarProductos",
        new { IdProducto = idProducto }, commandType:System.Data.CommandType.StoredProcedure);

    }
    }
public static string BuscarLocal(int id)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "BuscarLocal";
        string nombreLocal = connection.QueryFirstOrDefault<string>(query, new { Id = id }, commandType:System.Data.CommandType.StoredProcedure);
        return nombreLocal;
    }
}
    public static void ModificarProductos(Productos producto)
    {
          using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "ModificarProductos";
        connection.Execute(query, new { IdProducto = producto.Id, nombreProducto = producto.Nombre, fotoProducto = producto.Foto, categoriaProducto = producto.IdCategoria}, commandType:System.Data.CommandType.StoredProcedure);
    }


    }
   public static List<Productos> VerProductosCategoria()
    {
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "VerProductosCategoria";
        List<Productos> list = connection.Query<Productos>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
        return list;
    }
    }
    public static List<Productos> VerProductosMayorAMenor()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "VerProductosMayorAMenor";

        return connection.Query<Productos>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
    }
}

    public static List<Productos> VerProductosMenorAMayor()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "VerProductosMenorAMayor";

        return connection.Query<Productos>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
    }
}
    public static List<ProductosTemporales> verProductosMiLocal(int Id)
{
       using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "VerProductosMiLocal";
        return connection.Query<ProductosTemporales>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
    }
}
   public static List<Locales> MostrarLocales()
    {
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "MostrarLocales ";
        List<Locales> list = connection.Query<Locales>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
        return list;
    }
    }
         public static void CrearDueño(Dueños dueño)
      {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "CrearDueño";
                    connection.Execute(query, new
                    {
                        IdLocal = dueño.IdLocal,
                        IdUsuario= dueño.IdUsuario
  
                    }, commandType:System.Data.CommandType.StoredProcedure);
                }
      }
               public static void CrearCliente(Clientes cliente)
      {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "CrearCliente";
                    connection.Execute(query, new
                    {
                        IdProvincia = cliente.IdProvincia,
                        IdUsuario= cliente.IdUsuario
  
                    }, commandType:System.Data.CommandType.StoredProcedure);
                }
      }
      public static List<Provincias> TraerProvincias()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "TraerProvincias";
            List<Provincias> list = connection.Query<Provincias>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
            return list;
        }
    }        
    public static List<Categorias> TraerCategorias()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "TraerCategorias";
            List<Categorias> list = connection.Query<Categorias>(query, commandType:System.Data.CommandType.StoredProcedure).ToList();
            return list;
        }
    }    
  public static Productos VerProductoAModificar(int IdProducto, int IdLocal)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "VerProductoAModificar";

        Productos producto = connection.QueryFirstOrDefault<Productos>(
            query,
            new { IdProducto = idProducto },
            commandType: System.Data.CommandType.StoredProcedure
        );

        return producto;
    }
}

     public static void ModificarProductos(int cantidad, int idCategoria, DateTime fecha, string foto, string nombre, int precio, int idLocal)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
       connection.Execute(
            "ModificarProductos",
            new { Nombre = nombre, Foto = foto, IdCategoria = idCategoria },
            commandType: System.Data.CommandType.StoredProcedure
        );

        connection.Execute(
            "ModificarLocalesProductosVto",
            new { Cantidad = cantidad, FechaVencimiento = fecha },
            commandType: System.Data.CommandType.StoredProcedure
        );

          connection.Execute(
            "ModificarLocalesProductosInicial",
            new {  PrecioInicial = precio },
            commandType: System.Data.CommandType.StoredProcedure
        );
    }
}

}}