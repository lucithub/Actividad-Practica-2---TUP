using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Domain
{
    public class Facturas
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
        public Facturas()
        {
            List<DetalleFactura> Detalles = new List<DetalleFactura>();
        }
        public void Add(DetalleFactura detalle)
        {
            Detalles.Add(detalle);
        }
        public void Remove(DetalleFactura detalle)
        {
            Detalles.Remove(detalle);
        }
        public double Total()
        {
            double total = 0;
            foreach (var detalle in Detalles)
            {
                total += detalle.SubTotal();
            }
            return total;
        }
    }
}
