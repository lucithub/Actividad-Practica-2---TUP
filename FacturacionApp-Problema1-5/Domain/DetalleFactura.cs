using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Domain
{
    public class DetalleFactura
    {
        public int Codigo { get; set; }
        public Articulos Articulo { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double SubTotal()
        {
            return Cantidad * Precio;
        }
    }
}
