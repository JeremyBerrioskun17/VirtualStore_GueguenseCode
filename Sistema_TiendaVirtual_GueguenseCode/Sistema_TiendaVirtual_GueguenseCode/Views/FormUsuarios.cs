using Sistema_TiendaVirtual_GueguenseCode.Controllers;
using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_TiendaVirtual_GueguenseCode.Views
{
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void BttnAgregarUsuario_Click(object sender, EventArgs e)
        {
            string nombre = TxtUsuario.Text.Trim();
            string contraseña = TxtContraseña.Text;
            string repetirContraseña = TxtContraseñaReplay.Text;
            string tipoUsuario = CbxTipoUsuario.SelectedItem.ToString();

            // Validaciones
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(repetirContraseña) || string.IsNullOrWhiteSpace(tipoUsuario))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contraseña != repetirContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Usuario nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Contraseña = contraseña,
                Tipo_Usuario = tipoUsuario
            };

            CtrlUsuario ctrl = new CtrlUsuario();
            bool exito = ctrl.AgregarUsuario(nuevoUsuario);
            if (exito)
            {
                MessageBox.Show("Usuario agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtUsuario.Clear();
                TxtContraseña.Clear();
                TxtContraseñaReplay.Clear();
                CbxTipoUsuario.SelectedIndex = -1;

                CargarUsuariosEnGrid();
            }
            else
            {
                MessageBox.Show("Error al agregar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuariosEnGrid()
        {
            CtrlUsuario ctrl = new CtrlUsuario();
            var usuarios = ctrl.ObtenerTodosLosUsuarios();

            DtUsuarios.DataSource = null; // Limpia el DataGridView
            DtUsuarios.DataSource = usuarios; // Asigna la lista directamente

            DtUsuarios.Columns["Contraseña"].Visible = false;

            // Ajustar filas para que se autoajusten al contenido
            DtUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Ajustar columnas para que llenen el ancho del grid
            DtUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void CargarRoles()
        {
            var roles = new List<string>
            {
                "admin",
                "empleado"
            };
            CbxTipoUsuario.DataSource = roles;
        }


        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuariosEnGrid();
            CargarRoles();
            TxtContraseña.UseSystemPasswordChar = true;
            TxtContraseñaReplay.UseSystemPasswordChar = true;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = checkBox1.Checked;

            TxtContraseña.UseSystemPasswordChar = !mostrar;
            TxtContraseñaReplay.UseSystemPasswordChar = !mostrar;
        }
    }
}
