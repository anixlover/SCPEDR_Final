<%@ WebHandler Language="C#" Class="AVoucher" %>

using System;
using System.Web;
using System.IO;
using CTR;
using DTO;

public class AVoucher : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            if (context.Request.Files.Count > 0)
            {
                CtrVoucher oBLAPISol = new CtrVoucher();
                string ID = context.Request.QueryString["Id"].ToString();
                byte[] fileData = null;
               
                using (var binaryReader = new BinaryReader(context.Request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(context.Request.Files[0].ContentLength);
                }
                oBLAPISol.ActualizarVoucher(fileData,int.Parse(ID));
                
            }
        }
        catch (Exception ex)
        {
            
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}