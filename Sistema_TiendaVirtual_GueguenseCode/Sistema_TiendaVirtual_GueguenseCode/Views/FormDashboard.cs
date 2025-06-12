using Sistema_TiendaVirtual_GueguenseCode.Controllers;
using Sistema_TiendaVirtual_GueguenseCode.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_TiendaVirtual_GueguenseCode.Views
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            CtrlDashboard ctrl = new CtrlDashboard();
            TxtProdAct.Text = ctrl.ObtenerTotalProductosActivos().ToString();
            TxtTotalCat.Text = ctrl.ObtenerTotalCategorias().ToString();
            TxtProdStock.Text = ctrl.ObtenerProductosConStock().ToString();
            TxtTotalVenta.Text = ctrl.ObtenerTotalVentas().ToString();

            CargarGraficoCategorias();
        }

        private void CargarGraficoCategorias()
        {
            CtrlDashboard ctrl = new CtrlDashboard();
            var datos = ctrl.ObtenerCategoriasMasVendidas();

            chartCategorias.Series.Clear();
            chartCategorias.Titles.Clear();
            chartCategorias.Titles.Add("Categorías Más Vendidas");

            Series serie = new Series("Ventas");
            serie.ChartType = SeriesChartType.Doughnut; // Pie o Doughnut

            foreach (var item in datos)
            {
                serie.Points.AddXY(item.NombreCategoria, item.TotalVendidos);
            }

            chartCategorias.Series.Add(serie);

            // Opcional: Estética
            serie.IsValueShownAsLabel = true;
            chartCategorias.Legends[0].Docking = Docking.Right;
            chartCategorias.BackColor = Color.Transparent;
            chartCategorias.ChartAreas[0].BackColor = Color.Transparent;
        }
    }
}
