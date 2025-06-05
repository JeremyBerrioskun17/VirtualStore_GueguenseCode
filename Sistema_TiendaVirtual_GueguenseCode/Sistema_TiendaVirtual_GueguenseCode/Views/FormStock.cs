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

            // Ajustar tamaño columnas
            DataGridProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Ocultar columnas si es necesario
            if (DataGridProductos.Columns.Count >= 6)
            {
                DataGridProductos.Columns[4].Visible = false;
                DataGridProductos.Columns[5].Visible = false;
            }
        }


        private void FormStock_Load(object sender, EventArgs e)
        {
            CargarComboboxCategorias();
            CargarProductos();
        }


        private void LimpiarCampos()
        {
            TxtNombreProducto.Clear();
            TxtDescripcionProducto.Clear();
            TxtPrecioProducto.Clear();
            CbxCategorias.SelectedIndex = 0;
        }

        private void BttnGuardarProductos_Click(object sender, EventArgs e)
        {
            CtrlProductos ctrlProductos = new CtrlProductos();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(TxtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(TxtPrecioProducto.Text) ||
                string.IsNullOrWhiteSpace(TxtCantidad.Text))
            {
                MessageBox.Show("Por favor completa el nombre, precio y cantidad.");
                return;
            }

            if (!decimal.TryParse(TxtPrecioProducto.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            Producto nuevoProducto = new Producto
            {
                Nombre = TxtNombreProducto.Text,
                Descripcion = TxtDescripcionProducto.Text,
                Precio = precio,
                IdCategoria = Convert.ToInt32(CbxCategorias.SelectedValue),
                Cantidad = cantidad
            };

            bool exito = ctrlProductos.AgregarProducto(nuevoProducto);

            if (exito)
            {
                MessageBox.Show("Producto guardado correctamente.");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el producto.");
            }
            CargarProductos(); // Recargar el grid
        }

        private void DataGridProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridProductos.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int cantidad))
                {
                    if (cantidad < 5)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
    }
}
