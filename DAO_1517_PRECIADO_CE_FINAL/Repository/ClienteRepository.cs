using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_1517_PRECIADO_CE_FINAL;

namespace DAO_1517_PRECIADO_CE_FINAL
{
    public class ClientesRepository
    {
        private readonly dbFacturaEntities _context;

        public ClientesRepository()
        {
            _context = new dbFacturaEntities(); 
        }

        
        public List<Clientes> ObtenerTodosLosClientes()
        {
            return _context.Database.SqlQuery<Clientes>(
                "EXEC USP_OBTENER_TODOS_LOS_CLIENTES"
            ).ToList();
        }

    
        public List<Clientes> ObtenerClientesPorNombre(string nombreCliente)
        {
            return _context.Database.SqlQuery<Clientes>(
                "EXEC USP_BUSCAR_CLIENTE_POR_NOMBRE @NombreCliente",
                new SqlParameter("@NombreCliente", nombreCliente)
            ).ToList();
        }

    }
}
