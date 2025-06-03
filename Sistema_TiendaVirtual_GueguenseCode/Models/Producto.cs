﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_TiendaVirtual_GueguenseCode.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }        
        public string Nombre { get; set; }          
        public string Descripcion { get; set; }      
        public decimal Precio { get; set; }          
        public bool Status { get; set; }            
        public int IdCategoria { get; set; }       
        public string NombreCategoria { get; set; }
    }

}
