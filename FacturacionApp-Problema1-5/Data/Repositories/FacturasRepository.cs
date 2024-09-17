using FacturacionApp_Problema1_5.Data.Repositories.Interfaces;
using FacturacionApp_Problema1_5.Data.Utils;
using FacturacionApp_Problema1_5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionApp_Problema1_5.Data.Repositories
{
    public class FacturasRepository : IFacturasRepository
    {
        public int DeleteFactura(int numero)
        {
            int rows = 0;
            SqlConnection cnn = null;
            string query = "SP_BORRAR_FACTURA";
            cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nroFactura", numero);
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                rows = 0;
                throw;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return rows;
        }

        public Facturas GetFactura(int numero)
        {
            Facturas facturas = new Facturas();
            SqlConnection cnn = null;
            string query = "OBTENER_FACTURA_ID";
            cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nroFactura", numero);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    facturas.NroFactura = Convert.ToInt32(row["nroFactura"]);
                    facturas.Fecha = Convert.ToDateTime(row["fecha"]);
                    facturas.FormaPago.Codigo = Convert.ToInt32(row["formaPago"]);
                    facturas.Cliente = row["cliente"].ToString();
                }
            }
            catch (SqlException)
            {
                facturas= null;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return facturas;
        }

        public bool AddFactura(Facturas factura)
        {
            bool res = false;
            SqlConnection cnn = null;
            SqlTransaction t = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_INSERTAR_FACTURA", cnn, t);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@formaPago", factura.FormaPago.Codigo);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);
                SqlParameter param = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                factura.NroFactura = Convert.ToInt32(param.Value);
                foreach (var detail in factura.Detalles)
                {
                    var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE_FACTURAS", cnn, t);
                    cmdDetail.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@nroFactura", factura.NroFactura);
                    cmdDetail.Parameters.AddWithValue("@codigo_articulo", detail.Articulo.Codigo);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Cantidad);
                    cmdDetail.Parameters.AddWithValue("@precio_unitario", detail.Precio);
                    cmdDetail.ExecuteNonQuery();
                }
                t.Commit();
                res = true;
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                res = false;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return res;
        }
    }
}
