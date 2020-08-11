using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO;
using CTR;

public partial class Detalles_Solicitud : System.Web.UI.Page
{
    DtoUsuario user = new DtoUsuario();
    CtrUsuario ctruser = new CtrUsuario();
    DtoSolicitud objDtoSolicitud = new DtoSolicitud();
    Ctr_Solicitud objCtrSolicitud = new Ctr_Solicitud();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfecha.Visible = false;
            lblfecha.Visible = false;
            btnfehca.Visible = false;
            lbldias.Visible = false;
            CargarCliente();
            asignar(); 
            CargarMolduras2();
            asignar2();
        }
    }
    public void CargarCliente()
    {
        string dni= Session["clienteDNI"].ToString();
        user.PK_VU_Dni = dni;
        if (ctruser.ExisteUsuario(user))
        {
            lblsol.Text = Session["idSolicitudPago"].ToString();
            lbldni.Text = user.PK_VU_Dni;
            lblnombre.Text = user.VU_Nombre +" "+ user.VU_Apellidos;
            lblcorreo.Text = user.VU_Correo;
        }
    }
    public void CargarMolduras2() 
    {
        objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"]);

        if (objCtrSolicitud.leerSolicitudTipo(objDtoSolicitud))
        {
            if (objDtoSolicitud.VS_TipoSolicitud == "Personalizado por catalogo" || objDtoSolicitud.VS_TipoSolicitud == "Catalogo")
            {
                objCtrSolicitud.LeerSolicitud(objDtoSolicitud);
                imgPersonal.Visible = false;
                gvMolduras.Visible = true;
                txtcomentario.Visible = false;
                lblcosto.Text = "S/ " + objDtoSolicitud.DS_ImporteTotal.ToString();
                gvMolduras.DataSource = objCtrSolicitud.ListaMolduras(objDtoSolicitud);
                gvMolduras.DataBind();
            }
            if (objDtoSolicitud.VS_TipoSolicitud == "Personalizado por diseño propio")
            {
                objCtrSolicitud.leerSolicitudDiseñoPersonal(objDtoSolicitud);
                gvMolduras.Visible = false;
                imgPersonal.Visible = true;
                txtcomentario.Visible = true;
                lblcosto.Text = "Aproximado: S/"+objDtoSolicitud.DS_PrecioAprox.ToString();
                string imagen = Convert.ToBase64String(objDtoSolicitud.VBS_Imagen);
                imgPersonal.ImageUrl = "data:Image/png;base64," + imagen;
                txtcomentario.Text = objDtoSolicitud.VS_Comentario;
            }
        }
    }
    public void asignar()
    {
        objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"]);
        if (objCtrSolicitud.leerSolicitudTipo(objDtoSolicitud))
        {
            if (Session["estado"].ToString()=="2")
            {
                txtfecha.Visible = true;
                txtfecha.Text = DateTime.Today.Date.ToString();
                lblfecha.Visible = true;
                btnfehca.Visible = true;
                objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"].ToString());
            }
        }
    }
    public void asignar2()
    {
        objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"]);
        if (objCtrSolicitud.leerSolicitudTipo(objDtoSolicitud))
        {
            if (Session["estado"].ToString() == "2" && objDtoSolicitud.VS_TipoSolicitud == "Personalizado por diseño propio")
            {
                objCtrSolicitud.diasRecojo(objDtoSolicitud);
                txtfecha.Visible = false;
                txtfecha.Text = DateTime.Today.Date.ToString();
                lblfecha.Visible = true;
                btnfehca.Visible = true;
                lbldias.Visible = true;
                lbldias.Text = objCtrSolicitud.diasRecojo(objDtoSolicitud).ToString();
                objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"].ToString());
            }
        }
    }
    protected void btnfehca_Click(object sender, EventArgs e)
    {
        objDtoSolicitud.PK_IS_Cod = Convert.ToInt32(Session["idSolicitudPago"]);
        objCtrSolicitud.leerSolicitudTipo(objDtoSolicitud);
        DateTime fecha = Convert.ToDateTime(txtfecha.Text);
        if (objDtoSolicitud.VS_TipoSolicitud == "Personalizado por diseño propio")
        {
            objCtrSolicitud.ActualizarFechaPersonalizadoDiseñopropio(objDtoSolicitud, objCtrSolicitud.diasRecojo(objDtoSolicitud));
        }
        if (objDtoSolicitud.VS_TipoSolicitud == "Personalizado por catalogo" || objDtoSolicitud.VS_TipoSolicitud == "Catalogo")
        {
            objDtoSolicitud.DTS_FechaRecojo = fecha;
            objCtrSolicitud.actualizarEstadoFecha(objDtoSolicitud);
            msj(objDtoSolicitud);
        }
    }
    public void msj(DtoSolicitud s)
    {
        switch (s.error)
        {
            case 2:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({type: 'error',title: 'ERROR!',text: 'Fecha invalida!!!'});", true);
                break;

            case 66:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "swal({title:'Se ha asignado la fecha de recojo CORRECTAMENTE!',text:'Datos ENVIADOS!',type:'success'}, function(){window.location.href='AdministrarPedidos.aspx'});", true);
                break;
        }
    }

    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdministrarPedidos.aspx");
    }
}