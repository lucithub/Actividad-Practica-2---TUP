using FacturacionApp_Problema1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Data.Repositories.Interfaces
{
    public interface IFacturasRepository
    {
        Facturas GetFactura(int numero);
        bool AddFactura(Facturas factura);
        int DeleteFactura(int numero);
    }
}
