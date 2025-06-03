using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlProductos
    {
        // Obtener todos los productos
        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            string query = "SELECT " +
                "a.id_producto as [ID Producto]," +
                "a.nombre as [Nombre Producto], " +
                "a.descripcion as [Desc Producto], " +
                "a.precio as [Precio Producto], \r\nb.nombre as [Nombre Categoria] " +
                "FROM Productos a inner join Categorias b on a.id_categoria = b.id_categoria";

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
                                NombreCategoria= Convert.ToString(reader["Nombre Categoria"])
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

        // Agregar producto
        public bool AgregarProducto(Producto producto)
        {
            bool resultado = false;
            string query = "INSERT INTO Productos (nombre, descripcion, precio, id_categoria) " +
                           "VALUES (@nombre, @descripcion, @precio, @id_categoria)";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", (object)producto.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);

                        resultado = cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al agregar producto: " + ex.Message);
                    }
                }
            }

            return resultado;
        }

        // Actualizar producto
        public bool ActualizarProducto(Producto producto)
        {
            bool resultado = false;
            string query = "UPDATE Productos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, " +
                           "status = @status, id_categoria = @id_categoria WHERE id_producto = @id_producto";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();
                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", (object)producto.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@status", producto.Status);
                        cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                        cmd.Parameters.AddWithValue("@id_producto", producto.IdProducto);

                        resultado = cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al actualizar producto: " + ex.Message);
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
    }
}
