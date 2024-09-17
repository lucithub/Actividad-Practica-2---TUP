using ProductosApi___2._4.Models;

namespace ProductosApi___2._4.Services
{
    public interface IAplicacion
    {
        List<Productos> GetProductos();
        bool AgregarOEditarProducto(Productos productos);
        bool BorrarProducto(int codigo);
    }
}
