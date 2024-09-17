using FacturacionApp_Problema1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Data.Repositories.Interfaces
{
    public interface IFormasPagosRepository
    {
        List<FormaPago> GetFormasPagos();
        int SaveFormaPago(FormaPago formaPago);
        int DeleteFormaPago(int codigo);
    }
}
