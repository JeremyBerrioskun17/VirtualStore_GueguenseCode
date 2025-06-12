using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlFactura
    {
        public int GuardarFactura(Factura factura)
        {
            int idFactura = 0;

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    conexionDB.Open();
                    SqlTransaction tran = conexionDB.BeginTransaction();

                    try
                    {
                        // Insertar Factura
                        string queryFactura = @"INSERT INTO Facturas (fecha, total, id_usuario)
                                        OUTPUT INSERTED.id_factura
                                        VALUES (@fecha, @total, @usuario)";
                        using (SqlCommand cmd = new SqlCommand(queryFactura, conexionDB, tran))
                        {
                            cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
                            cmd.Parameters.AddWithValue("@total", factura.Total);
                            cmd.Parameters.AddWithValue("@usuario", factura.IdUsuario);

                            idFactura = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insertar Detalles mediante procedimiento almacenado
                        foreach (var detalle in factura.Detalles)
                        {
                            using (SqlCommand cmd = new SqlCommand("InsertarDetalleFactura", conexionDB, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@id_factura", idFactura);
                                cmd.Parameters.AddWithValue("@id_producto", detalle.IdProducto);
                                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                cmd.Parameters.AddWithValue("@precio_unitario", detalle.PrecioUnitario);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        Console.WriteLine("Error al guardar factura: " + ex.Message);
                        idFactura = 0;
                    }
                }
            }

            return idFactura;
        }

        public DataTable ObtenerReporteFacturas()
        {
            DataTable tabla = new DataTable();

            string query = @"
        SELECT 
            F.id_factura,
            F.fecha,
            F.total,
            U.nombre AS usuario,
            COUNT(DF.id_detalle) AS cantidad_productos
        FROM 
            Facturas F
        INNER JOIN 
            Usuarios U ON F.id_usuario = U.id_usuario
        INNER JOIN 
            Detalles_Factura DF ON F.id_factura = DF.id_factura
        GROUP BY 
            F.id_factura, F.fecha, F.total, U.nombre
        ORDER BY 
            F.fecha DESC";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                try
                {
                    conexionDB.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conexionDB);
                    da.Fill(tabla);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error al cargar reportes: " + ex.Message);
                }
            }

            return tabla;
        }

        public DataTable ObtenerDetalleFacturaPorId(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();

                        string query = @"
                    SELECT f.id_factura, f.fecha, c.nombre, d.cantidad, d.precio_unitario, f.total
                      FROM Facturas f
                      INNER JOIN Detalles_Factura d ON f.id_factura = d.id_factura
                      INNER JOIN Productos c ON d.id_Producto = c.id_Producto
                      WHERE f.id_factura = @id_factura";

                        using (SqlCommand cmd = new SqlCommand(query, conexionDB))
                        {
                            cmd.Parameters.AddWithValue("@id_factura", idFactura);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Aquí puedes loguear o manejar el error
                        throw new Exception("Error al obtener detalle de factura: " + ex.Message);
                    }
                }
            }

            return dt;
        }

    }
}
