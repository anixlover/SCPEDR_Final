using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DTO;
using CTR;
using DAO;
using System.Configuration;

using System.Data.SqlClient;

public partial class ActualizarImgVoucher : System.Web.UI.Page
{
    Ctr_Solicitud objCtrSolicitud = new Ctr_Solicitud();
    DtoSolicitud objDtoSolicitud = new DtoSolicitud();
    DtoMolduraxUsuario dtoMolduraxUsuario = new DtoMolduraxUsuario();
    CtrVoucher objCtrVoucher = new CtrVoucher();
    DtoVoucher objvoucherdao = new DtoVoucher();
    Log _log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        ImageVA.Visible = false;
        //txtPagina.InnerText = "Agregar imagen";
        if (!Page.IsPostBack)
        {
            //objCtrVoucher.DetallesVoucherSolicitudUsuario(objvoucherdao, objDtoSolicitud, dtoMolduraxUsuario);
            
            
            if (Request.Params["Id"] != null)
            {
                ImageVA.Visible = true;
                obtenerInformacionVoucher(Request.Params["Id"]);

            }
            else
            {
               
            }
        }
    }

    public void obtenerInformacionVoucher(string id)
    {
        objDtoSolicitud.PK_IS_Cod = int.Parse(id);
        objCtrVoucher.DetallesVoucherSolicitudUsuario(objvoucherdao, objDtoSolicitud, dtoMolduraxUsuario);

        string cs = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("SP_DVS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter paramId = new SqlParameter()
            {
                ParameterName = "@idsol",
                Value = id
            };
            cmd.Parameters.Add(paramId);
            con.Open();
            byte[] ByteArray = (byte[])cmd.ExecuteScalar();
            con.Close();
            string strbase64 = Convert.ToBase64String(ByteArray);
            ImageVA.ImageUrl = "data:Image/png;base64," + strbase64;
        }
        txtFechaEmision.Text = objDtoSolicitud.DTS_FechaEmicion.ToString();
        txtNroOpe.Text = objvoucherdao.PK_VV_NumVoucher.ToString();
        txtImporte.Text = objvoucherdao.DV_ImporteDepositado.ToString();


    }
    protected void btnActVou_Click(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void ActVoucher_Click(object sender, EventArgs e)
    {

    }

    protected void btnActualizarVoucher_Click(object sender, EventArgs e)
    {
        objDtoSolicitud.PK_IS_Cod = int.Parse(Request.Params["Id"]);
        Utils.AddScriptClientUpdatePanel(upBotonGuardar, "uploadFileActualizarVoucher(" + Request.Params["Id"] + ");");

        _log.CustomWriteOnLog("ActualizarImgenVoucher", "Actualizado");
    }
}