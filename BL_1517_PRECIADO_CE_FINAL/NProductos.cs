using DAO_1517_PRECIADO_CE_FINAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BL_1517_PRECIADO_CE_FINAL
{
    public class NProductos
    {
        private ProductosRepository _repoProductos = new ProductosRepository();

        
        public List<Productos> ObtenerTodosLosProductos()
        {
            return _repoProductos.ObtenerTodosLosProductos();
        }

        
        public List<Productos> ObtenerProductosPorNombre(string nombreProducto)
        {
            return _repoProductos.ObtenerProductosPorNombre(nombreProducto);
        }
    }
}