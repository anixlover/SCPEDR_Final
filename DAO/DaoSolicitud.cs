﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DAO
{
    public class DaoSolicitud
    {
        SqlConnection conexion;
        public DaoSolicitud()
        {
            conexion = new SqlConnection(ConexionBD.CadenaConexion);
        }

        public void RegistrarSolcitud_PC(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolcitud_PC", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tipos", objsolicitud.VS_TipoSolicitud);
            command.Parameters.AddWithValue("@cantidad", objsolicitud.IS_Cantidad);
            command.Parameters.AddWithValue("@impt", objsolicitud.DS_ImporteTotal);
            command.Parameters.AddWithValue("@comen", objsolicitud.VS_Comentario);
            command.Parameters.AddWithValue("@epago", objsolicitud.IS_EstadoPago);
            command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();
            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
            conexion.Close();
        }

        public void RegistrarSolicitud_PP(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolcitud_PP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tipos", objsolicitud.VS_TipoSolicitud);
            var binary1 = command.Parameters.Add("@imagen", SqlDbType.VarBinary, -1);
            binary1.Value = DBNull.Value;
            command.Parameters.AddWithValue("@medida", objsolicitud.VS_Medida);
            command.Parameters.AddWithValue("@cantidad", objsolicitud.IS_Cantidad);
            command.Parameters.AddWithValue("@aprox", objsolicitud.DS_PrecioAprox);
            command.Parameters.AddWithValue("@comen", objsolicitud.VS_Comentario);
            command.Parameters.AddWithValue("@epago", objsolicitud.IS_EstadoPago);
            command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();
            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
            conexion.Close();
        }

        public void RegistrarImgSolicitudP(byte[] bytes, int id)
        {
            SqlCommand command = new SqlCommand("SP_Registrar_Img_SolicitudP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idSol", id);
            command.Parameters.AddWithValue("@imagen", bytes);
            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public void RegistrarSolicitud_LD(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolicitud_C", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@impt", objsolicitud.DS_ImporteTotal);
            command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();
           
            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
            conexion.Close();
        }

        public int CantidadSolicitudes()
        {

            int valor_retornado = 0;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM T_Solicitud");

            Console.WriteLine(cmd);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {   
                valor_retornado = int.Parse(reader[0].ToString());

            }
            conexion.Close();

            return valor_retornado;
        }
        public void UpdateEstadoSolicitud(DtoSolicitud objsolicitud)
        {
            string update = "UPDATE T_Solicitud SET FK_ISE_Cod = 6, DTS_FechaEmicion= GETDATE()  Where PK_IS_Cod=" + objsolicitud.PK_IS_Cod;
            //string update = "UPDATE T_Solicitud SET FK_ISE_Cod = 6, DTS_FechaEmicion='"+ DateTime.Today.Date +"' Where PK_IS_Cod=" + objsolicitud.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(update, conexion);
            conexion.Open();
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public void RegistrarSolicitud_LD2(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolicitud_C_2", conexion);
            command.CommandType = CommandType.StoredProcedure;
    
            command.Parameters.AddWithValue("@impt", objsolicitud.DS_ImporteTotal);
            command.Parameters.AddWithValue("@cantidad", objsolicitud.IS_Cantidad);
            command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();

            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
            conexion.Close();
        }
        public DataTable ConsultarEstadoPago(DtoSolicitud objcep, DtoMolduraxUsuario objmxu)
        {
            DataTable dtcep = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_ConsultarEstadoPago", conexion);
            command.Parameters.AddWithValue("@DNI", objmxu.FK_VU_Cod);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtcep = new DataTable();
            daAdaptador.Fill(dtcep);
            conexion.Close();
            return dtcep;
        }
        public void RegistrarSolicitud_PxC(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolicitud_PxC3", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoSol", objsolicitud.VS_TipoSolicitud);
            command.Parameters.AddWithValue("@cant", objsolicitud.IS_Cantidad);
            command.Parameters.AddWithValue("@impt", objsolicitud.DS_ImporteTotal);
            command.Parameters.AddWithValue("@fechareco", objsolicitud.DTS_FechaRecojo);
            command.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();
            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@newID"].Value);
            }
            conexion.Close();
        }
        public void RegistrarSolicitud_PxPD(DtoSolicitud objsolicitud)
        {
            SqlCommand command = new SqlCommand("SP_RegistrarSolicitud_PxDP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoSol", objsolicitud.VS_TipoSolicitud);
            var binary1 = command.Parameters.Add("@img", SqlDbType.VarBinary, -1);
            binary1.Value = DBNull.Value;

            //var binary1 = command.Parameters.Add("@img", SqlDbType.VarBinary, -1);
            //binary1.Value = DBNull.Value;
            command.Parameters.AddWithValue("@medida", objsolicitud.VS_Medida);
            command.Parameters.AddWithValue("@cant", objsolicitud.IS_Cantidad);
            command.Parameters.AddWithValue("@precioaprox", objsolicitud.DS_PrecioAprox);
            command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            conexion.Open();
            using (SqlDataReader dr = command.ExecuteReader())
            {
                objsolicitud.PK_IS_Cod = Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
            conexion.Close();
        }

        public DataSet desplegableSolicitudEstado()
        {
            SqlDataAdapter solest = new SqlDataAdapter("SP_Desplegable_Solicitud_Estado", conexion);
            solest.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet DS = new DataSet();
            solest.Fill(DS);
            return DS;
        }
        //este es
        public DataTable ListarSolicitudxEstado(DtoSolicitud objSol, DtoMolduraxUsuario objmxu, DtoSolicitudEstado objSE)
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_Listar_Solicitud_x_Estado2", conexion);
            command.Parameters.AddWithValue("@DNI", objmxu.FK_VU_Cod);
            command.Parameters.AddWithValue("@idSolEst", objSE.PK_ISE_Cod);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }
        public DataTable ListaMoldurasSolicitud(DtoSolicitud objSol)
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_Listar_Molduras_Solicitud", conexion);
            command.Parameters.AddWithValue("@sol", objSol.PK_IS_Cod);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }
        public bool SelectSolicitud(DtoSolicitud objsol)
        {
            string Select = "SELECT * from T_Solicitud where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(Select, conexion);
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                objsol.VS_TipoSolicitud = (string)reader[1];
                objsol.DS_ImporteTotal = Convert.ToDouble(reader[6].ToString());
            }
            else objsol.error = 1;
            conexion.Close();
            return hayRegistros;
        }
        public bool SelectSolicitudTipo(DtoSolicitud objsol)
        {
            string Select = "SELECT * from T_Solicitud where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(Select, conexion);
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                objsol.PK_IS_Cod=(int)reader[0];
                objsol.VS_TipoSolicitud = (string)reader[1];
            }
            else objsol.error = 1;
            conexion.Close();
            return hayRegistros;
        }
        public int SelectSolicitudNumDias(DtoSolicitud objsol)
        {
            string Select = "Select dbo.DevuelveDias(" + objsol.PK_IS_Cod+")";
            SqlCommand unComando = new SqlCommand(Select, conexion);
            int dias=0;
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                dias = (int)reader[0];
            }
            conexion.Close();
            return dias;
        }
        public double SelectSolicitudImporte(DtoSolicitud objsol)
        {
            string Select = "Select dbo.DevuelveImporte(" + objsol.PK_IS_Cod + ")";
            SqlCommand unComando = new SqlCommand(Select, conexion);
            double imp = 0;
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                imp = Convert.ToDouble(reader[0].ToString());
            }
            conexion.Close();
            return imp;
        }
        public bool SelectSolicitudDiseñoPersonalizado(DtoSolicitud objsol)
        {
            string Select = "SELECT * from T_Solicitud where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(Select, conexion);
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                objsol.VS_TipoSolicitud = (string)reader[1];
                objsol.DS_PrecioAprox = Convert.ToDouble(reader[5].ToString());
                objsol.VBS_Imagen = (byte[])reader[2];
                objsol.VS_Comentario = (string)reader[7];
            }
            else objsol.error = 1;
            conexion.Close();
            return hayRegistros;
        }
        public string SelectSolicitudPago(DtoSolicitud objsol)
        {
            string v = "";
            SqlCommand unComando = new SqlCommand("SP_SelectPagos", conexion);
            conexion.Open();
            unComando.CommandType = CommandType.StoredProcedure;
            unComando.Parameters.AddWithValue("@sol", objsol.PK_IS_Cod);
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                v = (string)reader[0];  
            }
            conexion.Close();
            return v;
        }
        public DataTable SelectSolicitudes() 
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_Administrar_Solicitudes", conexion);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }
        public DataTable SelectSolicitudes2(string tipo)
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_Administrar_Solicitudes_Filtro", conexion);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.Parameters.AddWithValue("@tipo",tipo);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }

        public void UpdateSolicitudObservada(DtoSolicitud objsol) 
        {
            string update = "UPDATE T_Solicitud SET VS_Comentario='"+objsol.VS_Comentario+"', FK_ISE_Cod=7 where PK_IS_Cod =" + objsol.PK_IS_Cod;
            
            conexion.Open();
            SqlCommand unComando = new SqlCommand(update, conexion);
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public void UpdateSolicitudFecha(DtoSolicitud objsol)
        {
            string update = "UPDATE T_Solicitud SET DTS_FechaRegistro=GETDATE(), DTS_FechaRecojo='" + objsol.DTS_FechaRecojo + "', FK_ISE_Cod=3 where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(update, conexion);
            conexion.Open();
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public void UpdateSolicitudFecha2(DtoSolicitud objsol)
        {
            string update = "UPDATE T_Solicitud FK_ISE_Cod=9 where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(update, conexion);
            conexion.Open();
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public void UpdateSolicitudPendiente(DtoSolicitud objsol)
        {
            string update = "UPDATE T_Solicitud SET DTS_FechaRegistro=GETDATE(),DTS_FechaRecojo=CAST(DATEADD(day,15,GETDATE()) AS DATE),FK_ISE_Cod=9 where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(update, conexion);
            conexion.Open();            
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public void UpdateSolicitudFecha3(DtoSolicitud objsol,int dias)
        {
            string update = "UPDATE T_Solicitud SET DTS_FechaRecojo=CAST(DATEADD(day,"+dias+",GETDATE()) AS DATE),FK_ISE_Cod=9 where PK_IS_Cod =" + objsol.PK_IS_Cod;
            SqlCommand unComando = new SqlCommand(update, conexion);
            conexion.Open();
            unComando.ExecuteNonQuery();
            conexion.Close();
        }
        public DataTable Listar_Solicitud_Personalizado()
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_Listar_Solicitud_Personalizado", conexion);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }
        public void ObtenerSolicitudPersonalizado(DtoSolicitud objsol, DtoSolicitudEstado objtiposol)
        {
            SqlCommand command = new SqlCommand("SP_Detalle_Solicitud_P", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Cod", objsol.PK_IS_Cod);
            DataSet ds = new DataSet();
            conexion.Open();
            SqlDataAdapter solicitud = new SqlDataAdapter(command);
            solicitud.Fill(ds);
            solicitud.Dispose();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objsol.VBS_Imagen = Encoding.ASCII.GetBytes(reader[0].ToString());
                objsol.VS_TipoSolicitud = reader[1].ToString();
                objsol.PK_IS_Cod = int.Parse(reader[2].ToString());
                objsol.VS_Medida = Convert.ToDouble(reader[3].ToString());
                objsol.IS_Cantidad = int.Parse(reader[4].ToString());
                objsol.DS_PrecioAprox = Convert.ToDouble(reader[5].ToString());
                objsol.VS_Comentario = reader[6].ToString();
                objsol.IS_EstadoPago = int.Parse(reader[7].ToString());
                objtiposol.V_SE_Nombre = reader[8].ToString();
            }
            conexion.Close();
            conexion.Dispose();
        }
        public void ObtenerSolicitud(DtoSolicitud objsol, DtoMoldura objmol)
        {
            SqlCommand command = new SqlCommand("SP_Obtener_Solicitud", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codsol", objsol.PK_IS_Cod);
            DataSet ds = new DataSet();
            conexion.Open();
            SqlDataAdapter moldura = new SqlDataAdapter(command);
            moldura.Fill(ds);
            moldura.Dispose();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objsol.PK_IS_Cod = int.Parse(reader["PK_IS_Cod"].ToString());
                objsol.VS_TipoSolicitud = reader["VS_TipoSolicitud"].ToString();
                objsol.IS_Cantidad = int.Parse(reader["IS_Cantidad"].ToString());
                objmol.IM_Stock = int.Parse(reader["EstadoNombre"].ToString());
                //objsol.VS_Comentario = reader["EstadoNombre"].ToString();
                //objmoldura.PK_IM_Cod = int.Parse(reader[0].ToString());
                //objmoldura.VM_Descripcion = reader[1].ToString();
                //objtipo.PK_ITM_Tipo = int.Parse(reader[2].ToString());
                //objtipo.VTM_Nombre = reader[3].ToString();
                //objmoldura.DM_Medida = Convert.ToDouble(reader[4].ToString());
                //objtipo.VTM_UnidadMetrica = reader[5].ToString();
                //objmoldura.IM_Estado = int.Parse(reader[6].ToString());
                //objmoldura.IM_Stock = int.Parse(reader[7].ToString());
                //objmoldura.DM_Precio = Convert.ToDouble(reader[8].ToString());
                //objmoldura.VBM_Imagen = Encoding.ASCII.GetBytes(reader[9].ToString());
            }
            conexion.Close();
            conexion.Dispose();
        }
        public DataSet OpcionesEstadoSolicitud()
        {
            SqlDataAdapter tipomol = new SqlDataAdapter("SP_Desplegable_Estado_Solicitud", conexion);
            tipomol.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet DS = new DataSet();
            tipomol.Fill(DS);
            return DS;
        }
        public void ModalPXDP(DtoSolicitud objsol)
        {
            SqlCommand command = new SqlCommand("SP_TraeSolPersonalizadoxDP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tiposol", objsol.PK_IS_Cod);
            //DataSet ds = new DataSet();
            conexion.Open();
            SqlDataAdapter moldura = new SqlDataAdapter(command);
            //moldura.Fill(ds);
            moldura.Dispose();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                objsol.PK_IS_Cod = Convert.ToInt32(reader["PK_IS_Cod"].ToString());
                objsol.VS_TipoSolicitud = reader["VS_TipoSolicitud"].ToString();
                //objsol.DTS_FechaRegistro = Convert.ToDateTime(reader["DTS_FechaRegistro"].toString());
                objsol.VS_Comentario = reader["NombreSolDP"].ToString();
                objsol.VBS_Imagen = Encoding.ASCII.GetBytes(reader["VBS_Imagen"].ToString());
            }
            conexion.Close();
            conexion.Dispose();
        }
        public void ModalAllmDP(DtoSolicitud objsol)
        {
            SqlCommand command = new SqlCommand("SP_TraeSolmenosXDP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tiposol", objsol.PK_IS_Cod);
            //DataSet ds = new DataSet();
            conexion.Open();
            SqlDataAdapter moldura = new SqlDataAdapter(command);
            //moldura.Fill(ds);
            moldura.Dispose();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                objsol.PK_IS_Cod = Convert.ToInt32(reader["PK_IS_Cod"]);
                objsol.VS_TipoSolicitud = reader["VS_TipoSolicitud"].ToString();
                //objsol.DTS_FechaRegistro = Convert.ToDateTime(reader["DTS_FechaRegistro"].ToString());
                objsol.VS_Comentario = reader["NombreSol"].ToString();
            }
            conexion.Close();
            conexion.Dispose();
        }
        public DataTable SelectSolicitudesGestion()
        {
            DataTable dtsolicitudes = null;
            conexion.Open();
            SqlCommand command = new SqlCommand("SP_GestionarSolicitudes", conexion);
            SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            dtsolicitudes = new DataTable();
            daAdaptador.Fill(dtsolicitudes);
            conexion.Close();
            return dtsolicitudes;
        }

        public void Cotizar(DtoSolicitud objsol)
        {
            SqlCommand command = new SqlCommand("SP_Cotizar", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idSol", objsol.PK_IS_Cod);
            command.Parameters.AddWithValue("@impTotal", objsol.DS_ImporteTotal);
            command.Parameters.AddWithValue("@nDias", objsol.IS_Ndias);
            //command.Parameters.AddWithValue("@estadoSol", objsol.FK_ISE_Cod);
            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();

        }

        public void RechazarP(DtoSolicitud objsol)
        {
            SqlCommand command = new SqlCommand("SP_RechazarP", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idSol", objsol.PK_IS_Cod);
            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();

        }

    }
}
