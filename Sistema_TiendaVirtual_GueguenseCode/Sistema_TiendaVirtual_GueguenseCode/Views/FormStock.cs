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
        private ContextMenuStrip menuProductos;
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
                //DataGridProductos.Columns[4].Visible = false;
                DataGridProductos.Columns[5].Visible = false;
            }
        }


        private void FormStock_Load(object sender, EventArgs e)
        {
            CargarComboboxCategorias();
            CargarProductos();
            CrearMenuContextual();
        }


        private void LimpiarCampos()
        {
            TxtNombreProducto.Clear();
            TxtDescripcionProducto.Clear();
            TxtPrecioProducto.Clear();
            CbxCategorias.SelectedIndex = 0;
        }

 

        private void CrearMenuContextual()
        {
            menuProductos = new ContextMenuStrip();


            ToolStripMenuItem opcionEliminar = new ToolStripMenuItem("Cmbiar Estado del Producto");
            opcionEliminar.Click += OpcionEliminar_Click;

            menuProductos.Items.Add(opcionEliminar);

            DataGridProductos.ContextMenuStrip = menuProductos;
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

        private void DataGridProductos_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    var hit = DataGridProductos.HitTest(e.X, e.Y);
                    if (hit.RowIndex >= 0 && DataGridProductos.Rows[hit.RowIndex].Cells[0].Value != null)
                    {
                        DataGridProductos.ClearSelection();
                        DataGridProductos.Rows[hit.RowIndex].Selected = true;
                        menuProductos.Show(DataGridProductos, e.Location);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar el menú contextual: " + ex.Message);
                }
            }
        }
        private void OpcionEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridProductos.SelectedRows.Count > 0)
                {
                    var fila = DataGridProductos.SelectedRows[0];
                    int idProducto = Convert.ToInt32(fila.Cells["IdProducto"].Value);
                    DialogResult confirm = MessageBox.Show("¿Deseas cambiar el estado de este producto?", "Confirmación", MessageBoxButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        CtrlProductos ctrl = new CtrlProductos();
                        bool eliminado = ctrl.EliminarProducto(idProducto); 

                        if (eliminado)
                        {
                            MessageBox.Show("Producto actializado correctamente.");
                            CargarProductos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actializado el producto.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al deshabilitar producto: " + ex.Message);
            }
        }

        private void DataGridProductos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DataGridProductos.ClearSelection(); // Limpia selección anterior
                DataGridProductos.Rows[e.RowIndex].Selected = true; // Selecciona la fila actual
                menuProductos.Show(Cursor.Position); // Muestra menú contextual donde esté el cursor
            }
        }
    }
}
