﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public int IdUsuario { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}
