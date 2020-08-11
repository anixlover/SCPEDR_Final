<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUsuario.master" AutoEventWireup="true" CodeFile="ActualizarImgVoucher.aspx.cs" Inherits="ActualizarImgVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"></asp:ScriptManager>
    <h2>Actualizar</h2>
        <div style="text-align: center">

            <asp:Label ID="Label1" runat="server" Text="Voucher"></asp:Label>
            <br />
            
        </div>

        <div class="registrar-cliente">
            <asp:Label ID="lblSolicitud" runat="server" Text="..." style="font-weight: 700; color:#000000; font-size: xx-large;"></asp:Label>
            <div class="custom-file">
                    Imagen del voucher:<br />
                   <%-- &nbsp;--%>
                    <asp:Image ID="ImageVA" runat="server" class="rounded" />
                    <input name="fileAnexo" type ="file" id ="FileUpload1" accept=".png,.jpg" runat="server"  class="btn btn-warning" style="width: 100%;"  />
                   <%--onchange="ImagePreview(this);"--%>
                <br />
                 <div class="form-group">
                    <asp:Label ID="LabelFE" runat="server" Text="Fecha de emision"></asp:Label>
                    <asp:TextBox ID="txtFechaEmision" name="texto" runat="server" class="form-control" type="text" ReadOnly="true" BackColor="white" BorderColor="Black" Width="100%"></asp:TextBox>
                </div>
                 <div class="form-group">
                    <asp:Label ID="LabelNO" runat="server" Text="N°de operación"></asp:Label>
                    <asp:TextBox ID="txtNroOpe" name="texto" runat="server" class="form-control" type="text" ReadOnly="true" BackColor="white" BorderColor="Black" Width="100%"></asp:TextBox>
                </div>
                 <div class="form-group">
                    <asp:Label ID="LabelI" runat="server" Text="Importe"></asp:Label>
                    <asp:TextBox ID="txtImporte" name="texto" runat="server" class="form-control" type="text" ReadOnly="true" BackColor="white" BorderColor="Black" Width="100%"></asp:TextBox>
                </div>

                <%--<asp:Button ID="ActVoucher" runat="server" class="btn btn-danger btn-lg" Text="Actualizar" OnClick="ActVoucher_Click"></asp:Button>--%>
            </div>
        </div>
        

       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnActVou" runat="server" class="btn btn-danger btn-lg" Text="Actualizar" OnClick="btnActVou_Click"></asp:Button>
                     </ContentTemplate>
       </asp:UpdatePanel>--%>
    <asp:UpdatePanel ID="upBotonGuardar" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnActualizarVoucher" runat="server" CssClass="btn bg-indigo waves-effect" Style="float: right" Width="100%" Text="Actualizar"
                                                OnClick="btnActualizarVoucher_Click">
                                                <%--<i class="material-icons">save</i> Guardar--%>
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
    <script src="js/Aplicacion/UploadFile.js"></script>
   <%-- uploadFileImagenVoucher--%>
                
</asp:Content>

