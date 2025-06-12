using Sistema_TiendaVirtual_GueguenseCode.Controllers;
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
    public partial class FormReportes : Form
    {
        private ContextMenuStrip menuContextual;
        public FormReportes()
        {
            InitializeComponent();

            // Crear menú contextual y agregar opción
            menuContextual = new ContextMenuStrip();
            var verDetalleItem = new ToolStripMenuItem("Ver detalle de la factura");
            verDetalleItem.Click += VerDetalleItem_Click;
            menuContextual.Items.Add(verDetalleItem);

            // Asignar el menú contextual al DataGridView
            DtReportes.ContextMenuStrip = menuContextual;

            // Capturar evento MouseDown para seleccionar la fila clickeada antes de mostrar el menú
            DtReportes.MouseDown += DtReportes_MouseDown;
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            CargarReportesEnGrid();
        }

        private void CargarReportesEnGrid()
        {
            CtrlFactura ctrl = new CtrlFactura();
            DataTable reporte = ctrl.ObtenerReporteFacturas();
            DtReportes.DataSource = reporte;

            // Ajustar filas para que se autoajusten al contenido
            DtReportes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Ajustar columnas para que llenen el ancho del grid
            DtReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void ExportarDataGridViewAExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(Type.Missing);

                // Encabezados
                for (int i = 1; i < dgv.Columns.Count + 1; i++)
                {
                    excel.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }

                // Filas
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        excel.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                excel.Columns.AutoFit();
                excel.Visible = true; // Para mostrar el archivo generado
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void DtReportes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = DtReportes.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    DtReportes.ClearSelection();
                    DtReportes.Rows[hit.RowIndex].Selected = true;
                    DtReportes.CurrentCell = DtReportes.Rows[hit.RowIndex].Cells[0]; // Opcional, para que la fila quede activa
                }
            }
        }

        private void VerDetalleItem_Click(object sender, EventArgs e)
        {
            if (DtReportes.SelectedRows.Count > 0)
            {
                // Obtener el ID de la factura de la fila seleccionada.
                // Supongamos que la columna con ID se llama "IdFactura"
                var filaSeleccionada = DtReportes.SelectedRows[0];
                var idFacturaObj = filaSeleccionada.Cells["id_factura"].Value;

                if (idFacturaObj != null)
                {
                    int idFactura = Convert.ToInt32(idFacturaObj);

                    // Crear y mostrar el diálogo pasando el ID
                    var dialog = new DialogVerDetalleFactura(idFactura);
                    dialog.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BttnExport_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewAExcel(DtReportes);
        }
    }
}
