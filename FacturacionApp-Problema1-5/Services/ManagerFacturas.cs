using FacturacionApp_Problema1_5.Data.Repositories;
using FacturacionApp_Problema1_5.Data.Repositories.Interfaces;
using FacturacionApp_Problema1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Services
{
    public class ManagerFacturas
    {
        IArticuloRepository articuloRepository;
        IFacturasRepository facturasRepository;
        IFormasPagosRepository formasPagosRepository;
        public ManagerFacturas()
        {
            articuloRepository = new ArticulosRepository();
            facturasRepository = new FacturasRepository();
            formasPagosRepository = new FormasPagosRepository();
        }
        public List<FormaPago> GetAllFormasPagos()
        {
            List<FormaPago> formasPagos = new List<FormaPago>();
            formasPagos = formasPagosRepository.GetFormasPagos();
            return formasPagos;
        }
        public void AddFormaPago(FormaPago formaPago)
        {
            formasPagosRepository.SaveFormaPago(formaPago);
        }
        public void DeleteFormaPago(int codigo)
        {
            formasPagosRepository.DeleteFormaPago(codigo);
        }
        public List<Articulos> GetAllArticulos()
        {
            List<Articulos> articulos = new List<Articulos>();
            articulos = articuloRepository.GetArticulos();
            return articulos;
        }
        public Articulos GetByIDArticulo(int codigo)
        {
            Articulos articulo = new Articulos();
            articulo = articuloRepository.GetArticulo(codigo);
            return articulo;
        }
        public int AddArticulo(Articulos articulo)
        {
            int filasAfectadas = articuloRepository.SaveArticulo(articulo);
            return filasAfectadas;
        }
        public int DeleteArticulo(int codigo)
        {
            int filasAfectadas = articuloRepository.DeleteArticulo(codigo);
            return filasAfectadas;

        }
        public Facturas GetFactura(int numero)
        {
            Facturas factura = new Facturas();
            factura = facturasRepository.GetFactura(numero);
            return factura;
        }
        public void AddFactura(Facturas factura)
        {
            facturasRepository.AddFactura(factura);
        }
        public void DeleteFactura(int numero)
        {
            facturasRepository.DeleteFactura(numero);
        }

    }
}
