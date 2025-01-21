using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_1517_PRECIADO_CE_FINAL;
using BL_1517_PRECIADO_CE_FINAL;

namespace BL_1517_PRECIADO_CE_FINAL
{
 public class NFacturas
    {
        private readonly FacturasRepository _repoFacturas;

        public NFacturas()
        {
            _repoFacturas = new FacturasRepository();
        }

        public int GuardarFactura(FacturaCabecera cabecera, List<FacturaDetalle> detalles)
        {
            if (cabecera == null)
            {
                throw new ArgumentException("La cabecera de la factura no puede estar vacía.");
            }

            if (detalles == null || detalles.Count == 0)
            {
                throw new ArgumentException("Debe incluir al menos un detalle en la factura.");
            }

            
            int numeroFactura = _repoFacturas.InsertarFacturaCabecera(cabecera);

         
            foreach (var detalle in detalles)
            {
                detalle.NumeroFactura = numeroFactura; // Asignar el número de factura generado
                _repoFacturas.InsertarDetalleFactura(detalle);
            }

            return numeroFactura;
        }
    }
}
