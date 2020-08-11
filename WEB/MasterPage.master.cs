using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO;
using CTR;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Log Log = new Log();
	protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Log.WriteOnLog("-------------------------------------------------------------------------------------------------------------");
            Log.WriteOnLog("-----------------------------Ingresando a masterpage y Obtener pestañas disponibles--------------------------");
            Log.WriteOnLog("-------------------------------------------------------------------------------------------------------------");
                int perfil = int.Parse(Session["id_perfil"].ToString());
            switch (perfil)
            {

            }
        }
        catch (Exception ex)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

	}
    public void perfilGerente()
    {
        string html = string.Format(@"
            <li class='active'>
                        <a href = 'GestionCatalogo.aspx' >
                            <i class='material-icons'>check</i>
                            <span>Gestionar Catalago</span>
                        </a>
                    </li>
            <li class='active'>
                        <a href = 'Evaluar_Pedido_Personalizado.aspx' >
                            <i class='material-icons'>check</i>
                            <span>Evaluar Pedido Personalizado</span>
                        </a>
                    </li>
        ");
        this.Literal1.Text = html;
    }
    public void perfilVendedor()
    {

    }
    public void perfilTrabajador()
    {

    }
    protected void UsuarioOption_ServerClick(object sender, EventArgs e)
    {

    }

    protected void AdministradorOption_ServerClick(object sender, EventArgs e)
    {

    }

    protected void btnCerrarSesion_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Login.aspx");
    }
}
