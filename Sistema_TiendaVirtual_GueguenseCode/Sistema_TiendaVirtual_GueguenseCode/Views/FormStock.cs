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
    public partial class FormStock : Form
    {
        public FormStock()
        {
            InitializeComponent();
        }

        private void CargarComboboxCategorias()
        {
            CtrlCategorias controlador = new CtrlCategorias();
            List<Categoria> categorias = controlador.ObtenerCategorias();

            CbxCategorias.DataSource = categorias;
            CbxCategorias.DisplayMember = "Nombre"; // Lo que el usuario ve
            CbxCategorias.ValueMember = "Id_Categoria"; // El valor que puedes usar internamente
        }

        private void CargarProductos()
        {
            CtrlProductos ctrlProductos = new CtrlProductos();

            List<Producto> productos = ctrlProductos.ObtenerProductos();
            DataGridProductos.DataSource = productos;

            // Ajustar el tamaño de las columnas al contenido de cada celda
            DataGridProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Ocultar columnas 4 y 5 
            if (DataGridProductos.Columns.Count >= 5)
            {
                DataGridProductos.Columns[4].Visible = false; // Columna 4
                DataGridProductos.Columns[5].Visible = false; // Columna 5
            }
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            CargarComboboxCategorias();
            CargarProductos();
        }

        private void BttnLogin_Click(object sender, EventArgs e)
        {
            CtrlProductos ctrlProductos = new CtrlProductos();
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(TxtNombreProducto.Text) || string.IsNullOrWhiteSpace(TxtPrecioProducto.Text))
            {
                MessageBox.Show("Por favor completa el nombre y precio.");
                return;
            }

            Producto nuevoProducto = new Producto
            {
                Nombre = TxtNombreProducto.Text,
                Descripcion = TxtDescripcionProducto.Text,
                Precio = decimal.Parse(TxtPrecioProducto.Text),
                IdCategoria = Convert.ToInt32(CbxCategorias.SelectedValue)
            };

            bool exito = ctrlProductos.AgregarProducto(nuevoProducto);

            if (exito)
            {
                MessageBox.Show("Producto guardado correctamente.");
                LimpiarCampos();
                CargarProductos(); // Recargar el grid
            }
            else
            {
                MessageBox.Show("Error al guardar el producto.");
            }
        }

        private void LimpiarCampos()
        {
            TxtNombreProducto.Clear();
            TxtDescripcionProducto.Clear();
            TxtPrecioProducto.Clear();
            CbxCategorias.SelectedIndex = 0;
        }
    }
}
