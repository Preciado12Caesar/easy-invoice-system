using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_1517_PRECIADO_CE_FINAL;

namespace BL_1517_PRECIADO_CE_FINAL
{

    public class NClientes
    {
        private ClientesRepository _repoClientes = new ClientesRepository();

      
        public List<Clientes> ObtenerTodosLosClientes()
        {
            return _repoClientes.ObtenerTodosLosClientes();
        }

        
        public List<Clientes> ObtenerClientesPorNombre(string nombreCliente)
        {
            return _repoClientes.ObtenerClientesPorNombre(nombreCliente);
        }
    }
}