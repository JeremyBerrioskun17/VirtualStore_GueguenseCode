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
    public partial class DialogVerDetalleFactura : Form
    {
        private int idFactura;

        public DialogVerDetalleFactura(int idFactura)
        {
            InitializeComponent();
            this.idFactura = idFactura;

        }


        private void CargarDetalleFactura()
        {
            try
            {
                CtrlFactura ctrl = new CtrlFactura();

                // Supongamos que tienes un método que devuelve un DataTable con los detalles de la factura
                DataTable detalles = ctrl.ObtenerDetalleFacturaPorId(idFactura);

                if (detalles != null && detalles.Rows.Count > 0)
                {
                    // Asumiendo que total, codigo y fecha están en la primera fila (o puedes obtenerlos de otra consulta)
                    var filaFactura = detalles.Rows[0];

                    lblCodigoFactura.Text = "N° Factura: " + idFactura.ToString();
                    lblFechaFactura.Text = Convert.ToDateTime(filaFactura["Fecha"]).ToString("dd/MM/yyyy");
                    lblTotalFactura.Text = "C$ " + Convert.ToDecimal(filaFactura["Total"]).ToString();

                    // Cargar detalles en el DataGridView
                    DTDetalleFactura.DataSource = detalles;

                    // Opcional: ajustar columnas
                    DTDetalleFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles para esta factura.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearFacturaElectronica_Click(object sender, EventArgs e)
        {
            // Aquí implementa la lógica para crear la factura electrónica
            MessageBox.Show("Funcionalidad de factura electrónica aún no implementada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DialogVerDetalleFactura_Load(object sender, EventArgs e)
        {
            // Aquí cargas el detalle de la factura con ese id
            CargarDetalleFactura();
        }
    }

}
