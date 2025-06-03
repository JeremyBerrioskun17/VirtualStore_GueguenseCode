using Sistema_TiendaVirtual_GueguenseCode.Controllers;
using Sistema_TiendaVirtual_GueguenseCode.Models;
using Sistema_TiendaVirtual_GueguenseCode.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_TiendaVirtual_GueguenseCode
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BttnLogin_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Text.Trim();
            string contrasena = TxtContrasena.Text.Trim();

            // Validación simple antes de llamar al controlador
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty (contrasena))
            {
                MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamada al controlador
            CtrlUsuario controlador = new CtrlUsuario();
            Usuario usuarioEncontrado = controlador.VerificarUsuario(usuario, contrasena);

            if (usuarioEncontrado != null)
            {
                // Guardar en cache
                UsuarioCache.Id = usuarioEncontrado.Id_Usuario;
                UsuarioCache.Nombre = usuarioEncontrado.Nombre;
                UsuarioCache.Rol = usuarioEncontrado.Tipo_Usuario;

                MessageBox.Show("Inicio de sesión exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMain frm = new FormMain();
                frm.WindowState = FormWindowState.Maximized; // Esto lo maximiza
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ChkContraseña_CheckedChanged(object sender, EventArgs e)
        {
            TxtContrasena.UseSystemPasswordChar = !ChkContraseña.Checked;
        }
    }
}
