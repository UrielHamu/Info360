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


   public static void EliminarProductos(int idProducto)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute("EliminarProductosVto",new { IdProducto = idProducto },commandType: System.Data.CommandType.StoredProcedure);

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
public static string TraerCategoria(int id)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "TraerCategoria";
        string nombreCategoria = connection.QueryFirstOrDefault<string>(query, new { Id = id }, commandType:System.Data.CommandType.StoredProcedure);
        return nombreCategoria;
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

     public static void ModificarProductos(int Id, int precio)
{
      using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(
            "ModificarLocalesProductosInicial",
            new { Id = Id, PrecioInicial = precio },
            commandType: System.Data.CommandType.StoredProcedure
        );
    }

}
public static Productos TraerProducto(int id)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "TraerProducto";

        Productos producto = connection.Query<Productos>(query, new { IdProducto = id }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

        return producto;
    }
}


public static List<ProductosLocalesProductosInicial> TraerProductosMiLocal(int idLocal)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "TraerLocalesProductosInicialMiLocal";
        List<ProductosLocalesProductosInicial> listaFinal = new List<ProductosLocalesProductosInicial>();
        List<LocalesProductosInicial> list = connection.Query<LocalesProductosInicial>(query, new {IdLocal=idLocal}, commandType: System.Data.CommandType.StoredProcedure).ToList();

        foreach (LocalesProductosInicial productoInicial in list)
        {
            Productos producto = TraerProducto(productoInicial.IdProducto);
            listaFinal.Add(new ProductosLocalesProductosInicial(producto, productoInicial));
        }
        return listaFinal;
    }
}

public static ProductosLocalesProductosInicial TraerProductosLocalesProductosInicial(int id){
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query="TraerProductosLocalesProductosInicial";
        LocalesProductosInicial ProductoInicial= connection.QueryFirstOrDefault<LocalesProductosInicial>(query, new { Id = id }, commandType:System.Data.CommandType.StoredProcedure);
        return new ProductosLocalesProductosInicial(TraerProducto(id), ProductoInicial);
    }
} 
        //desde aca no estan hechos los stored procedures
public static List<ProductosTemporalesVto> verProductosMiLocalVto(int idLocal){
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query="TraerLocalesProductosVtoMiLocal";
            List<ProductosTemporalesVto> ListaFinal=new List<ProductosTemporalesVto>();
            List<LocalesProductosVto> ListaLocalesProductoVto =  connection.Query<LocalesProductosVto>(query, new {IdLocal=idLocal}, commandType: System.Data.CommandType.StoredProcedure).ToList();
            foreach(LocalesProductosVto localesProductosVto in ListaLocalesProductoVto)
                {
                    Productos producto=TraerProducto(localesProductosVto.IdProducto);
                    LocalesProductosInicial localesProductosInicial=TraerLocalesProductosInicial(producto.Id, idLocal);
                    ListaFinal.Add( new ProductosTemporalesVto(producto, localesProductosInicial, localesProductosVto));
                }
            return ListaFinal;
        }
}

public static List<ProductosTemporales> verProductosMiLocalInicial(int idLocal){
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query="TraerLocalesProductosVtoMiLocal";
            List<ProductosTemporales> ListaFinal=new List<ProductosTemporales>();
            List<LocalesProductosVto> ListaLocalesProductoVto =  connection.Query<LocalesProductosVto>(query, new {IdLocal=idLocal}, commandType: System.Data.CommandType.StoredProcedure).ToList();
            foreach(LocalesProductosVto localesProductosVto in ListaLocalesProductoVto)
                {
                    Productos producto=TraerProducto(localesProductosVto.IdProducto);
                    LocalesProductosInicial localesProductosInicial=TraerLocalesProductosInicial(producto.Id, idLocal);
                    ListaFinal.Add( new ProductosTemporales(producto, localesProductosInicial, localesProductosVto));
                }
            return ListaFinal;
        }
}
public static LocalesProductosInicial TraerLocalesProductosInicial(int idProducto, int idLocal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query="TraerLocalesProductosInicial";
                return connection.QueryFirstOrDefault<LocalesProductosInicial>(query, new { IdProducto = idProducto, IdLocal=idLocal }, commandType:System.Data.CommandType.StoredProcedure);
            }
            
        }

 
        public static void CrearProductos(int cantidad, DateTime fecha, int idProducto, int idLocal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("CrearLocalesProductosVto",new { IdProducto = idProducto, Cantidad=cantidad, FechaVencimiento=fecha, IdLocal=idLocal},commandType: System.Data.CommandType.StoredProcedure);
            }
        }  
        public static ProductosTemporales VerProductoAModificar(int idLocalesProductosVto, int idLocal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "TraerLocalesProductosVto";
                LocalesProductosVto localesProductosVto=connection.QueryFirstOrDefault<LocalesProductosVto>(query, new { Id = idLocalesProductosVto}, commandType:System.Data.CommandType.StoredProcedure);
                Productos producto = TraerProducto(localesProductosVto.IdProducto);
                LocalesProductosInicial localesProductosInicial=TraerLocalesProductosInicial(producto.Id, idLocal);

                return new ProductosTemporales(producto, localesProductosInicial,localesProductosVto);
            }
        }
        public static void ModificarProducto(int IdLocalesProductosInicial, int nuevoPrecio)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "ModificarProducto";
            connection.Execute(query, new {Id = IdLocalesProductosInicial, NuevoPrecio=nuevoPrecio}, commandType:System.Data.CommandType.StoredProcedure);
        }        
        }
        public static string TraerLocal(int idLocal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "TraerLocal";
                return connection.QueryFirstOrDefault<string>(query, new { Id = idLocal}, commandType:System.Data.CommandType.StoredProcedure);
            }

        }    
    }
}