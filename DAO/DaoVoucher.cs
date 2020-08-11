﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DaoVoucher
    {
        SqlConnection conexion;
        public DaoVoucher()
        {
            conexion = new SqlConnection(ConexionBD.CadenaConexion);
        }
        public void InsertarVoucher(DtoVoucher Objvoucher)
        {
            SqlCommand cmd = new SqlCommand("SP_InsertarVoucher", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numvoucher", Objvoucher.PK_VV_NumVoucher);
            cmd.Parameters.AddWithValue("@importe", Objvoucher.DV_ImporteDepositado);
            cmd.Parameters.AddWithValue("@comentario", Objvoucher.VV_Comentario);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void InsertarImagenVoucher(byte[] obj, string pkvoucher)
        {
            SqlCommand cmd = new SqlCommand("RegistrarImagenVoucher", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numvoucher", pkvoucher);
            cmd.Parameters.AddWithValue("@foto",obj);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public bool SelectPagoVoucher(DtoVoucher v)
        {
            string Select = "SELECT * from T_Voucher where PK_VV_NumVoucher ='" +  v.PK_VV_NumVoucher+"'";
            SqlCommand unComando = new SqlCommand(Select, conexion);
            conexion.Open();
            SqlDataReader reader = unComando.ExecuteReader();
            bool hayRegistros = reader.Read();
            if (hayRegistros)
            {
                v.VBV_Foto = (byte[])reader[1];
                v.DV_ImporteDepositado = Convert.ToDouble(reader[2].ToString());
                v.FK_VP_Cod = (string)reader[4];
            }
            conexion.Close();
            return hayRegistros;
        }
        

        public void DetallesVoucherSolicitudUsuario(DtoVoucher objdtoVoucher,DtoSolicitud objDtoSolicitud,DtoMolduraxUsuario objDtoMolduraxUsuario)
        {
            //SqlCommand command = new SqlCommand("SP_DetallesVoucherUsuarioxSolicitud", conexion);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@dni",objDtoMolduraxUsuario.FK_VU_Cod);
            //command.Parameters.AddWithValue("@idsol", objDtoSolicitud.PK_IS_Cod);
            //DataSet ds = new DataSet();
            //conexion.Open();
            //SqlDataAdapter moldura = new SqlDataAdapter(command);
            //moldura.Fill(ds);
            //moldura.Dispose();

            //SqlDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    objDtoSolicitud.DTS_FechaEmicion = Convert.ToDateTime(reader[0].ToString());
            //    objdtoVoucher.PK_VV_NumVoucher = reader[1].ToString();
            //    objdtoVoucher.VBV_Foto = (byte[])reader[2];
            //    objdtoVoucher.DV_ImporteDepositado = Convert.ToInt32(reader[3].ToString());
            //}
            //conexion.Close();
            //conexion.Dispose();
            SqlCommand command = new SqlCommand("SP_DVS", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsol", objDtoSolicitud.PK_IS_Cod);
            DataSet ds = new DataSet();
            conexion.Open();
            SqlDataAdapter moldura = new SqlDataAdapter(command);
            moldura.Fill(ds);
            moldura.Dispose();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objdtoVoucher.VBV_Foto = Encoding.ASCII.GetBytes(reader[0].ToString()); 
                objDtoSolicitud.DTS_FechaEmicion = Convert.ToDateTime(reader[1].ToString());
                objdtoVoucher.PK_VV_NumVoucher = reader[2].ToString();
                objdtoVoucher.DV_ImporteDepositado = Double.Parse(reader[3].ToString());
            }
            conexion.Close();
            conexion.Dispose();
        }

        public void ActualizarVoucher(DtoSolicitud objDtoSolicitud)
        { 
            SqlCommand command = new SqlCommand("[SP_ActualizarVoucher", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsol", objDtoSolicitud.PK_IS_Cod);
            var binary1 = command.Parameters.Add("@imagen", SqlDbType.VarBinary, -1);
            binary1.Value = DBNull.Value;
            //command.Parameters.AddWithValue("@imagen", objdtoVoucher.VBV_Foto);
            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
