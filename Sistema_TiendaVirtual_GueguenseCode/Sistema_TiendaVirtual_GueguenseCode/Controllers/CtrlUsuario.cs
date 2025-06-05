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

        public bool AgregarUsuario(Usuario nuevoUsuario)
        {
            string query = "INSERT INTO Usuarios (nombre, contraseña, tipo_usuario) VALUES (@nombre, @contraseña, @tipo)";

            using (SqlConnection conexionDB = Conexion.conexion())
            {
                if (conexionDB != null)
                {
                    try
                    {
                        conexionDB.Open();

                        SqlCommand cmd = new SqlCommand(query, conexionDB);
                        cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre.Trim());
                        cmd.Parameters.AddWithValue("@contraseña", nuevoUsuario.Contraseña.Trim());
                        cmd.Parameters.AddWithValue("@tipo", nuevoUsuario.Tipo_Usuario.Trim());

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al agregar usuario: " + ex.Message);
                    }
                }
            }

            return false;
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string query = "SELECT id_usuario, nombre, tipo_usuario FROM Usuarios";

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
                            Usuario usuario = new Usuario
                            {
                                Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Tipo_Usuario = reader["tipo_usuario"].ToString()
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                    }
                }
            }

            return listaUsuarios;
        }
    }
}
