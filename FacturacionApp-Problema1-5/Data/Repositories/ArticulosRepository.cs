using FacturacionApp_Problema1_5.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionApp_Problema1_5.Data.Utils;
using FacturacionApp_Problema1_5.Domain;

namespace FacturacionApp_Problema1_5.Data.Repositories
{
    public class ArticulosRepository : IArticuloRepository
    {
        public int DeleteArticulo(int codigo)
        {
            int rows = 0;
            SqlConnection cnn = null;
            string query = "SP_BORRAR_ARTICULO";
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

        public List<Articulos> GetArticulos()
        {
            List<Articulos> Articulos = new List<Articulos>();
            SqlConnection cnn = null;
            var helper = DataHelper.GetInstance();
            var tabla = helper.ExecuteSPQuery("SP_OBTENER_ARTICULOS");
            foreach (DataRow row in tabla.Rows)
            {
                Articulos articulo = new Articulos();
                articulo.Codigo = Convert.ToInt32(row["codigo"]);
                articulo.Nombre = row["nombre"].ToString();
                articulo.PrecioUnitario = Convert.ToDouble(row["precio_unitario"]);
                Articulos.Add(articulo);
            }
            return Articulos;
        }

        public Articulos GetArticulo(int codigo)
        {
            Articulos articulo = new Articulos();
            SqlConnection cnn = null;
            string query = "SP_OBTENER_BYID_ARTICULO";
            cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    articulo.Codigo = Convert.ToInt32(row["codigo"]);
                    articulo.Nombre = row["nombre"].ToString();
                    articulo.PrecioUnitario = Convert.ToDouble(row["precio_unitario"]);
                }
            }
            catch (SqlException)
            {
                articulo = null;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return articulo;
        }

        public int SaveArticulo(Articulos articulo)
        {
            int rows = 0;
            SqlConnection cnn = null;
            string query = "SP_GUARDAR_ARTICULO";
            cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@precio_unitario", articulo.PrecioUnitario);
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
