using ProductosApi___2._4.Models;

namespace ProductosApi___2._4.Services
{
    public class ProductoService : IAplicacion
    {
        public static List<Productos> lstProducto = new List<Productos>();
        public ProductoService() 
        {
            lstProducto = new List<Productos>();
        }
        public List<Productos> ObtenerProductos()
        {
            return lstProducto;
        }



    }
}
