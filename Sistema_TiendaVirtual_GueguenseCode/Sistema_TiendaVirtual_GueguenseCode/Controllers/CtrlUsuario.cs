using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_TiendaVirtual_GueguenseCode.Controllers
{
    public class CtrlUsuario
    {
        public Usuario VerificarUsuario(string nombre, string contraseña)
        {
            string query = "SELECT * FROM Usuarios WHERE nombre = @nombre AND contraseña = @contraseña";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();

                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        cmd.Parameters.AddWithValue("@contraseña", contraseña.Trim());

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            return new Usuario()
                            {
                                Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Contraseña = reader["contraseña"].ToString(),
                                Tipo_Usuario = reader["tipo_usuario"].ToString()
                            };
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al verificar usuario: " + ex.Message);
                    }
                }
            }

            return null; // Usuario no encontrado
        }


        public List<Usuario> ObtenerUsuarioPorCredenciales(string nombre, string contraseña)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string query = "SELECT * FROM Usuarios WHERE nombre = @nombre AND contraseña = @contraseña";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();

                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        cmd.Parameters.AddWithValue("@contraseña", contraseña.Trim());

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario()
                            {
                                Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Contraseña = reader["contraseña"].ToString(),
                                Tipo_Usuario = reader["tipo_usuario"].ToString()
                            };
                            listaUsuarios.Add(usuario);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener usuario: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo establecer la conexión.");
                }
            }

            return listaUsuarios;
        }

    }
}
