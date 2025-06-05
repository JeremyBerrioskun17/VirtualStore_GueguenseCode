using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlProductos
    {
        public (decimal Precio, int Cantidad) ObtenerPrecioYCantidadProducto(int idProducto)
        {
            decimal precio = 0;
            int cantidad = 0;

            using (SqlConnection conexion = Conexion.conexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        conexion.Open();
                        string query = @"
                    SELECT p.precio, i.cantidad
                    FROM Productos p
                    INNER JOIN Inventarios i ON p.id_producto = i.id_producto
                    WHERE p.id_producto = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", idProducto);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    precio = reader.GetDecimal(0);
                                    cantidad = reader.GetInt32(1);
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener datos del producto: " + ex.Message);
                    }
                }
            }

            return (precio, cantidad);
        }

        // Obtener todos los productos
        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            string query = @"
        SELECT 
            a.id_producto AS [ID Producto],
            a.nombre AS [Nombre Producto],
            a.descripcion AS [Desc Producto],
            a.precio AS [Precio Producto],
            b.nombre AS [Nombre Categoria],
            ISNULL(c.cantidad, 0) AS [Cantidad]
        FROM Productos a
        INNER JOIN Categorias b ON a.id_categoria = b.id_categoria
        LEFT JOIN Inventarios c ON a.id_producto = c.id_producto";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            productos.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["ID Producto"]),
                                Nombre = reader["Nombre Producto"].ToString(),
                                Descripcion = reader["Desc Producto"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio Producto"]),
                                NombreCategoria = reader["Nombre Categoria"].ToString(),
                                Cantidad = Convert.ToInt32(reader["Cantidad"])
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener productos: " + ex.Message);
                    }
                }
            }

            return productos;
        }


        public bool AgregarProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();

                        using (SqlCommand cmd = new SqlCommand("AgregarProductoConInventario", conexionDB))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                            cmd.Parameters.AddWithValue("@descripcion", (object)producto.Descripcion ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@precio", producto.Precio);
                            cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                            cmd.Parameters.AddWithValue("@cantidad_inicial", producto.Cantidad); // campo extra

                            resultado = cmd.ExecuteNonQuery() > 0;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al agregar producto: " + ex.Message);
                    }
                }
            }

            return resultado;
        }


        // Eliminar producto
        public bool EliminarProducto(int idProducto)
        {
            bool resultado = false;
            string query = "DELETE FROM Productos WHERE id_producto = @id_producto";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);

                        resultado = cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al eliminar producto: " + ex.Message);
                    }
                }
            }

            return resultado;
        }

        // Obtener un solo producto por ID
        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = null;
            string query = "SELECT * FROM Productos WHERE id_producto = @id_producto";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["id_producto"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Status = Convert.ToBoolean(reader["status"]),
                                IdCategoria = Convert.ToInt32(reader["id_categoria"])
                            };
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener el producto: " + ex.Message);
                    }
                }
            }

            return producto;
        }

        public List<Producto> ObtenerProductosCombo()
        {
            List<Producto> productos = new List<Producto>();
            string query = "SELECT id_producto, nombre, precio FROM Productos";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            productos.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["id_producto"]),
                                Nombre = reader["nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]) 
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener productos para ComboBox: " + ex.Message);
                    }
                }
            }

            return productos;
        }
    }
}
