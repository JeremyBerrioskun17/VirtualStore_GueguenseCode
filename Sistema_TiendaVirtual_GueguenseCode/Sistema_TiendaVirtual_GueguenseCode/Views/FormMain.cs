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
    public partial class FormMain : Form
    {
        private Form formularioActivo = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LbUsuario.Text = "Usuario: " + UsuarioCache.Nombre;
        }

        private void BttnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void BttnStock_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormStock());
        }

        private void AbrirFormularioEnPanel(Form formularioHijo)
        {
            // Cierra el formulario anterior si hay uno abierto
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formularioHijo;

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            PanelChildren.Controls.Clear();
            PanelChildren.Controls.Add(formularioHijo);
            PanelChildren.Tag = formularioHijo;

            formularioHijo.Show();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Esto cierra todos los formularios abiertos
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FormMain)
                {
                    frm.Close(); // Esto disparará el Application.Exit() si se hace desde FormMain
                    break;
                }
            }
        }

        private void BttnFacturas_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormFactura());
        }
    }
}
