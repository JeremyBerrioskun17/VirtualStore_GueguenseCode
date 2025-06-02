using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_TiendaVirtual_GueguenseCode.Models
{
    class Conexion
    {
        public static SqlConnection conexion()
        {
            // Define la cadena de conexión
            // string Cadena_Conexion = "Server=localhost;Database=TiendaVirtual_ULSA;Trusted_Connection=True;";
            string Cadena_Conexion = "Server=JEREMY;Database=TiendaVirtual_ULSA;Trusted_Connection=True;";
            try
            {
                // Crea la conexión utilizando la cadena de conexión
                SqlConnection conexionDB = new SqlConnection(Cadena_Conexion);

                // Retorna la conexión sin abrirla
                return conexionDB;
            }
            catch (SqlException ex)
            {
                // Si hay un error en la conexión, lo mostramos en la consola
                Console.WriteLine("Error al intentar conectar: " + ex.Message);

                // Retorna null si hay un error en la conexión
                return null;
            }
        }
    }
}
