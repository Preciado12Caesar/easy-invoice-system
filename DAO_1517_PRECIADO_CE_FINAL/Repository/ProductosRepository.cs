using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_1517_PRECIADO_CE_FINAL
{
    public class ProductosRepository
    {
        private readonly dbFacturaEntities _context;

        public ProductosRepository()
        {
            _context = new dbFacturaEntities();
        }

        
        public List<Productos> ObtenerTodosLosProductos()
        {
            return _context.Database.SqlQuery<Productos>(
                "EXEC USP_OBTENER_TODOS_LOS_PRODUCTOS"
            ).ToList();
        }

       
        public List<Productos> ObtenerProductosPorNombre(string nombreProducto)
        {
            return _context.Database.SqlQuery<Productos>(
                "EXEC USP_BUSCAR_PRODUCTO_POR_NOMBRE @NombreProducto",
                new SqlParameter("@NombreProducto", nombreProducto)
            ).ToList();
        }
    }
}
