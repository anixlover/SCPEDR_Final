<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Detalles_Solicitud.aspx.cs" Inherits="Detalles_Solicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_header" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            color: #33CC33;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" Runat="Server">
    <form id="form1" runat="server" method="POST">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="body table-responsive">
            <div class="header">
                <h2>
                    <asp:Label ID="lbltittle" runat="server" Text="Detalles de la solicitud"></asp:Label>
                <asp:Label ID="lblsol" runat="server" Text="..."></asp:Label>
                    <small></small>
                </h2>
                <ul class="header-dropdown m-r--5">
                </ul>
            </div>
            <div class="card">
                <div class="header bg-info">
                    <h2>Cliente
                                <small></small>
                    </h2>
                    <ul class="header-dropdown m-r--5">
                    </ul>
                </div>
                <div>
                    <br />
                    &nbsp;<strong>&nbsp;&nbsp;&nbsp;&nbsp; N° de documento:</strong>:
                <asp:Label ID="lbldni" runat="server" Text="..."></asp:Label><br />
                    &nbsp;<strong>&nbsp;&nbsp;&nbsp;&nbsp; Nombre y Apellidos:</strong>
                    <asp:Label ID="lblnombre" runat="server" Text="..."></asp:Label><br />
                    &nbsp;<strong>&nbsp;&nbsp;&nbsp;&nbsp; Correo:</strong>
                    <asp:Label ID="lblcorreo" runat="server" Text="..."></asp:Label><br />
                    <br />
                </div>
            </div>
            <div class="card">
                <div class="header bg-info">
                    <h2>Molduras
                                <small></small>
                    </h2>
                    <ul class="header-dropdown m-r--5">
                    </ul>
                </div>
                <div class="body">
                    <asp:Image ID="imgPersonal" runat="server" />
                    <asp:GridView ID="gvMolduras" CssClass="table table-bordered table-hover js-basic-example dataTable" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="PK_IM_Cod" ItemStyle-HorizontalAlign="Center" HeaderText="Coóigo de Moldura" />
                            <%--<asp:ImageField DataImageUrlField="VBM_Imagen" ItemStyle-HorizontalAlign="Center" HeaderText="Imagen" />--%>
                            <asp:BoundField DataField="VM_Descripcion" ItemStyle-HorizontalAlign="Center" HeaderText="Nombre de Moldura" />
                            <asp:BoundField DataField="VTM_Nombre" ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de Moldura" />
                            <asp:BoundField DataField="IMU_Cantidad" ItemStyle-HorizontalAlign="Center" HeaderText="Cantidad" />
                            <asp:BoundField DataField="DMU_Precio" ItemStyle-HorizontalAlign="Center" HeaderText="Precio" />
                        </Columns>
                    </asp:GridView><br />
                    <asp:TextBox ID="txtcomentario" runat="server" CssClass="form-control" Width="100%"></asp:TextBox><br />
                    <span class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp; Costo Total <span class="auto-style2">
                <asp:Label ID="lblcosto" runat="server" Text="0.00" style="font-weight: 700; font-size: xx-large; color: #009900"></asp:Label>
                </span>
                <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="Regresar" CssClass="btn btn-secondary btn-lg" Width="100%" Font-Bold="True" Font-Size="Small" OnClick="Back_Click"/>
                <br />
                </span>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>                            
                            <br />
                            <asp:Label ID="lblfecha" runat="server" Text="Asignar Fecha de recojo:" Style="font-weight: 700; font-size: x-large;"></asp:Label><br />
                            <br />
                            <asp:TextBox ID="txtfecha" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lbldias" runat="server" Text="..."></asp:Label>&nbsp;Días a asignar<br />
                            <asp:Button ID="btnfehca" runat="server" Text="Asignar" CssClass="btn btn-success" OnClick="btnfehca_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>                
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Js" Runat="Server">
</asp:Content>

