using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlCategorias
    {
        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            string query = "SELECT * FROM Categorias";

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
                            Categoria categoria = new Categoria()
                            {
                                Id_Categoria = Convert.ToInt32(reader["id_categoria"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"] != DBNull.Value ? reader["descripcion"].ToString() : ""
                            };

                            listaCategorias.Add(categoria);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener categorías: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo establecer la conexión.");
                }
            }

            return listaCategorias;
        }
    }
}
