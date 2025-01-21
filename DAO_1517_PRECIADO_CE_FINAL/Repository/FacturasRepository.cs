using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAO_1517_PRECIADO_CE_FINAL
{
    public class FacturasRepository
    {
        private readonly dbFacturaEntities _context;

        public FacturasRepository()
        {
            _context = new dbFacturaEntities(); 
        }

        public int InsertarFacturaCabecera(FacturaCabecera factura)
        {
      
            var numeroFacturaParam = new SqlParameter("@NumeroFactura", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            
            _context.Database.ExecuteSqlCommand(
                "EXEC InsertarFacturaCabecera @CodCliente, @FechaFactura, @TotalFactura, @IGVFactura, @NetoFactura, @NumeroFactura OUT",
                new SqlParameter("@CodCliente", factura.CodCliente),
                new SqlParameter("@FechaFactura", factura.FechaFactura),
                new SqlParameter("@TotalFactura", factura.TotalFactura),
                new SqlParameter("@IGVFactura", factura.IGVFactura),
                new SqlParameter("@NetoFactura", factura.NetoFactura),
                numeroFacturaParam
            );

            _context.SaveChanges(); 

            return (int)numeroFacturaParam.Value; 
        }

       
        public void InsertarDetalleFactura(FacturaDetalle detalle)
        {
            _context.Database.ExecuteSqlCommand(
                "EXEC InsertarDetalleFactura @NumeroFactura, @CodProducto, @NombreProducto, @Cantidad, @PrecioProducto, @Subtotal",
                new SqlParameter("@NumeroFactura", detalle.NumeroFactura),
                new SqlParameter("@CodProducto", detalle.CodProducto),
                new SqlParameter("@NombreProducto", detalle.NombreProducto),
                new SqlParameter("@Cantidad", detalle.Cantidad),
                new SqlParameter("@PrecioProducto", detalle.PrecioProducto),
                new SqlParameter("@Subtotal", detalle.Subtotal)
            );
        }

        
        public void GuardarFactura(FacturaCabecera cabecera, List<FacturaDetalle> detalles)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                    int numeroFactura = InsertarFacturaCabecera(cabecera);

                 
                    foreach (var detalle in detalles)
                    {
                        detalle.NumeroFactura = numeroFactura;
                        InsertarDetalleFactura(detalle);
                    }

                   
                    transaction.Commit();
                }
                catch (Exception)
                {
                 
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}