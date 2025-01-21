using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_1517_PRECIADO_CE_FINAL;
using BL_1517_PRECIADO_CE_FINAL;
using static System.Net.Mime.MediaTypeNames;

namespace _1517_PRECIADO_CE_FINAL
{
    public partial class frmFactura : System.Web.UI.Page
    {
        private NClientes negocioClientes = new NClientes();
        private NProductos negocioProductos = new NProductos();
        private NFacturas negocioFacturas = new NFacturas();
        private static List<FacturaDetalle> detallesFactura = new List<FacturaDetalle>(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                detallesFactura.Clear(); 
                GenerarNumeroFactura();
                CargarClientes();
                CargarProductos();
            }
        }

        public static int contadorFactura = 1001;

        private void GenerarNumeroFactura()
        {
             
            string numeroFactura = contadorFactura.ToString("D2");   
            txtFacturaNro.Text = numeroFactura;

             
            contadorFactura++;
        }


        private void CargarClientes()
        {
            var clientes = negocioClientes.ObtenerTodosLosClientes();
            ddlClientes.DataSource = clientes;
            ddlClientes.DataTextField = "NombreCliente"; 
            ddlClientes.DataValueField = "CodCliente";
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("-- Seleccione un Cliente --", ""));
        }

        private void CargarProductos()
        {
            var productos = negocioProductos.ObtenerTodosLosProductos();
            ddlProductos.DataSource = productos;
            ddlProductos.DataTextField = "NombreProducto"; 
            ddlProductos.DataValueField = "NombreProducto"; 
            ddlProductos.DataBind();

            ddlProductos.Items.Insert(0, new ListItem("-- Seleccione un Producto --", ""));
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreCliente = ddlClientes.SelectedValue;

            if (!string.IsNullOrEmpty(nombreCliente))
            {
                var clientes = negocioClientes.ObtenerClientesPorNombre(nombreCliente);

                if (clientes.Count > 0)
                {
                    var cliente = clientes[0];
                    txtDireccionCliente.Text = cliente.DireccionCliente;
                    txtTelefonoCliente.Text = cliente.TelefonoCliente;
                }
                else
                {
                    MostrarMensaje("No se encontró el cliente seleccionado.");
                }
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreProducto = ddlProductos.SelectedValue;

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                var productos = negocioProductos.ObtenerProductosPorNombre(nombreProducto);

                if (productos.Count > 0)
                {
                    var producto = productos[0];
                    txtPrecioProducto.Text = producto.PrecioProducto.ToString("0.00");
                    txtStockProducto.Text = producto.StockProducto.ToString();
                }
                else
                {
                    MostrarMensaje("No se encontró el producto seleccionado.");
                }
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (ddlProductos.SelectedValue == "")
            {
                MostrarMensaje("Debe seleccionar un producto.");
                return;
            }

            if (!int.TryParse(txtCantidadProducto.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Ingrese una cantidad válida mayor a 0.");
                return;
            }

            var producto = negocioProductos.ObtenerProductosPorNombre(ddlProductos.SelectedValue)[0];

            if (producto != null && cantidad > producto.StockProducto)
            {
                MostrarMensaje("La cantidad supera el stock disponible.");
                return;
            }

            decimal precio = producto.PrecioProducto;
            decimal subtotal = cantidad * precio;

            var detalle = new FacturaDetalle
            {
                CodProducto = producto.CodProducto,
                NombreProducto = producto.NombreProducto, 
                Cantidad = cantidad,
                PrecioProducto = precio,
                Subtotal = subtotal
            };

            detallesFactura.Add(detalle);

            gvDetalles.DataSource = detallesFactura;
            gvDetalles.DataBind();

            CalcularTotales();
        }

        private void CalcularTotales()
        {
            decimal total = 0;

            foreach (var detalle in detallesFactura)
            {
                total += detalle.Subtotal;
            }

            decimal igv = total * 0.18m; 
            decimal neto = total + igv;

            txtIGV.Text = igv.ToString("0.00");
            txtTotal.Text = total.ToString("0.00");
            txtNeto.Text = neto.ToString("0.00");
        }

        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            GenerarNumeroFactura();
           
            var cabecera = new FacturaCabecera
                {
                    CodCliente = ddlClientes.SelectedValue,
                    FechaFactura = DateTime.Now,
                    TotalFactura = decimal.Parse(txtTotal.Text),
                    IGVFactura = decimal.Parse(txtIGV.Text),
                    NetoFactura = decimal.Parse(txtNeto.Text)
                };

                 
                int numeroFactura = negocioFacturas.GuardarFactura(cabecera, detallesFactura);

                // Confirmación
                MostrarMensaje($"Factura #{numeroFactura} guardada correctamente.");
                LimpiarFormulario();
            
            
        }


        private void LimpiarFormulario()
        {
            detallesFactura.Clear();

             
            ddlClientes.SelectedIndex = 0;
            txtDireccionCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;

            ddlProductos.SelectedIndex = 0;
            txtPrecioProducto.Text = string.Empty;
            txtStockProducto.Text = string.Empty;
            txtCantidadProducto.Text = string.Empty;

            txtIGV.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtNeto.Text = string.Empty;

            gvDetalles.DataSource = null;
            gvDetalles.DataBind();
        }

        private void MostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Alert", $"alert('{mensaje}');", true);
        }

        protected void txtFacturaNro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}