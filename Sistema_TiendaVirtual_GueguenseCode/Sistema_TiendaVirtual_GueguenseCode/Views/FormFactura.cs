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

namespace Sistema_TiendaVirtual_GueguenseCode.Views
{
    public partial class FormFactura : Form
    {
        private System.Windows.Forms.Timer timerFechaHora;


        public FormFactura()
        {
            InitializeComponent();
            CargarProductos();
            CargarTimer();
            InicializarTabla();
        }

        private void CargarTimer()
        {
            timerFechaHora = new System.Windows.Forms.Timer();
            timerFechaHora.Interval = 1000; // 1000 ms = 1 segundo
            timerFechaHora.Tick += TimerFechaHora_Tick;
            timerFechaHora.Start();
        }


        private void InicializarTabla()
        {
            DtProductos.Columns.Add("IdProducto", "ID Producto");
            DtProductos.Columns["IdProducto"].Visible = false;

            DtProductos.Columns.Add("Nombre", "Producto");
            DtProductos.Columns.Add("Precio", "Precio Unitario");
            DtProductos.Columns.Add("Cantidad", "Cantidad");
            DtProductos.Columns.Add("Total", "Total");
        }


        private void CargarProductos()
        {
            CtrlProductos controller = new CtrlProductos();
            var listaProductos = controller.ObtenerProductosCombo();

            CbxProductos.DataSource = listaProductos;
            CbxProductos.DisplayMember = "Nombre";        // Lo que se muestra
            CbxProductos.ValueMember = "IdProducto";      // Lo que se usa internamente
        }


        private void ActualizarSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow fila in DtProductos.Rows)
            {
                if (fila.Cells["Total"].Value != null)
                {
                    string totalStr = fila.Cells["Total"].Value.ToString().Trim();
                    string[] partes = totalStr.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length == 2)
                    {
                        string valorNumerico = partes[1].Replace(",", "");
                        if (decimal.TryParse(valorNumerico, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal total))
                        {
                            subtotal += total;
                        }
                    }
                    else if (totalStr.StartsWith("C$"))
                    {
                        string valorNumerico = totalStr.Substring(2).Trim().Replace(",", "");
                        if (decimal.TryParse(valorNumerico, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal total))
                        {
                            subtotal += total;
                        }
                    }
                }
            }

            LbSubtotal.Text = $"C$ {subtotal:F2}";

            // Descuento como porcentaje
            decimal porcentajeDescuento = 0;
            if (!string.IsNullOrWhiteSpace(TxtDescuento.Text))
            {
                decimal.TryParse(TxtDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out porcentajeDescuento);
            }

            decimal montoDescuento = subtotal * (porcentajeDescuento / 100);
            decimal totalFinal = subtotal - montoDescuento;
            if (totalFinal < 0) totalFinal = 0;

            LbTotal.Text = $"C$ {totalFinal:F2}";
        }


        private void TimerFechaHora_Tick(object sender, EventArgs e)
        {
            // Actualizar etiquetas con la fecha y hora actual
            LbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");  // formato día/mes/año
            LbHora.Text = DateTime.Now.ToString("HH:mm:ss");    // formato 24 horas con segundos
        }

        private void BttnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (CbxProductos.SelectedItem is Producto productoSeleccionado)
            {
                if (int.TryParse(TxtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    // Obtener la cantidad disponible del label (suponiendo que el texto es "Stock: 10")
                    string textoStock = LbCantidad.Text.Replace("Stock:", "").Trim();
                    if (!int.TryParse(textoStock, out int stockDisponible))
                    {
                        MessageBox.Show("No se pudo determinar la cantidad en stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validar si la cantidad es mayor que el stock
                    if (cantidad > stockDisponible)
                    {
                        MessageBox.Show("La cantidad solicitada supera el stock disponible.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Calcular total
                    decimal totalProducto = productoSeleccionado.Precio * cantidad;

                    // Formatear manualmente con "C$" y dos decimales
                    string precioFormateado = $"C$ {productoSeleccionado.Precio:F2}";
                    string totalFormateado = $"C$ {totalProducto:F2}";

                    // Agregar fila al DataGridView
                    DtProductos.Rows.Add(
                        productoSeleccionado.IdProducto,
                        productoSeleccionado.Nombre,
                        precioFormateado,
                        cantidad,
                        totalFormateado
                    );

                    // Actualizar Subtotal
                    ActualizarSubtotal();
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CbxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxProductos.SelectedItem is Producto productoSeleccionado)
            {
                CtrlProductos ctrl = new CtrlProductos();
                var resultado = ctrl.ObtenerPrecioYCantidadProducto(productoSeleccionado.IdProducto);

                LbPrecio.Text = $"C$ {resultado.Precio:F2}";
                LbCantidad.Text = $"Stock: {resultado.Cantidad}";
            }
        }
            
        private void TztDescuento_TextChanged(object sender, EventArgs e)
        {
            ActualizarSubtotal(); // se recalcula todo automáticamente
        }

        private void BttnFacturacion_Click(object sender, EventArgs e)
        {
            if (DtProductos.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar productos al carrito antes de facturar.", "Advertencia");
                return;
            }

            try
            {
                // Obtener total desde el label
                string totalTexto = LbTotal.Text.Replace("C$", "").Trim();
                decimal totalFactura = Convert.ToDecimal(totalTexto);

                // Asumimos que tienes el ID del usuario logueado (cámbialo por el valor real)
                int idUsuarioActual = 1;

                // Crear lista de detalles desde el DataGridView
                List<DetalleFactura> listaDetalles = new List<DetalleFactura>();
                foreach (DataGridViewRow fila in DtProductos.Rows)
                {
                    // Evitar filas vacías o en modo edición
                    if (fila.IsNewRow) continue;

                    if (fila.Cells["IdProducto"].Value != null &&
                        fila.Cells["Cantidad"].Value != null &&
                        fila.Cells["Precio"].Value != null)
                    {
                        listaDetalles.Add(new DetalleFactura
                        {
                            IdProducto = Convert.ToInt32(fila.Cells["IdProducto"].Value),
                            Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value),
                            PrecioUnitario = Convert.ToDecimal(fila.Cells["Precio"].Value.ToString().Replace("C$", "").Trim())
                        });
                    }
                }


                // Crear objeto Factura
                Factura factura = new Factura
                {
                    Fecha = DateTime.Now,
                    Total = totalFactura,
                    IdUsuario = idUsuarioActual,
                    Detalles = listaDetalles
                };

                // Llamar al controller
                var controller = new CtrlFactura();
                int idGenerado = controller.GuardarFactura(factura);

                if (idGenerado > 0)
                {
                    MessageBox.Show($"Factura N° {idGenerado} registrada correctamente.", "Éxito");

                    // Limpiar controles
                    DtProductos.Rows.Clear();
                    LbSubtotal.Text = "C$ 0.00";
                    LbTotal.Text = "C$ 0.00";
                    TxtDescuento.Text = "0";
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al guardar la factura.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al facturar: " + ex.Message, "Error");
            }
        }

        private void TztDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir dígitos, control (como backspace) y un solo punto decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // bloquear
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && TxtDescuento.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir dígitos, control (como backspace) y un solo punto decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // bloquear
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && TxtCantidad.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
