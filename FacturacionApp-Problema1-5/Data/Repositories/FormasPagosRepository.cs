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
    internal class FormasPagosRepository : IFormasPagosRepository
    {
        public int DeleteFormaPago(int codigo)
        {
            int rows = 0;
            SqlConnection cnn = null;
            string query = "SP_BORRAR_FORMAS_PAGOS";
            cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", codigo);
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

        public List<FormaPago> GetFormasPagos()
        {
            List<FormaPago> formasPagos = new List<FormaPago>();
            SqlConnection cnn = null;
            var helper = DataHelper.GetInstance();
            var tabla = helper.ExecuteSPQuery("SP_GET_FORMAS_PAGOS");
            foreach (DataRow row in tabla.Rows)
            {
                FormaPago formaPago = new FormaPago();
                formaPago.Codigo = Convert.ToInt32(row["codigo"]);
                formaPago.Nombre = row["nombre"].ToString();
                formasPagos.Add(formaPago);
            }
            return formasPagos;
        }

        public int SaveFormaPago(FormaPago formaPago)
        {
            int rows = 0;
            SqlConnection cnn = null;
            string query = "SP_GUARDAR_FORMAS_PAGOS";
            cnn = DataHelper.GetInstance().GetConnection();
            
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", formaPago.Nombre);
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
    }
}
