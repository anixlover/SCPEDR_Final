using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTO;
using CTR;
using DAO;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;

public partial class Actualizar_Proceso : System.Web.UI.Page
{
    CtrMoldura objCtrMoldura = new CtrMoldura();

    Ctr_Solicitud objCtrSolicitud = new Ctr_Solicitud();
    DtoSolicitud objdtosol = new DtoSolicitud();
    DtoMoldura objDtoMoldura = new DtoMoldura();
    DtoTipoMoldura objDtoTipoMoldura = new DtoTipoMoldura();
    Log _log = new Log();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //OpcionesTipoMoldura();
            if (Request.Params["Id"] != null)
            {
                //Image1.Visible = true;
                //txtPagina.InnerText = "Actualizar moldura";

                obtenerInformacionSolicitud(Request.Params["Id"]);

            }
            else
            {
                //ddlestadosolicitud.SelectedValue = "1";
            }
        }
    }

    public void OpcionesTipoMoldura()
    {
        DataSet ds = new DataSet();
        ds = objCtrSolicitud.OpcionesSolicitudEstado();
        ddlestadosolicitud.DataSource = ds;
        ddlestadosolicitud.DataTextField = "V_SE_Nombre";
        ddlestadosolicitud.DataValueField = "PK_ISE_Cod";
        ddlestadosolicitud.DataBind();
        ddlestadosolicitud.Items.Insert(0, new ListItem("Seleccione", "0"));
        _log.CustomWriteOnLog("Actualizar_Proceso", "Termino de llenar el ddl");

    }
    public void obtenerInformacionSolicitud(string id)
    {
        _log.CustomWriteOnLog("Actualizar_Proceso", "-------------------------------------------------- Entro a actualización ----------------------------------------");
        objdtosol.PK_IS_Cod = int.Parse(id);
        objCtrSolicitud.ObtenerSolicitud(objdtosol, objDtoMoldura);

        txtCodigo.Text = objdtosol.PK_IS_Cod.ToString();
        txttiposolicitud.Text = objdtosol.VS_TipoSolicitud;
        txtcantidad.Text = objdtosol.IS_Cantidad.ToString();
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlestadosolicitud.SelectedValue != "Seleccionar")
            {
                if (ddlestadosolicitud.SelectedValue == "1")
                {
                    string valor = ddlestadosolicitud.SelectedValue;
                    objdtosol.PK_IS_Cod = int.Parse(Request.Params["Id"]);
                    objdtosol.FK_ISE_Cod = 9;
                    objCtrSolicitud.ActualizarEstadoSolicitud(objdtosol);

                }
                if (ddlestadosolicitud.SelectedValue == "2")
                {
                    string valor = ddlestadosolicitud.SelectedValue;
                    objdtosol.PK_IS_Cod = int.Parse(Request.Params["Id"]);
                    objdtosol.FK_ISE_Cod = 10;
                    objCtrSolicitud.ActualizarEstadoSolicitud(objdtosol);

                }
                if (ddlestadosolicitud.SelectedValue == "3")
                {
                    string valor = ddlestadosolicitud.SelectedValue;
                    objdtosol.PK_IS_Cod = int.Parse(Request.Params["Id"]);
                    objdtosol.FK_ISE_Cod = 11;
                    objCtrSolicitud.ActualizarEstadoSolicitud(objdtosol);

                }
                Utils.AddScriptClientUpdatePanel(upBotonEnviar, "showSuccessMessage2()");
                return;

            }
            else
            {
                Response.Redirect("~/Gestionar_Estado_Pedido.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

        }
        catch (Exception ex)
        {
            _log.CustomWriteOnLog("Actualizar_Proceso", "btnboleta_Click error  : " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Gestionar_Estado_Pedido.aspx");
    }
}