// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.DA.DataBankManagementDA
// Assembly: SIIV.Requirement.DA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4532E3C2-7D38-4E0C-8CE9-E0E8FA4FAED3
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.Requirement.DA.dll

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
  public class DataBankManagementDA
  {
    private string cadenaConexion = string.Empty;

    public DataBankManagementDA() => this.cadenaConexion = SIIV.Common.DA.Constants.Constants.NombreConexion;

    public void ConciliatDataBank(int pintCode, string pstrPlate, int pintDataBank)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ConciliatDataBank]"))
        {
          instance.AddInParameter(storedProcCommand, "Codigo", DbType.Int32, (object) pintCode);
          instance.AddInParameter(storedProcCommand, "v_NroPlacaNueva", DbType.String, (object) pstrPlate);
          instance.AddInParameter(storedProcCommand, "i_IdDatosBanco", DbType.Int32, (object) pintDataBank);
          instance.ExecuteNonQuery(storedProcCommand);
        }
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
