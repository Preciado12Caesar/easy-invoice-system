<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFactura.aspx.cs" Inherits="_1517_PRECIADO_CE_FINAL.frmFactura" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Factura</title>
 
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .readonly-field {
            background-color: #f8f9fa;
            border: 1px solid #ced4da;
            padding: 5px;
        }

        fieldset {
            margin-bottom: 20px;
            padding: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        legend {
            font-weight: bold;
            font-size: 1.2em;
            padding: 0 10px;
        }

        .gridview {
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            text-align: center;
        }

        .totales {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2 class="text-center mb-4">Generar Factura</h2>

             
            <div class="row mb-4">
                <div class="col-md-4">
                    <label for="txtFacturaNro" class="form-label">Número de Factura:</label>
                    <asp:TextBox ID="txtFacturaNro" runat="server" ReadOnly="true" CssClass="form-control readonly-field" OnTextChanged="txtFacturaNro_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-8">
                    <label for="ddlClientes" class="form-label">Cliente:</label>
                    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>

            
            <div class="row mb-4">
                <div class="col-md-6">
                    <label for="txtDireccionCliente" class="form-label">Dirección:</label>
                    <asp:TextBox ID="txtDireccionCliente" runat="server" ReadOnly="true" CssClass="form-control readonly-field"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtTelefonoCliente" class="form-label">Teléfono:</label>
                    <asp:TextBox ID="txtTelefonoCliente" runat="server" ReadOnly="true" CssClass="form-control readonly-field"></asp:TextBox>
                </div>
            </div>

       
            <div class="row mb-4">
                <div class="col-md-4">
                    <label for="ddlProductos" class="form-label">Producto:</label>
                    <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label for="txtPrecioProducto" class="form-label">Precio:</label>
                    <asp:TextBox ID="txtPrecioProducto" runat="server" ReadOnly="true" CssClass="form-control readonly-field"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label for="txtStockProducto" class="form-label">Stock:</label>
                    <asp:TextBox ID="txtStockProducto" runat="server" ReadOnly="true" CssClass="form-control readonly-field"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label for="txtCantidadProducto" class="form-label">Cantidad:</label>
                    <asp:TextBox ID="txtCantidadProducto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 d-flex align-items-end">
                    <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregarProducto_Click" />
                </div>
            </div>

        
            <div class="row mb-4">
                <div class="col-md-12">
                    <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gridview">
                        <Columns>
                            <asp:BoundField DataField="CodProducto" HeaderText="Código" />
                            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PrecioProducto" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

          
            <div class="row">
                <div class="col-md-4">
                    <label for="txtIGV" class="form-label">IGV (18%):</label>
                    <asp:TextBox ID="txtIGV" runat="server" ReadOnly="true" CssClass="form-control readonly-field totales"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="txtTotal" class="form-label">Total:</label>
                    <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" CssClass="form-control readonly-field totales"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="txtNeto" class="form-label">Neto:</label>
                    <asp:TextBox ID="txtNeto" runat="server" ReadOnly="true" CssClass="form-control readonly-field totales"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnGuardarFactura" runat="server" Text="Guardar Factura" CssClass="btn btn-success" OnClick="btnGuardarFactura_Click" />
                </div>
            </div>
        </div>
    </form>

 
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
