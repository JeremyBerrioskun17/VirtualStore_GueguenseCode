using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlDashboard
    {

        public int ObtenerTotalProductosActivos()
        {
            int total = 0;
            string query = "SELECT COUNT(*) FROM Productos WHERE status = 1";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlCommand cmd = new SqlCommand(query, conexionDB);
                    total = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return total;
        }


        public int ObtenerTotalCategorias()
        {
            int total = 0;
            string query = "SELECT COUNT(*) FROM Categorias";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlCommand cmd = new SqlCommand(query, conexionDB);
                    total = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return total;
        }


        public int ObtenerProductosConStock()
        {
            int total = 0;
            string query = "SELECT COUNT(*) FROM Inventarios WHERE cantidad > 0";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlCommand cmd = new SqlCommand(query, conexionDB);
                    total = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return total;
        }

        public int ObtenerTotalVentas()
        {
            int total = 0;
            string query = "SELECT COUNT(*) FROM Facturas";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlCommand cmd = new SqlCommand(query, conexionDB);
                    total = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return total;
        }

        public List<CategoriaVenta> ObtenerCategoriasMasVendidas()
        {
            List<CategoriaVenta> lista = new List<CategoriaVenta>();
            string query = @"
        SELECT 
            c.nombre AS Categoria,
            SUM(df.cantidad) AS TotalVendidos
        FROM 
            Detalles_Factura df
        JOIN 
            Productos p ON df.id_producto = p.id_producto
        JOIN 
            Categorias c ON p.id_categoria = c.id_categoria
        GROUP BY 
            c.nombre
        ORDER BY 
            TotalVendidos DESC";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlCommand cmd = new SqlCommand(query, conexionDB);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoriaVenta categoria = new CategoriaVenta()
                        {
                            NombreCategoria = reader["Categoria"].ToString(),
                            TotalVendidos = Convert.ToInt32(reader["TotalVendidos"])
                        };

                        lista.Add(categoria);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return lista;
        }

    }
}
