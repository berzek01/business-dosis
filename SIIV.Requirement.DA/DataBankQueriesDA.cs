// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.DA.DataBankQueriesDA
// Assembly: SIIV.Requirement.DA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3193B483-1C99-47A3-B806-34369171C902
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.Requirement.DA.dll

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SIIV.Common.Resource;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

#nullable disable
namespace SIIV.Requirement.DA
{
  public class DataBankQueriesDA
  {
    private string cadenaConexion = string.Empty;

    public DataBankQueriesDA() => this.cadenaConexion = SIIV.Common.DA.Constants.Constants.NombreConexion;

    public DataTable PaymentAcreditation(
      int pintBankId,
      string pstrPaymentDate,
      string pstrTerminal,
      string psrtUserCode,
      string pstrProduct,
      int pintPaymentType)//LISTO ???
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Placas_SolicitudPlaca_Acreditacionpago]"))
        {
          instance.AddInParameter(storedProcCommand, "i_Banco", DbType.Int32, (object) pintBankId);
          instance.AddInParameter(storedProcCommand, "d_FechaPago", DbType.String, (object) pstrPaymentDate);
          instance.AddInParameter(storedProcCommand, "v_Terminal", DbType.String, (object) pstrTerminal);
          instance.AddInParameter(storedProcCommand, "v_CodigoIDUsuario", DbType.String, (object) psrtUserCode);
          instance.AddInParameter(storedProcCommand, "v_IdProducto", DbType.String, (object) pstrProduct);
          instance.AddInParameter(storedProcCommand, "i_TipoPago", DbType.Int32, (object) pintPaymentType);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable;
      }
      catch (SqlException ex)
      {
        throw new HandledException(ex);
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex);
      }
    }

    public DataTable PaymentAcreditationbyOperation(
      int pintBankId,
      string pstrPaymentDate,
      string pstrOperation,
      string psrtUserCode,
      string pstrProduct)//LISTO ???
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Placas_SolicitudPlaca_AcreditacionpagoPorOperacion]"))
      {
        instance.AddInParameter(storedProcCommand, "i_Banco", DbType.Int32, (object) pintBankId);
        instance.AddInParameter(storedProcCommand, "d_FechaPago", DbType.String, (object) pstrPaymentDate);
        instance.AddInParameter(storedProcCommand, "v_NroOperacion", DbType.String, (object) pstrOperation);
        instance.AddInParameter(storedProcCommand, "v_CodigoIDUsuario", DbType.String, (object) psrtUserCode);
        instance.AddInParameter(storedProcCommand, "v_IdProducto", DbType.String, (object) pstrProduct);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable;
    }

    public DataTable getDataBankbyId(int pintBankId)//LISTO
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataBankbyId;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Placas_DatosBanco_Seleccionar]"))
        {
          instance.AddInParameter(storedProcCommand, "i_IdBanco", DbType.Int32, (object) pintBankId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataBankbyId = new DataTable();
            dataBankbyId.Load(reader);
          }
        }
        return dataBankbyId;
      }
      catch (SqlException ex)
      {
        throw new HandledException(ex);
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex);
      }
    }

    public int getDataBanKConciliate(int pintDataBankid)//LISTO
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_AcreditationVerification]"))
        {
          instance.AddInParameter(storedProcCommand, "i_DataBankId", DbType.Int32, (object) pintDataBankid);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0]["b_Conciliado"]) : 2;
      }
      catch (SqlException ex)
      {
        throw new HandledException(ex);
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex);
      }
    }
  }
}
