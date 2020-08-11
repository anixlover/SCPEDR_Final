<%@ WebHandler Language="C#" Class="AVoucher" %>

using System;
using System.Web;
using System.IO;
using CTR;
using DTO;

public class AVoucher : IHttpHandler
{

    Log _Log = new Log();
    public void ProcessRequest(HttpContext context)
    {
        _Log.CustomWriteOnLog("ActualizarImgenVoucher", "Entro a metodo ashx");
        try
        {
            if (context.Request.Files.Count > 0)
            {
                CtrVoucher oBLAPISol = new CtrVoucher();
                string ID = context.Request.QueryString["Id"].ToString();
                byte[] fileData = null;
                    
                _Log.CustomWriteOnLog("ActualizarImgenVoucher", " 2");
                using (var binaryReader = new BinaryReader(context.Request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(context.Request.Files[0].ContentLength);
                }
                _Log.CustomWriteOnLog("ActualizarImgenVoucher", "3");
                _Log.CustomWriteOnLog("ActualizarImgenVoucher", "Valor de Id a actualizar es" + ID);

                oBLAPISol.ActualizarVoucher(fileData, int.Parse(ID));
                    
                _Log.CustomWriteOnLog("ActualizarImgenVoucher", "4");
            }
            _Log.CustomWriteOnLog("ActualizarImgenVoucher", "5");
        }
        catch (Exception ex)
        {
                _Log.CustomWriteOnLog("ActualizarImgenVoucher", "Error" + ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}