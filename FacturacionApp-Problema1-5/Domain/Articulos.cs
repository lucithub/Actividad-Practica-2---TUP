using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Domain
{
    public class Articulos
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public override string ToString()
        {
            return $"Codigo: {Codigo} - Nombre: {Nombre}";
        }

    }
}
