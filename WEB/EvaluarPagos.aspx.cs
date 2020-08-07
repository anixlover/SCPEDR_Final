using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO;
using CTR;
public partial class EvaluarPagos : System.Web.UI.Page
{
    DtoSolicitud sol = new DtoSolicitud();
    Ctr_Solicitud ctrsol = new Ctr_Solicitud();
    DtoVoucher v = new DtoVoucher();
    CtrVoucher ctrv = new CtrVoucher();
    DtoPago p = new DtoPago();
    CtrPago ctrp = new CtrPago();
    int solicitud = 0; 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            solicitud = Convert.ToInt32(Session["idSolicitudPago"].ToString());
            cargarPago();
        }
    }
    public void cargarPago()
    {
        
        sol.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"].ToString());
        p.FK_IS_Cod= Convert.ToInt32(Session["idSolicitudPago"].ToString());
        v.PK_VV_NumVoucher= ctrsol.HayPago(sol);
        ctrsol.leerSolicitudTipo(sol);
        if (ctrv.hayVoucher(v))
        {
            ctrp.HayRUC(p);
            lblruc.Text = "RUC: " + p.VP_RUC;
            string image = Convert.ToBase64String(v.VBV_Foto);
            Imagenvoucher.ImageUrl = "data:Image/png;base64," + image;
            lblImporte.Text = v.DV_ImporteDepositado.ToString();
            lbloperacion.Text = v.PK_VV_NumVoucher;
            lblsol.Text = Session["idSolicitudPago"].ToString();
        }
    }
    protected void btnObservar_Click(object sender, EventArgs e)
    {
        if (txtComentario.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({type: 'error',title: 'ERROR!',text: 'No inserto ninguna OBSERVACIÓN!!'});", true);
            return;
        }
        sol.VS_Comentario = txtComentario.Text;
        sol.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"].ToString());
        ctrsol.actualizarEstadoObservacion(sol);
        mostrarmsjPAGO(sol);
    }
    public void mostrarmsjPAGO(DtoSolicitud p)
    {
        switch (p.error)
        {
            case 2:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({type: 'error',title: 'ERROR!',text: 'Fecha INVALIDA!!'});", true);
                break;
            case 55:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({title:'Observación enviada!',text:'Datos ENVIADOS!',type:'success'}, function(){window.location.href='AdministrarPedidos.aspx'});", true);
                break;
            case 77:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({title:'Pago APORBADO!',text:'Datos ENVIADOS!',type:'success'}, function(){window.location.href='AdministrarPedidos.aspx'});", true);
                break;
        }
    }
    Log _log = new Log();
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            sol.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"].ToString());
            ctrsol.leerSolicitudTipo(sol);
            if (sol.VS_TipoSolicitud == "Catalogo")
            {
                ctrsol.actualizarEstadoAproves(sol);
                mostrarmsjPAGO(sol);
            }
            if (sol.VS_TipoSolicitud == "Personalizado por catalogo")
            {
                ctrsol.ActualizarFechaPersonalizadoCatalogo(sol);
                mostrarmsjPAGO(sol);
            }
        }
        catch (Exception ex)
        {
            _log.CustomWriteOnLog("Aprobar.cs", "error   " + ex.Message);

        }
    }

    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdministrarPedidos.aspx");
    }
}