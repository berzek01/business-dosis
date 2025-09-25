// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.DA.RequirementQueriesDA
// Assembly: SIIV.Requirement.DA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4532E3C2-7D38-4E0C-8CE9-E0E8FA4FAED3
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.Requirement.DA.dll

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SIIV.Common.Resource;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

#nullable disable
namespace SIIV.Requirement.DA
{
  public class RequirementQueriesDA
  {
    private string cadenaConexion = string.Empty;
    private string cadenaConexionReporting = string.Empty;

    public RequirementQueriesDA()
    {
      this.cadenaConexion = SIIV.Common.DA.Constants.Constants.NombreConexion;
      this.cadenaConexionReporting = SIIV.Common.DA.Constants.Constants.NombreConexionReporting;
    }

    public DataTable UniversalQueryRead(
      string pstrPlateNew,
      int pintStartdate,
      int pintFinishdate,
      string pstrPlateOld,
      string pstrTitleNumber,
      int pintRequirementPlateId,
      string pstrOwnerName,
      int pintCategoryId,
      int pintProcessTypeId,
      int pintStatus,
      string pstrSerial,
      string pstrPaymentCode,
      int pintUserId,
      int pintQueryType,
      int pintiplateTypeId,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UniversalQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNew", DbType.String, (object) pstrPlateNew);
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, "v_PlateOld", DbType.String, (object) pstrPlateOld);
          instance.AddInParameter(storedProcCommand, "v_TitleNumber", DbType.String, (object) pstrTitleNumber);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
          instance.AddInParameter(storedProcCommand, "v_Owner", DbType.String, (object) pstrOwnerName);
          instance.AddInParameter(storedProcCommand, "i_Category", DbType.Int32, (object) pintCategoryId);
          instance.AddInParameter(storedProcCommand, "i_ProcessTypeId", DbType.Int32, (object) pintProcessTypeId);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "v_SerialNumber", DbType.String, (object) pstrSerial);
          instance.AddInParameter(storedProcCommand, "v_PaymentCode", DbType.String, (object) pstrPaymentCode);
          instance.AddInParameter(storedProcCommand, "i_UserId", DbType.Int32, (object) pintUserId);
          instance.AddInParameter(storedProcCommand, "i_QueryType", DbType.Int32, (object) pintQueryType);
          instance.AddInParameter(storedProcCommand, "i_PlateTypeId", DbType.Int32, (object) pintiplateTypeId);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pinttotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable EBillingUniversalQueryRead(
      int pintProfType,
      int pintStartdate,
      int pintFinishdate,
      string pstrProfDocument,
      string pstrClient,
      string pstrCliDocument,
      int pintRequirement,
      int pintUserId,
      int pintQueryType,
      int pintiplateTypeId,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_EBillingUniversalQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ProfType", DbType.Int32, (object) pintProfType);
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, "v_ProfDocument", DbType.String, (object) pstrProfDocument);
          instance.AddInParameter(storedProcCommand, "v_Client", DbType.String, (object) pstrClient);
          instance.AddInParameter(storedProcCommand, "v_CliDocumento", DbType.String, (object) pstrCliDocument);
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirement);
          instance.AddInParameter(storedProcCommand, "i_UserId", DbType.Int32, (object) pintUserId);
          instance.AddInParameter(storedProcCommand, "i_QueryType", DbType.Int32, (object) pintQueryType);
          instance.AddInParameter(storedProcCommand, "i_PlateTypeId", DbType.Int32, (object) pintiplateTypeId);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pinttotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable EBillingQueryExport(
      string pstringProfType,
      int pintStartdate,
      int pintFinishdate,
      string strClient,
      string strDesde,
      string strHasta,
      string strSerieDocument,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_EBillingQueryExport]"))
        {
          instance.AddInParameter(storedProcCommand, "v_tipoDocumento", DbType.String, (object) pstringProfType);
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, "v_SerieDocumento", DbType.String, (object) strSerieDocument);
          instance.AddInParameter(storedProcCommand, "i_CorrelativoIni", DbType.String, (object) strDesde);
          instance.AddInParameter(storedProcCommand, "i_CorrelativoFin", DbType.String, (object) strHasta);
          instance.AddInParameter(storedProcCommand, "v_Cliente", DbType.String, (object) strClient);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pinttotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable CashRegisterUniversalQueryRead(
      int pintWarehouseId,
      string pstrDescription,
      int pintLocationId,
      int pintBoxStatus,
      int pintStatus,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_CashRegisterUniversalQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "i_BoxId", DbType.Int32, (object) pintWarehouseId);
          instance.AddInParameter(storedProcCommand, "v_Description", DbType.String, (object) pstrDescription);
          instance.AddInParameter(storedProcCommand, "i_LocationId", DbType.Int32, (object) pintLocationId);
          instance.AddInParameter(storedProcCommand, "i_BoxStatus", DbType.Int32, (object) pintBoxStatus);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
          instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable BlockPlatesUniversalQueryRead(
      int pintBlockId,
      string pstrPlate,
      int pintStatus,
      int pintUserId,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_BlockPlatesUniversalQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "i_BlockId", DbType.Int32, (object) pintBlockId);
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) pstrPlate);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintUserId);
          instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
          instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable PermissionBlockPlates(int pintUserId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_PermissionBlockPlates]"))
        {
          instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintUserId);
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

    public DataTable GestPlatesRequirementStatus(string pstrPlate)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GestPlatesRequirementStatus]"))
        {
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) pstrPlate);
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

    public DataTable CashRegConciliationQuery(
      int pintStartdate,
      int pintFinishdate,
      int i_CashRegId,
      string pstrDescription,
      int i_LocationId,
      int i_CashRegSStatus,
      int i_Status,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_CashRegConciliationQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
          instance.AddInParameter(storedProcCommand, "v_Description", DbType.String, (object) pstrDescription);
          instance.AddInParameter(storedProcCommand, nameof (i_LocationId), DbType.Int32, (object) i_LocationId);
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegSStatus), DbType.Int32, (object) i_CashRegSStatus);
          instance.AddInParameter(storedProcCommand, nameof (i_Status), DbType.Int32, (object) i_Status);
          instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
          instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable GetRequiredStageByIdProcess(int pintProcessTypeId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable stageByIdProcess;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequiredStageByIdProcess]"))
      {
        instance.AddInParameter(storedProcCommand, "i_ProcessTypeId", DbType.String, (object) pintProcessTypeId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          stageByIdProcess = new DataTable();
          stageByIdProcess.Load(reader);
        }
      }
      stageByIdProcess.Columns.Add("Completo", typeof (int)).DefaultValue = (object) 0;
      return stageByIdProcess;
    }

    public DataTable GetRequirementDatabyPlate(string pstrPlate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable requirementDatabyPlate;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequirementDatabyPlate]"))
      {
        instance.AddInParameter(storedProcCommand, "v_PlateNew", DbType.String, (object) pstrPlate);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          requirementDatabyPlate = new DataTable();
          requirementDatabyPlate.Load(reader);
        }
      }
      return requirementDatabyPlate;
    }

    public DataTable GetRequirementDatabyRetail(int i_RequirementId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable requirementDatabyRetail;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequirementDatabyRetail]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.Int32, (object) i_RequirementId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          requirementDatabyRetail = new DataTable();
          requirementDatabyRetail.Load(reader);
        }
      }
      return requirementDatabyRetail;
    }

    public DataTable GetRequirementDatabyPaymentCode(string v_PaymentCode)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable databyPaymentCode;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequirementDatabyPaymentCode]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (v_PaymentCode), DbType.String, (object) v_PaymentCode);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          databyPaymentCode = new DataTable();
          databyPaymentCode.Load(reader);
        }
      }
      return databyPaymentCode;
    }

    public DataTable GetPaymentDatabyPaymentCode(string v_PaymentCode)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable databyPaymentCode;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetPaymentDatabyPaymentCode]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (v_PaymentCode), DbType.String, (object) v_PaymentCode);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          databyPaymentCode = new DataTable();
          databyPaymentCode.Load(reader);
        }
      }
      return databyPaymentCode;
    }

    public int ValidateExistRequirementByPlateTitle(string pstrPlateNumber, string pstrTitle)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateExistRequirementByPlateTitle]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "v_TitleNumber", DbType.String, (object) pstrTitle);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public int ValidateExistSunarpByPlateTitle(string pstrPlateNumber, string pstrTitle)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateExistSunarpByPlateTitle]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "v_TitleNumber", DbType.String, (object) pstrTitle);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public int ValidateExistSunarpByPlateVinSerie(string pstrPlateNumber, string pstrVinSerie)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateExistSunarpByPlateVinSerie]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "v_VinSerieNumber", DbType.String, (object) pstrVinSerie);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public DataTable ValidateExistDeliveryPoint(int pintDeliveryPointId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateExistDeliveryPoint]"))
        {
          instance.AddInParameter(storedProcCommand, "i_DeliveryPointId", DbType.Int32, (object) pintDeliveryPointId);
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

    public string DeliveryDireccion(int pintDeliveryPointId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        string str;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeliveryDireccion]"))
        {
          instance.AddInParameter(storedProcCommand, "i_DeliveryPointId", DbType.Int32, (object) pintDeliveryPointId);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            str = dataReader[0].ToString();
          }
        }
        return str;
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

    public DataTable SunarpDataRead(string pstrPlateNumber, string pstrTitle, int irol)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataRead]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "v_TitleNumber", DbType.String, (object) pstrTitle);
          instance.AddInParameter(storedProcCommand, nameof (irol), DbType.Int32, (object) irol);
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

    public DataTable SunarpDataReadbyId(int pintVehicleId, int irol)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataReadbyId]"))
        {
          instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.Int32, (object) pintVehicleId);
          instance.AddInParameter(storedProcCommand, nameof (irol), DbType.Int32, (object) irol);
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

    public DataTable SunarpDataRead3rd(string pstrPlateNumber, int irol)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataRead3rd]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, nameof (irol), DbType.Int32, (object) irol);
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

    public DataTable SunarpDataReadChangeUse(string pstrPlateNumber, int irol)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataReadChangeUse]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, nameof (irol), DbType.Int32, (object) irol);
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

    public DataTable SunarpDataReadChangeUseRectification(string pstrPlateNumber, int pintVehicleId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataReadChangeUseRectification]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.Int32, (object) pintVehicleId);
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

    public DataTable GetOwnersByIdSunarp(int pintVehicleId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable ownersByIdSunarp;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetOwnersByIdSunarp]"))
        {
          instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.String, (object) pintVehicleId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            ownersByIdSunarp = new DataTable();
            ownersByIdSunarp.Load(reader);
          }
        }
        return ownersByIdSunarp;
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

    public DataTable GetProofPaymenType()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable proofPaymenType;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "222");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            proofPaymenType = new DataTable();
            proofPaymenType.Load(reader);
            proofPaymenType.Rows[0].Delete();
          }
        }
        return proofPaymenType;
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

    public DataTable GetActionProceesEBilling(int iRequirementID)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable actionProceesEbilling;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[GetActionProceesEBilling]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) iRequirementID);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            actionProceesEbilling = new DataTable();
            actionProceesEbilling.Load(reader);
          }
        }
        return actionProceesEbilling;
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

    public DataTable GetActionProceesCashRegUser(int i_InsertUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable proceesCashRegUser;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[GetActionProceesCashRegUser]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            proceesCashRegUser = new DataTable();
            proceesCashRegUser.Load(reader);
          }
        }
        return proceesCashRegUser;
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

    public DataTable GetCashRegisterList(
      int i_CashRegId,
      int i_LocationId,
      string v_BoxCode,
      string v_BoxIdentification,
      int i_InsertUserId,
      int i_Type)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable cashRegisterList;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_GetCashRegisterList]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
          instance.AddInParameter(storedProcCommand, nameof (i_LocationId), DbType.Int32, (object) i_LocationId);
          instance.AddInParameter(storedProcCommand, "v_CashRegCode", DbType.String, (object) v_BoxCode);
          instance.AddInParameter(storedProcCommand, "v_Identification", DbType.String, (object) v_BoxIdentification);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.String, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_Type), DbType.Int32, (object) i_Type);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            cashRegisterList = new DataTable();
            cashRegisterList.Load(reader);
          }
        }
        return cashRegisterList;
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

    public DataTable GetPersonType()
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable personType;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
      {
        instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "217");
        instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          personType = new DataTable();
          personType.Load(reader);
        }
      }
      return personType;
    }

    public DataTable GetProofPaymenTypeSpecial()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable paymenTypeSpecial;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "222");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "1, 2");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            paymenTypeSpecial = new DataTable();
            paymenTypeSpecial.Load(reader);
          }
        }
        return paymenTypeSpecial;
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

    public DataTable GetDocumentType()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable documentType;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "218");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "1, 2, 3, 4, 32");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            documentType = new DataTable();
            documentType.Load(reader);
          }
        }
        DataRow row = documentType.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "";
        row[2] = (object) 0;
        row[3] = (object) "Seleccione";
        row[4] = (object) "";
        row[5] = (object) "";
        row[6] = (object) 0;
        row[7] = (object) "";
        row[8] = (object) 0;
        row[9] = (object) 0;
        row[10] = (object) "";
        row[11] = (object) "2000-01-01";
        row[12] = (object) "";
        row[13] = (object) "2000-01-01";
        row[14] = (object) "";
        documentType.Rows.InsertAt(row, 0);
        return documentType;
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

    public DataTable GetRequirementPlateStatus()
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable requirementPlateStatus;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
      {
        instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "203");
        instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          requirementPlateStatus = new DataTable();
          requirementPlateStatus.Load(reader);
        }
      }
      DataRow row = requirementPlateStatus.NewRow();
      row[0] = (object) -1;
      row[1] = (object) "";
      row[2] = (object) -3;
      row[3] = (object) "Seleccione";
      row[4] = (object) "";
      row[5] = (object) "";
      row[6] = (object) 0;
      row[7] = (object) "";
      row[8] = (object) 0;
      row[9] = (object) 0;
      row[10] = (object) "";
      row[11] = (object) "2000-01-01";
      row[12] = (object) "";
      row[13] = (object) "2000-01-01";
      row[14] = (object) "";
      requirementPlateStatus.Rows.InsertAt(row, 0);
      return requirementPlateStatus;
    }

    public DataTable GetEbillingProofPaymentType()
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable proofPaymentType;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
      {
        instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "520");
        instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          proofPaymentType = new DataTable();
          proofPaymentType.Load(reader);
        }
      }
      DataRow row = proofPaymentType.NewRow();
      row[0] = (object) -1;
      row[1] = (object) "";
      row[2] = (object) -3;
      row[3] = (object) "Seleccione";
      row[4] = (object) "";
      row[5] = (object) "";
      row[6] = (object) 0;
      row[7] = (object) "";
      row[8] = (object) 0;
      row[9] = (object) 0;
      row[10] = (object) "";
      row[11] = (object) "2000-01-01";
      row[12] = (object) "";
      row[13] = (object) "2000-01-01";
      row[14] = (object) "";
      proofPaymentType.Rows.InsertAt(row, 0);
      return proofPaymentType;
    }

    public DataTable GetLocation(
      string pstrLocationid,
      string pstrDescription,
      string pstrCompanyId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable location;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_LocationRead]"))
        {
          instance.AddInParameter(storedProcCommand, "v_LocationId", DbType.String, (object) pstrLocationid);
          instance.AddInParameter(storedProcCommand, "v_Description", DbType.String, (object) pstrDescription);
          instance.AddInParameter(storedProcCommand, "v_CompanyId", DbType.String, (object) pstrCompanyId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            location = new DataTable();
            location.Load(reader);
          }
        }
        DataRow row = location.NewRow();
        row[0] = (object) 0;
        row[1] = (object) 0;
        row[2] = (object) 0;
        row[3] = (object) 0;
        row[4] = (object) "Seleccione";
        row[5] = (object) "";
        row[6] = (object) "";
        row[7] = (object) 1;
        location.Rows.InsertAt(row, 0);
        return location;
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

    public DataTable GetLocationRequirement()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable locationRequirement;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_LocationReadRequirement]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            locationRequirement = new DataTable();
            locationRequirement.Load(reader);
          }
        }
        DataRow row = locationRequirement.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "Seleccione";
        locationRequirement.Rows.InsertAt(row, 0);
        return locationRequirement;
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

    public DataTable GetLocationRequirement(int i_SystemUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable locationRequirement;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_LocationReadBySystemUserId]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            locationRequirement = new DataTable();
            locationRequirement.Load(reader);
          }
        }
        DataRow row = locationRequirement.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "Seleccione";
        locationRequirement.Rows.InsertAt(row, 0);
        return locationRequirement;
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

    public DataTable GetBank()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable bank;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "209");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            bank = new DataTable();
            bank.Load(reader);
          }
        }
        DataRow row = bank.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "";
        row[2] = (object) 0;
        row[3] = (object) "Seleccione";
        row[4] = (object) "";
        row[5] = (object) "";
        row[6] = (object) 0;
        row[7] = (object) "";
        row[8] = (object) 0;
        row[9] = (object) 0;
        row[10] = (object) "";
        row[11] = (object) "2000-01-01";
        row[12] = (object) "";
        row[13] = (object) "2000-01-01";
        row[14] = (object) "";
        bank.Rows.InsertAt(row, 0);
        return bank;
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

    public DataTable GetChangeUse(int pintUseTypeId, int pintCategoryId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable changeUse;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Common.usp_GetUseTypeChange"))
        {
          instance.AddInParameter(storedProcCommand, "i_UseTypeId", DbType.Int32, (object) pintUseTypeId);
          instance.AddInParameter(storedProcCommand, "i_CategoryId", DbType.Int32, (object) pintCategoryId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            changeUse = new DataTable();
            changeUse.Load(reader);
          }
        }
        DataRow row = changeUse.NewRow();
        row[0] = (object) "0";
        row[1] = (object) "Selccione";
        changeUse.Rows.InsertAt(row, 0);
        return changeUse;
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

    public DataTable GetProductbyTypeUse(int pintUseTypeId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable productbyTypeUse;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetProductbyTypeUse]"))
        {
          instance.AddInParameter(storedProcCommand, "i_UseTypeId", DbType.Int32, (object) pintUseTypeId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            productbyTypeUse = new DataTable();
            productbyTypeUse.Load(reader);
          }
        }
        return productbyTypeUse;
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

    public DataTable GetPriceServiceDelivery(int i_ProductId, int pintCorrespondenceType)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable priceServiceDelivery;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetPriceServiceDelivery]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ProductId), DbType.Int32, (object) i_ProductId);
          instance.AddInParameter(storedProcCommand, "i_CorrespondenceType", DbType.Int32, (object) pintCorrespondenceType);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            priceServiceDelivery = new DataTable();
            priceServiceDelivery.Load(reader);
          }
        }
        return priceServiceDelivery;
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

    public int Verify3rdPlate(string pstrPlateNumber)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_Verify3rdPlate]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public int VerifyProcess3rdPlate(string pstrPlateNumber)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_VerifyProcess3rdPlate]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public DataTable ValidateProductPrice3rd(string pstrPlateNumber, int irol)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateProductPrice3rd]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, nameof (irol), DbType.Int32, (object) irol);
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

    public string GetCorrelativeTitle()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        string correlativeTitle;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_CorrelativeInsertSunarp]"))
        {
          instance.AddInParameter(storedProcCommand, "v_CorrelativeDescription", DbType.String, (object) "TituloAAP");
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            correlativeTitle = dataReader[0].ToString();
          }
        }
        return correlativeTitle;
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

    public DataTable ChangeUseRead(
      string pstrUseType,
      string pstrCategory,
      string pstrUseTargetType)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_ChangeUseRead]"))
        {
          instance.AddInParameter(storedProcCommand, "v_UseType", DbType.String, (object) pstrUseType);
          instance.AddInParameter(storedProcCommand, "v_Category", DbType.String, (object) pstrCategory);
          instance.AddInParameter(storedProcCommand, "v_UseTargetType", DbType.String, (object) pstrUseTargetType);
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

    public DataTable GetUseType()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable useType;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "221");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            useType = new DataTable();
            useType.Load(reader);
          }
        }
        return useType;
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

    public DataTable GetProcess()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable process;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "207");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "1,4,5,6");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            process = new DataTable();
            process.Load(reader);
          }
        }
        DataRow row = process.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "";
        row[2] = (object) 0;
        row[3] = (object) "Seleccione";
        row[4] = (object) "";
        row[5] = (object) "";
        row[6] = (object) 0;
        row[7] = (object) "";
        row[8] = (object) 0;
        row[9] = (object) 0;
        row[10] = (object) "";
        row[11] = (object) "2000-01-01";
        row[12] = (object) "";
        row[13] = (object) "2000-01-01";
        row[14] = (object) "";
        process.Rows.InsertAt(row, 0);
        return process;
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

    public DataTable GetCategory()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable category;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) "204");
          instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
          instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            category = new DataTable();
            category.Load(reader);
          }
        }
        DataRow row = category.NewRow();
        row[0] = (object) 0;
        row[1] = (object) "";
        row[2] = (object) 0;
        row[3] = (object) "Seleccione";
        row[4] = (object) "";
        row[5] = (object) "";
        row[6] = (object) 0;
        row[7] = (object) "";
        row[8] = (object) 0;
        row[9] = (object) 0;
        row[10] = (object) "";
        row[11] = (object) "2000-01-01";
        row[12] = (object) "";
        row[13] = (object) "2000-01-01";
        row[14] = (object) "";
        category.Rows.InsertAt(row, 0);
        return category;
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

    public DataTable GetGroup(string strGroup)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable group;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_SystemParameterGet]"))
      {
        instance.AddInParameter(storedProcCommand, "v_GroupsId", DbType.String, (object) strGroup);
        instance.AddInParameter(storedProcCommand, "v_ParametersId", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Visibles", DbType.String, (object) "");
        instance.AddInParameter(storedProcCommand, "v_Status", DbType.String, (object) "");
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          group = new DataTable();
          group.Load(reader);
        }
      }
      return group;
    }

    public DataTable GenerateCUR(int pintRequirementId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable cur;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementGenerateCur]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            cur = new DataTable();
            cur.Load(reader);
          }
        }
        return cur;
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

    public DataTable GenerateCURByIds(int pintRequirementId, int pintRequirementPlateId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable curByIds;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementGenerateCurByIds]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          curByIds = new DataTable();
          curByIds.Load(reader);
        }
      }
      return curByIds;
    }

    public DataTable GenerateDeliveryCUR(int pintRequirementId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable deliveryCur;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementDeliveryGenerateCur]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            deliveryCur = new DataTable();
            deliveryCur.Load(reader);
          }
        }
        return deliveryCur;
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

    public DataTable GenerateCashRegister(int i_CashRegId, int i_InsertUserId, int i_Type)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable cashRegister;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_GenerateCashRegister]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
        instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
        instance.AddInParameter(storedProcCommand, nameof (i_Type), DbType.Int32, (object) i_Type);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          cashRegister = new DataTable();
          cashRegister.Load(reader);
        }
      }
      return cashRegister;
    }

    public DataTable GetSpecialPlateClass(int pintVehicleRegistration, int pintSpecialPlate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable specialPlateClass;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Registration.usp_GetSpecialPlateClass"))
      {
        instance.AddInParameter(storedProcCommand, "i_RegistrationTypeId", DbType.Int32, (object) pintVehicleRegistration);
        instance.AddInParameter(storedProcCommand, "i_SpecialPlateTypeId", DbType.Int32, (object) pintSpecialPlate);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          specialPlateClass = new DataTable();
          specialPlateClass.Load(reader);
        }
      }
      DataRow row = specialPlateClass.NewRow();
      row[0] = (object) 0;
      row[1] = (object) 0;
      row[2] = (object) 0;
      row[3] = (object) 0;
      row[4] = (object) "Selccione";
      row[5] = (object) 0;
      specialPlateClass.Rows.InsertAt(row, 0);
      return specialPlateClass;
    }

    public DataTable GetSpecialPlate(int pintVehicleRegistration)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable specialPlate;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Registration.usp_GetSpecialPlate"))
        {
          instance.AddInParameter(storedProcCommand, "i_VehicleRegistration", DbType.Int32, (object) pintVehicleRegistration);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            specialPlate = new DataTable();
            specialPlate.Load(reader);
          }
        }
        DataRow row = specialPlate.NewRow();
        row[0] = (object) "0";
        row[1] = (object) "Selccione";
        row[2] = (object) "";
        specialPlate.Rows.InsertAt(row, 0);
        return specialPlate;
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

    public DataTable GetSpecialRequirementType(
      int pintVehicleRegistration,
      int pintSpecialPlateType,
      int pintVehicleClass)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable specialRequirementType;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_GetSpecialRequirementType"))
      {
        instance.AddInParameter(storedProcCommand, "i_RegistrationTypeId", DbType.Int32, (object) pintVehicleRegistration);
        instance.AddInParameter(storedProcCommand, "i_SpecialPlateTypeId", DbType.Int32, (object) pintSpecialPlateType);
        instance.AddInParameter(storedProcCommand, "i_VehicleClassId", DbType.Int32, (object) pintVehicleClass);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          specialRequirementType = new DataTable();
          specialRequirementType.Load(reader);
        }
      }
      DataRow row = specialRequirementType.NewRow();
      row[0] = (object) 0;
      row[1] = (object) 0;
      row[2] = (object) 0;
      row[3] = (object) 0;
      row[4] = (object) "Seleccione";
      row[5] = (object) 0;
      row[6] = (object) 0;
      specialRequirementType.Rows.InsertAt(row, 0);
      return specialRequirementType;
    }

    public DataTable ValidateExistMRE(string pstrPlateNumber, int pintProcessTypeId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_ValidateExistRequirementMRE"))
        {
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) pstrPlateNumber);
          instance.AddInParameter(storedProcCommand, "i_ProcessId", DbType.Int32, (object) pintProcessTypeId);
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

    public DataTable GetProductByRegistrationClass(
      int pintVehicleRegistration,
      int pintVehicleClass)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable registrationClass;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_GetProductByRegistrationClass"))
        {
          instance.AddInParameter(storedProcCommand, "i_vehicleRegistrationid", DbType.Int32, (object) pintVehicleRegistration);
          instance.AddInParameter(storedProcCommand, "i_vehicleclassid", DbType.Int32, (object) pintVehicleClass);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            registrationClass = new DataTable();
            registrationClass.Load(reader);
          }
        }
        return registrationClass;
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

    public DataTable RelatedDocumentRead(
      int i_RoleConfigId,
      int startRowIndex,
      int maxRows,
      out int pintTotalRows)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RelatedDocumentRead]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RoleConfigId), DbType.Int32, (object) i_RoleConfigId);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public int DeleteOffice(int i_RelatedDocumentId, int i_SystemUserId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeleteOffice]"))
        {
          instance.AddInParameter(storedProcCommand, "@i_RelatedDocumentId", DbType.Int32, (object) i_RelatedDocumentId);
          instance.AddInParameter(storedProcCommand, "@i_SystemUserId", DbType.Int32, (object) i_SystemUserId);
          instance.AddOutParameter(storedProcCommand, "@i_Result", DbType.Int32, num);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_Result"));
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

    public string CodeOfficeGenerate()
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        string str;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_OfficeCodeGenerate]"))
        {
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            str = Convert.ToString(dataReader[0].ToString());
          }
        }
        return str;
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

    public DataTable GetRelatedDocument(int i_RoleConfigId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable relatedDocument;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRelatedDocument]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RoleConfigId), DbType.Int32, (object) i_RoleConfigId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            relatedDocument = new DataTable();
            relatedDocument.Load(reader);
          }
        }
        return relatedDocument;
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

    public DataTable GetRelatedDocumentbyId(int id)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable relatedDocumentbyId;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_GetRelatedDocumentById"))
        {
          instance.AddInParameter(storedProcCommand, "i_RelatedDocumentId", DbType.Int32, (object) id);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            relatedDocumentbyId = new DataTable();
            relatedDocumentbyId.Load(reader);
          }
        }
        return relatedDocumentbyId;
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

    public DataTable GetApprovedRequirement(
      int intType,
      int intStartDate,
      int intFinishDate,
      int intAppoved,
      int intProcess,
      string strInsertUser,
      string strOficio)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable approvedRequirement;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_GetApprovedRequirement"))
        {
          instance.AddInParameter(storedProcCommand, "i_Type", DbType.Int32, (object) intType);
          instance.AddInParameter(storedProcCommand, "i_StarDate", DbType.Int32, (object) intStartDate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) intFinishDate);
          instance.AddInParameter(storedProcCommand, "i_Approved", DbType.Int32, (object) intAppoved);
          instance.AddInParameter(storedProcCommand, "i_ProcessId", DbType.Int32, (object) intProcess);
          instance.AddInParameter(storedProcCommand, "v_InsertUserId", DbType.String, (object) strInsertUser);
          instance.AddInParameter(storedProcCommand, "v_Oficio", DbType.String, (object) strOficio);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            approvedRequirement = new DataTable();
            approvedRequirement.Load(reader);
          }
        }
        return approvedRequirement;
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

    public DataTable GetRequirementPlateDatabyId(int pintRequirement)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable requirementPlateDatabyId;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequirementPlateDatabyId]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirement);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          requirementPlateDatabyId = new DataTable();
          requirementPlateDatabyId.Load(reader);
        }
      }
      return requirementPlateDatabyId;
    }

    public DataTable GetSunarpDatabyId(int pintVehicleId, int pintRequirementPlate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable sunarpDatabyId;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Registration].[usp_GetSunarpDatabyId]"))
      {
        instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.Int32, (object) pintVehicleId);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateid", DbType.Int32, (object) pintRequirementPlate);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          sunarpDatabyId = new DataTable();
          sunarpDatabyId.Load(reader);
        }
      }
      return sunarpDatabyId;
    }

    public DataTable GetPaymentDatabyRequirement(int pintRequirement, int pintRequirementPlate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable databyRequirement;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_getPaymentDatabyRequirement]"))
      {
        instance.AddInParameter(storedProcCommand, "i_Requirement", DbType.Int32, (object) pintRequirement);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlate", DbType.Int32, (object) pintRequirementPlate);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          databyRequirement = new DataTable();
          databyRequirement.Load(reader);
        }
      }
      return databyRequirement;
    }

    public DataTable GetDistrict(int i_ParameterId, int iVehicleClassId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable district;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_getDistrictDelivery]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ParameterId), DbType.Int32, (object) i_ParameterId);
          instance.AddInParameter(storedProcCommand, "i_VehicleClassId", DbType.Int32, (object) iVehicleClassId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            district = new DataTable();
            district.Load(reader);
          }
        }
        return district;
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

    public DataTable GetDistrictByDeliveryPoint(
      int pintDeliveryPoint,
      int i_VehicleClasification,
      int i_Cobertura)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable districtByDeliveryPoint;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_getDistrictByDeliveryPoint]"))
        {
          instance.AddInParameter(storedProcCommand, "i_DeliveryPointId", DbType.Int32, (object) pintDeliveryPoint);
          instance.AddInParameter(storedProcCommand, nameof (i_VehicleClasification), DbType.Int32, (object) i_VehicleClasification);
          instance.AddInParameter(storedProcCommand, nameof (i_Cobertura), DbType.Int32, (object) i_Cobertura);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            districtByDeliveryPoint = new DataTable();
            districtByDeliveryPoint.Load(reader);
          }
        }
        return districtByDeliveryPoint;
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

    public DataTable GetProofPaperDatabyRequirement(int pintRequirementPlate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable databyRequirement;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_GetProofPaperDatabyRequirement"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlate);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          databyRequirement = new DataTable();
          databyRequirement.Load(reader);
        }
      }
      return databyRequirement;
    }

    public int GetProductCorrespondence(int pintProductid, int pintCorrespondenceType)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Product].[usp_ProductCorrespondenceGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ProductId", DbType.Int32, (object) pintProductid);
          instance.AddInParameter(storedProcCommand, "i_CorrespondenceType", DbType.Int32, (object) pintCorrespondenceType);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0].ToString()) : 0;
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

    public DataTable GetAgreggateProducts(
      int pintCorrespondenceTypeId,
      int pintVehicleClasificationId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable agreggateProducts;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Product].[usp_ProductAggregateGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_CorrespondenceType", DbType.Int32, (object) pintCorrespondenceTypeId);
          instance.AddInParameter(storedProcCommand, "i_VehicleClasification", DbType.Int32, (object) pintVehicleClasificationId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            agreggateProducts = new DataTable();
            agreggateProducts.Load(reader);
          }
        }
        return agreggateProducts;
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

    public string GetRequisitebyRequirement(int pintRequirementId, int pintProcessId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequisitebyRequirement]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequiremetId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_ProcessTypeId", DbType.Int32, (object) pintProcessId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows[0][0].ToString();
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

    public int GetSystemUserPublic(int pintSystemUserId, int pintApplicationId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_GetSystemUserPublic]"))
        {
          instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintSystemUserId);
          instance.AddInParameter(storedProcCommand, "i_ApplicationId", DbType.Int32, (object) pintApplicationId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0].ToString()) : 0;
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

    public string GetSystemUserExtendedAction(int pintSystemUserId, int pintApplicationId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_GetSystemUserExtendedAction]"))
        {
          instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintSystemUserId);
          instance.AddInParameter(storedProcCommand, "i_ApplicationId", DbType.Int32, (object) pintApplicationId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows.Count > 0 ? dataTable.Rows[0][0].ToString() : "";
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

    public void GetRequirementStatus(
      string pstrPlaetNumber,
      int pintRequirementId,
      out string opstrStatusDescription,
      out string opstrObservations,
      out string opstrSerialNumber,
      out string opstrBrand,
      out string opstrModel,
      out string opstrOwnerCompleteName,
      out string opstrPlatePrevious,
      out string opstrPlateNew,
      out string opstrDuplicate,
      out string opstrDescription,
      out string opstrStatus,
      out string opstrDeliveryPoint,
      out string opstrStartDate,
      out string opstrInsertDate)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexionReporting);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementGetStatus]"))
      {
        storedProcCommand.CommandTimeout = 0;
        instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlaetNumber);
        instance.AddInParameter(storedProcCommand, "v_RequirementPlateId", DbType.Int32, (object) pintRequirementId);
        instance.AddOutParameter(storedProcCommand, "v_StatusDescription", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Observations", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_SerialNumber", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Brand", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Model", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_OwnerCompleteName", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_PlatePrevious", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_PlateNew", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Duplicate", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Description", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_Status", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "v_DeliveryPoint", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "d_StartDate", DbType.String, int.MaxValue);
        instance.AddOutParameter(storedProcCommand, "d_InsertDate", DbType.String, int.MaxValue);
        instance.ExecuteNonQuery(storedProcCommand);
        opstrStatusDescription = instance.GetParameterValue(storedProcCommand, "v_StatusDescription").ToString();
        opstrObservations = instance.GetParameterValue(storedProcCommand, "v_Observations").ToString();
        opstrSerialNumber = instance.GetParameterValue(storedProcCommand, "v_SerialNumber").ToString();
        opstrBrand = instance.GetParameterValue(storedProcCommand, "v_Brand").ToString();
        opstrModel = instance.GetParameterValue(storedProcCommand, "v_Model").ToString();
        opstrOwnerCompleteName = instance.GetParameterValue(storedProcCommand, "v_OwnerCompleteName").ToString();
        opstrPlatePrevious = instance.GetParameterValue(storedProcCommand, "v_PlatePrevious").ToString();
        opstrPlateNew = instance.GetParameterValue(storedProcCommand, "v_PlateNew").ToString();
        opstrDuplicate = instance.GetParameterValue(storedProcCommand, "v_Duplicate").ToString();
        opstrDescription = instance.GetParameterValue(storedProcCommand, "v_Description").ToString();
        opstrStatus = instance.GetParameterValue(storedProcCommand, "v_Status").ToString();
        opstrDeliveryPoint = instance.GetParameterValue(storedProcCommand, "v_DeliveryPoint").ToString();
        opstrStartDate = instance.GetParameterValue(storedProcCommand, "d_StartDate").ToString();
        opstrInsertDate = instance.GetParameterValue(storedProcCommand, "d_InsertDate").ToString();
      }
    }

    public string GetPaymentCode(int pintRequirementPlate)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementPaymentCodeGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlate);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows.Count > 0 ? dataTable.Rows[0][0].ToString() : "";
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

    public DataTable GetRequirementContributor(int pintContributorType, int pintRequirement)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable requirementContributor;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementContributorGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ContributorType", DbType.Int32, (object) pintContributorType);
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirement);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            requirementContributor = new DataTable();
            requirementContributor.Load(reader);
          }
        }
        return requirementContributor;
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

    public int GetRequirementIdByRequirementPlateId(int pintRequirementPlateId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      int int32;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementIdByRequirementPlateIdGet]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
        instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, int.MaxValue);
        instance.ExecuteNonQuery(storedProcCommand);
        int32 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId"));
      }
      return int32;
    }

    public int GetRequirementPlateIdByPlateNumber(string pstrPlateNumber)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      int int32;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementPlateIdByPlateGet]"))
      {
        instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) pstrPlateNumber);
        instance.AddOutParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, int.MaxValue);
        instance.ExecuteNonQuery(storedProcCommand);
        int32 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementPlateId"));
      }
      return int32;
    }

    public DataTable GetRequirementProgramation(string v_Plate)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable requirementProgramation;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementProgramationGetByIds]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_Plate), DbType.String, (object) v_Plate);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            requirementProgramation = new DataTable();
            requirementProgramation.Load(reader);
          }
        }
        return requirementProgramation;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public DataTable GetDeliveryCouriers(int iLocation)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable deliveryCouriers;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementProgramationGetDeliveryCouriers]"))
        {
          instance.AddInParameter(storedProcCommand, "i_locationId", DbType.Int16, (object) iLocation);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            deliveryCouriers = new DataTable();
            deliveryCouriers.Load(reader);
          }
        }
        return deliveryCouriers;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public DataTable GetRequirementSchedule(string v_Plate)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable requirementSchedule;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_BlockSchedule]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_Plate), DbType.String, (object) v_Plate);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            requirementSchedule = new DataTable();
            requirementSchedule.Load(reader);
          }
        }
        return requirementSchedule;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void RequirementGetPrice(
      int pintProductId,
      out Decimal f_PriceCost,
      out Decimal f_PriceTax,
      out Decimal f_PriceSale)
    {
      try
      {
        f_PriceCost = 0M;
        f_PriceTax = 0M;
        f_PriceSale = 0M;
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[SpecialPlate].[usp_RequirementGetPrice]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ProductId", DbType.Int32, (object) pintProductId);
          instance.AddOutParameter(storedProcCommand, nameof (f_PriceCost), DbType.Double, int.MaxValue);
          instance.AddOutParameter(storedProcCommand, nameof (f_PriceTax), DbType.Double, int.MaxValue);
          instance.AddOutParameter(storedProcCommand, nameof (f_PriceSale), DbType.Double, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          f_PriceCost = Convert.ToDecimal(instance.GetParameterValue(storedProcCommand, nameof (f_PriceCost)));
          f_PriceTax = Convert.ToDecimal(instance.GetParameterValue(storedProcCommand, nameof (f_PriceTax)));
          f_PriceSale = Convert.ToDecimal(instance.GetParameterValue(storedProcCommand, nameof (f_PriceSale)));
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

    public DataTable DeliveryListGetByIdZone(int i_ZoneReference, int i_RequirementPlateId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable byIdZone;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeliveryListGetByIdZone]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ZoneReference), DbType.String, (object) i_ZoneReference);
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            byIdZone = new DataTable();
            byIdZone.Load(reader);
          }
        }
        return byIdZone;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR RECUPERAR EL LISTADO DE PLACAS.");
      }
    }

    public DataTable DeliveryListGetByIdZoneOne(int i_RequirementPlateId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable byIdZoneOne;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeliveryListGetByIdZoneOne]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            byIdZoneOne = new DataTable();
            byIdZoneOne.Load(reader);
          }
        }
        return byIdZoneOne;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR RECUPERAR LA SOLICITUD.");
      }
    }

    public DataTable DeliverySendEmail(int i_RequirementPlateId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[RequirementEmailDelivery]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR OBTENER LOS DATOS.");
      }
    }

    public int RequirementProgramationUpdate(
      int i_RequirementPlateId,
      int i_Motive,
      string v_ObservationA,
      int i_SystemUserId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[RequirementProgramationStatusUpdate]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, nameof (i_Motive), DbType.Int32, (object) i_Motive);
          instance.AddInParameter(storedProcCommand, nameof (v_ObservationA), DbType.String, (object) v_ObservationA);
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
        }
      }
      catch (DataException ex)
      {
        Debug.WriteLine(ex.Message);
      }
      return num;
    }

    public DataTable SearchStatusDelivery(
      int i_SystemUserId,
      int i_RequirementPlateId,
      string v_PlateNew,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_Flag,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchDeliveryPlateStatus]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
        instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
        instance.AddInParameter(storedProcCommand, nameof (v_PlateNew), DbType.String, (object) v_PlateNew);
        instance.AddInParameter(storedProcCommand, nameof (i_Status), DbType.Int32, (object) i_Status);
        instance.AddInParameter(storedProcCommand, nameof (d_StartDate), DbType.DateTime, (object) d_StartDate);
        instance.AddInParameter(storedProcCommand, nameof (d_EndDate), DbType.DateTime, (object) d_EndDate);
        instance.AddInParameter(storedProcCommand, nameof (i_Flag), DbType.Int32, (object) i_Flag);
        instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
        instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
        instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
        pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
      }
      return dataTable;
    }

    public DataTable SearchStatusDeliveryReport()
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchDeliveryPlateStatusDetailReport]"))
      {
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable;
    }

    public DataTable SearchDeliveryPlatePaymentReport(
      int i_SystemUserId,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Reports].[SearchDeliveryPlatePaymentReport]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
        instance.AddInParameter(storedProcCommand, nameof (d_StartDate), DbType.DateTime, (object) d_StartDate);
        instance.AddInParameter(storedProcCommand, "d_FinishDate", DbType.DateTime, (object) d_EndDate);
        instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
        instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
        instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
        pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
      }
      return dataTable;
    }

    public DataTable SearchStatusDeliveryDetail(int i_RequirementPlateId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchDeliveryPlateStatusDetail]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable;
    }

    public DataTable GetGroupsDelivery(int i_GroupId, int i_ParameterId, string v_option)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable groupsDelivery = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetGroupsDelivery]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_GroupId), DbType.Int32, (object) i_GroupId);
        instance.AddInParameter(storedProcCommand, nameof (i_ParameterId), DbType.Int32, (object) i_ParameterId);
        instance.AddInParameter(storedProcCommand, nameof (v_option), DbType.String, (object) v_option);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          groupsDelivery = new DataTable();
          groupsDelivery.Load(reader);
        }
      }
      return groupsDelivery;
    }

    public DataTable GetVReferenceDelivery(int i_GroupId, int i_ParameterId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable vreferenceDelivery = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_VReferenceDelivery]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_GroupId), DbType.Int32, (object) i_GroupId);
        instance.AddInParameter(storedProcCommand, nameof (i_ParameterId), DbType.Int32, (object) i_ParameterId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          vreferenceDelivery = new DataTable();
          vreferenceDelivery.Load(reader);
        }
      }
      return vreferenceDelivery;
    }

    public DataTable GetRequirementProgrmationDelivery(
      int i_RequirementPlateid,
      int pintSystemUserId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable progrmationDelivery = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetRequirementProgrmationDelivery]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateid), DbType.Int32, (object) i_RequirementPlateid);
        instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintSystemUserId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          progrmationDelivery = new DataTable();
          progrmationDelivery.Load(reader);
        }
      }
      return progrmationDelivery;
    }

    public DataTable ProcessSunarpToProcessType(string v_PlateMotive)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable processType = (DataTable) null;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ProcessSunarpToProcessType]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_PlateMotive), DbType.String, (object) v_PlateMotive);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            processType = new DataTable();
            processType.Load(reader);
          }
        }
        return processType;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR OBTENER EL TIPO DE PROCESO.");
      }
    }

    public string GetDescriptionProduct(int pintProductid)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Product].[usp_ProductDescriptionGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ProductId", DbType.Int32, (object) pintProductid);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows[0][0].ToString();
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

    public int GetCantDeliveryServices(int i_SystemUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_CantDeliveryServicesGet]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0].ToString()) : 0;
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

    public DataTable GetBlockShedule(int i_BlockSheduleId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable blockShedule;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetBlockSchedule]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_BlockSheduleId), DbType.Int32, (object) i_BlockSheduleId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            blockShedule = new DataTable();
            blockShedule.Load(reader);
          }
        }
        return blockShedule;
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

    public DataTable GetBlockSheduleZone(string v_ReferenceId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable blockSheduleZone;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetBlockScheduleZone]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_ReferenceId), DbType.String, (object) v_ReferenceId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            blockSheduleZone = new DataTable();
            blockSheduleZone.Load(reader);
          }
        }
        return blockSheduleZone;
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

    public DataTable SearchBlockSheduleZone(DateTime dt_Date, int i_BlockSheduleId, int i_ZoneId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchBlockScheduleZone]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (dt_Date), DbType.DateTime, (object) dt_Date);
          instance.AddInParameter(storedProcCommand, nameof (i_BlockSheduleId), DbType.Int32, (object) i_BlockSheduleId);
          instance.AddInParameter(storedProcCommand, nameof (i_ZoneId), DbType.Int32, (object) i_ZoneId);
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

    public DataTable ZoneByLocationGet(string pintLocationId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ZoneByLocationGet]"))
        {
          instance.AddInParameter(storedProcCommand, "v_LocationId", DbType.String, (object) pintLocationId);
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

    public DataTable SearchExceptionsByLocation(
      int pintLocationId,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ExceptionsDeliveryByLocationGet]"))
        {
          instance.AddInParameter(storedProcCommand, "i_LocationId", DbType.Int32, (object) pintLocationId);
          instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
          instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable GetDescriptionExceptionsByType(int pinTypeExceptionsId, int pinZoneId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable exceptionsByType;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDescriptionExceptionsByType]"))
        {
          instance.AddInParameter(storedProcCommand, "i_TypeExceptionsId", DbType.Int32, (object) pinTypeExceptionsId);
          instance.AddInParameter(storedProcCommand, "i_ZoneId", DbType.Int32, (object) pinZoneId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            exceptionsByType = new DataTable();
            exceptionsByType.Load(reader);
          }
        }
        return exceptionsByType;
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

    public int GetCantDeliveryRequirement(
      string v_Plate,
      int iZonaId,
      int iDistrictId,
      int ischedule,
      string vDate)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_CantDeliveryRequirementGet]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_Plate), DbType.String, (object) v_Plate);
          instance.AddInParameter(storedProcCommand, "i_ZonaId", DbType.Int32, (object) iZonaId);
          instance.AddInParameter(storedProcCommand, "i_DistrictId", DbType.Int32, (object) iDistrictId);
          instance.AddInParameter(storedProcCommand, "i_schedule", DbType.Int32, (object) ischedule);
          instance.AddInParameter(storedProcCommand, "v_Date", DbType.String, (object) vDate);
          instance.AddOutParameter(storedProcCommand, "Return", DbType.Int32, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "Return"));
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

    public DataTable RequirementGetByPaymentCode(string v_PaymentCode, int i_RequirementId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable byPaymentCode;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementGetByPaymentCode]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_PaymentCode), DbType.String, (object) v_PaymentCode);
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.Int32, (object) i_RequirementId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            byPaymentCode = new DataTable();
            byPaymentCode.Load(reader);
          }
        }
        return byPaymentCode;
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

    public int UpdateRequirementBoundVisa(int i_RequirementId)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_UpdateRequirementBoundVisa]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.String, (object) i_RequirementId);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int UpdateRequirementBoundCash(int i_RequirementId)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_UpdateRequirementBoundCash]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.String, (object) i_RequirementId);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int UpdateETicketVisa(string v_PaymentCode, string v_Eticket, string v_NumOrden)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_UpdateETicketVisa]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_PaymentCode), DbType.String, (object) v_PaymentCode);
          instance.AddInParameter(storedProcCommand, nameof (v_Eticket), DbType.String, (object) v_Eticket);
          instance.AddInParameter(storedProcCommand, nameof (v_NumOrden), DbType.String, (object) v_NumOrden);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int AccreditPayment(string Cod, int Type, string IdTrans)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_AccreditPayment]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (Cod), DbType.String, (object) Cod);
          instance.AddInParameter(storedProcCommand, nameof (Type), DbType.Int16, (object) Type);
          instance.AddInParameter(storedProcCommand, nameof (IdTrans), DbType.String, (object) IdTrans);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int EBillingPayment()
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_EBillingPayment]"))
        {
          storedProcCommand.CommandTimeout = 120;
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public DataTable GetEBillingIssued()
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable ebillingIssued;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_GetEBillingIssued]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            ebillingIssued = new DataTable();
            ebillingIssued.Load(reader);
          }
        }
        return ebillingIssued;
      }
      catch (SqlException ex)
      {
        throw new Exception(ex.Message);
      }
      catch (Exception ex)
      {
        throw new Exception("ERROR AL CONSULTAR EL LISTADO.");
      }
    }

    public DataTable GetEBillingInternal()
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable ebillingInternal;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_GetEBillingInternal]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            ebillingInternal = new DataTable();
            ebillingInternal.Load(reader);
          }
        }
        return ebillingInternal;
      }
      catch (SqlException ex)
      {
        throw new Exception(ex.Message);
      }
      catch (Exception ex)
      {
        throw new Exception("ERROR AL CONSULTAR EL LISTADO.");
      }
    }

    public DataTable ETicketVisaGet(string v_Eticket)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ETicketVisaGet]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_Eticket), DbType.String, (object) v_Eticket);
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

    public string ElectronicBillGet(int i_requirementPlateId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ElectronicBillGet]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_requirementPlateId), DbType.Int32, (object) i_requirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows[0][0].ToString();
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

    public int UpdateDeletePaymentRequirement(string v_PaymentCode, int i_SystemUserId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeletePaymentS]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_PaymentCode), DbType.String, (object) v_PaymentCode);
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.String, (object) i_SystemUserId);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public DataTable GetLogServiceVisaByStatusPayment()
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable visaByStatusPayment;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ServiceVisaLogGetByStatusPayment]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            visaByStatusPayment = new DataTable();
            visaByStatusPayment.Load(reader);
          }
        }
        return visaByStatusPayment;
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

    public int ProcessAccreditPaymentVisa()
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_AccreditPaymentPendingVISA]"))
          num = instance.ExecuteNonQuery(storedProcCommand);
        return num;
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

    public int ProcessAccreditPaymentBanks()
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_AccreditPaymentPendingBanks]"))
          num = instance.ExecuteNonQuery(storedProcCommand);
        return num;
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

    public int CancelPayment(string Cod, int Type)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_CancelPayment]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (Cod), DbType.String, (object) Cod);
          instance.AddInParameter(storedProcCommand, nameof (Type), DbType.Int16, (object) Type);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public DataTable RetailGetByProduct(
      int i_ProductId,
      int i_ProductTypeId,
      int i_ProductUseId,
      int i_CategoryId,
      string v_Description,
      int startRowIndex,
      int maxRows,
      out int totalRows)
    {
      try
      {
        DataTable byProduct = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RetailGetByProduct]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ProductId), DbType.Int16, (object) i_ProductId);
          instance.AddInParameter(storedProcCommand, nameof (i_ProductTypeId), DbType.Int16, (object) i_ProductTypeId);
          instance.AddInParameter(storedProcCommand, nameof (i_ProductUseId), DbType.Int16, (object) i_ProductUseId);
          instance.AddInParameter(storedProcCommand, nameof (i_CategoryId), DbType.Int16, (object) i_CategoryId);
          instance.AddInParameter(storedProcCommand, nameof (v_Description), DbType.String, (object) v_Description);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, nameof (totalRows), DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            byProduct = new DataTable();
            byProduct.Load(reader);
          }
          totalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, nameof (totalRows)));
        }
        return byProduct;
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

    public DataTable DeliveryUniversalQueryRead(
      string pstrPlateNew,
      int pintStartdate,
      int pintFinishdate,
      int pintRequirementId,
      string pstrOwnerName,
      int pintStatus,
      string pstrPaymentCode,
      int pintUserId,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_DeliveryUniversalQuery]"))
        {
          instance.AddInParameter(storedProcCommand, "v_PlateNew", DbType.String, (object) pstrPlateNew);
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "v_Owner", DbType.String, (object) pstrOwnerName);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "v_PaymentCode", DbType.String, (object) pstrPaymentCode);
          instance.AddInParameter(storedProcCommand, "i_UserId", DbType.Int32, (object) pintUserId);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
          pinttotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
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

    public DataTable GetManufacturedPlates(
      int i_SystemUserId,
      int i_DeliveryPointId,
      int i_ProcessTypeId,
      int pintStartdate,
      int pintFinishdate,
      string v_platenumber,
      string v_OwnerCompleteName,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      try
      {
        DataTable manufacturedPlates = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetManufacturedPlates]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_DeliveryPointId), DbType.Int16, (object) i_DeliveryPointId);
          instance.AddInParameter(storedProcCommand, nameof (i_ProcessTypeId), DbType.Int16, (object) i_ProcessTypeId);
          instance.AddInParameter(storedProcCommand, "i_StartDate", DbType.Int32, (object) pintStartdate);
          instance.AddInParameter(storedProcCommand, "i_FinishDate", DbType.Int32, (object) pintFinishdate);
          instance.AddInParameter(storedProcCommand, "v_PlateNumber", DbType.String, (object) v_platenumber);
          instance.AddInParameter(storedProcCommand, nameof (v_OwnerCompleteName), DbType.String, (object) v_OwnerCompleteName);
          instance.AddInParameter(storedProcCommand, "startRowIndex", DbType.Int32, (object) pintStartRowIndex);
          instance.AddInParameter(storedProcCommand, "maxRows", DbType.Int32, (object) pintMaxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            manufacturedPlates = new DataTable();
            manufacturedPlates.Load(reader);
          }
          pintTotalRows = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "totalRows"));
        }
        return manufacturedPlates;
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

    public DataTable GetDataManufacturedByPlate(int i_RequirementPlateId)
    {
      try
      {
        DataTable manufacturedByPlate = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDataManufacturedByPlate]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            manufacturedByPlate = new DataTable();
            manufacturedByPlate.Load(reader);
          }
        }
        return manufacturedByPlate;
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

    public int RequirementCallInsert(ArrayList arrFilter)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementCallInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "@i_RequirementCallId", DbType.Int32, arrFilter[0]);
          instance.AddInParameter(storedProcCommand, "@i_RequirementPlateId", DbType.Int32, arrFilter[1]);
          instance.AddInParameter(storedProcCommand, "@i_MotiveNegative", DbType.Int32, arrFilter[2]);
          instance.AddInParameter(storedProcCommand, "@v_Observacion", DbType.String, arrFilter[3]);
          instance.AddInParameter(storedProcCommand, "@i_Status", DbType.String, arrFilter[4]);
          instance.AddInParameter(storedProcCommand, "@i_SystemUserId", DbType.String, arrFilter[5]);
          instance.AddInParameter(storedProcCommand, "@i_isComplete", DbType.Int32, arrFilter[6]);
          instance.AddOutParameter(storedProcCommand, "@i_Result", DbType.Int32, num);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_Result"));
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

    public DataTable GetDetailbyProductDelivery(int i_ProductId)
    {
      try
      {
        DataTable detailbyProductDelivery = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDetailbyProductDelivery]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ProductId), DbType.Int32, (object) i_ProductId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            detailbyProductDelivery = new DataTable();
            detailbyProductDelivery.Load(reader);
          }
        }
        return detailbyProductDelivery;
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

    public int StockMovemenDeliverytInsertTransfer(string CodOrden)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Warehouse].[usp_StockMovementDeliveryAcreditTransfer]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (CodOrden), DbType.String, (object) CodOrden);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int UpdateRequirementCancel(int i_RequirementId)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateRequirementCancel]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.String, (object) i_RequirementId);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int ValidateExistRequirementDelivery(int i_RequirementPlateId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ValidateExistRequirementDelivery]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader dataReader = instance.ExecuteReader(storedProcCommand))
          {
            dataReader.Read();
            int32 = Convert.ToInt32(dataReader[0].ToString());
          }
        }
        return int32;
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

    public DataTable GetRequirementTrazability()
    {
      try
      {
        DataTable requirementTrazability = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDataTrazabilitySendEmail]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            requirementTrazability = new DataTable();
            requirementTrazability.Load(reader);
          }
        }
        return requirementTrazability;
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

    public DataTable GetPreviousStatusRequirement(int i_RequirementPlateId)
    {
      try
      {
        DataTable statusRequirement = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetPreviousStatusRequirement]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            statusRequirement = new DataTable();
            statusRequirement.Load(reader);
          }
        }
        return statusRequirement;
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

    public DataTable GetDataTrazabilityId(int i_RequirementPlateId)
    {
      try
      {
        DataTable dataTrazabilityId = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDataTrazabilityId]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTrazabilityId = new DataTable();
            dataTrazabilityId.Load(reader);
          }
        }
        return dataTrazabilityId;
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

    public DataTable GetDataTrazabilityMasiveId(int i_RequirementId)
    {
      try
      {
        DataTable trazabilityMasiveId = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetDataTrazabilityMasiveId]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementId), DbType.Int32, (object) i_RequirementId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            trazabilityMasiveId = new DataTable();
            trazabilityMasiveId.Load(reader);
          }
        }
        return trazabilityMasiveId;
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

    public int UpdateConfirmationSendEmail(
      int i_EmailNotificationId,
      bool b_ResultSend,
      string v_Observation)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateConfirmationSendEmail]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_EmailNotificationId), DbType.Int32, (object) i_EmailNotificationId);
          instance.AddInParameter(storedProcCommand, nameof (b_ResultSend), DbType.Boolean, (object) b_ResultSend);
          instance.AddInParameter(storedProcCommand, nameof (v_Observation), DbType.String, (object) v_Observation);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int UpdateConfirmationAlertVehicleSendEmail(
      int i_AlertVehicleEmailId,
      int i_AlertVehicleId,
      int i_ResultSend,
      string v_Observation)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateConfirmationAlertVehicleSendEmail]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_AlertVehicleEmailId), DbType.Int32, (object) i_AlertVehicleEmailId);
          instance.AddInParameter(storedProcCommand, nameof (i_AlertVehicleId), DbType.Int32, (object) i_AlertVehicleId);
          instance.AddInParameter(storedProcCommand, nameof (i_ResultSend), DbType.Int32, (object) i_ResultSend);
          instance.AddInParameter(storedProcCommand, nameof (v_Observation), DbType.String, (object) v_Observation);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public int UpdateConfirmTransferDeliverySendEmail()
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ConfirmTransferDeliverySendEmail]"))
          num = instance.ExecuteNonQuery(storedProcCommand);
        return num;
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

    public int UpdateConfirmationReadEmail(int i_TypeNotificationId, int i_NotificationId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateConfirmationReadEmail]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_TypeNotificationId), DbType.Int32, (object) i_TypeNotificationId);
          instance.AddInParameter(storedProcCommand, "i_EmailNotificationId", DbType.Int32, (object) i_NotificationId);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public DataTable GetFabricatedPlateAlert()
    {
      try
      {
        DataTable fabricatedPlateAlert = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetFabricatedPlateAlert]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            fabricatedPlateAlert = new DataTable();
            fabricatedPlateAlert.Load(reader);
          }
        }
        return fabricatedPlateAlert;
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

    public DataTable GetAlertVehicleEmail()
    {
      try
      {
        DataTable alertVehicleEmail = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetAlertVehicleEmail]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            alertVehicleEmail = new DataTable();
            alertVehicleEmail.Load(reader);
          }
        }
        return alertVehicleEmail;
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

    public DataTable GetGestorRequirement(int i_GestorRequirementId, int i_TypeId)
    {
      try
      {
        DataTable gestorRequirement = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetGestorRequirement]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_GestorRequirementId), DbType.Int32, (object) i_GestorRequirementId);
          instance.AddInParameter(storedProcCommand, nameof (i_TypeId), DbType.Int32, (object) i_TypeId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            gestorRequirement = new DataTable();
            gestorRequirement.Load(reader);
          }
        }
        return gestorRequirement;
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

    public int ProcessGestorRequirement(int i_GestorRequirementId)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ProcessGestorRequirement]"))
        {
          instance.AddInParameter(storedProcCommand, "@i_GestorRequirementId", DbType.Int32, (object) i_GestorRequirementId);
          instance.AddOutParameter(storedProcCommand, "@i_Result", DbType.Int32, num);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_Result"));
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

    public int GenerateProcessProductive(int i_RequirementId)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_GenerateProcessProductive]"))
        {
          instance.AddInParameter(storedProcCommand, "@i_RequirementId", DbType.Int32, (object) i_RequirementId);
          return Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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

    public int UpdatePhoneNumber(
      int i_RequirementPlateId,
      int i_SystemUserId,
      string v_PhoneNumberNew)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdatePhoneNumber]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, nameof (i_SystemUserId), DbType.Int32, (object) i_SystemUserId);
          instance.AddInParameter(storedProcCommand, "v_PhoneNumber", DbType.String, (object) v_PhoneNumberNew);
          num = instance.ExecuteNonQuery(storedProcCommand);
        }
        return num;
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

    public DataTable GetConciliacionPlate(string v_PlateNew, int i_ActionId)
    {
      try
      {
        DataTable conciliacionPlate = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchPlateConciliacion]"))
        {
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) v_PlateNew);
          instance.AddInParameter(storedProcCommand, nameof (i_ActionId), DbType.Int32, (object) i_ActionId);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            conciliacionPlate = new DataTable();
            conciliacionPlate.Load(reader);
          }
        }
        return conciliacionPlate;
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

    public DataTable GetDeliveryProduct()
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable deliveryProduct;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Product].[usp_GetDeliveryProduct]"))
        {
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            deliveryProduct = new DataTable();
            deliveryProduct.Load(reader);
          }
        }
        return deliveryProduct;
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

    public string GetDeliveryProductOfDistrict(int i_DistrictId, int i_VehicleClasification)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        DataTable dataTable = (DataTable) null;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Product].[usp_GetDeliveryProductOfDistrict]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_DistrictId), DbType.Int32, (object) i_DistrictId);
          instance.AddInParameter(storedProcCommand, nameof (i_VehicleClasification), DbType.Int32, (object) i_VehicleClasification);
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            dataTable = new DataTable();
            dataTable.Load(reader);
          }
        }
        return dataTable.Rows[0][0].ToString();
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

    public DataTable SearchDeliveryData(int i_RequirementPlateId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable = (DataTable) null;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchDeliveryRequirement]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (i_RequirementPlateId), DbType.Int32, (object) i_RequirementPlateId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable;
    }

    public int InsertPaymentLog(ArrayList arrFilter)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_PaymentVisaLogInsert]"))
        {
          instance.AddOutParameter(storedProcCommand, "@i_PaymentVisaLogId", DbType.Int32, int.MaxValue);
          instance.AddInParameter(storedProcCommand, "@i_RequirementId", DbType.Int32, arrFilter[1]);
          instance.AddInParameter(storedProcCommand, "@v_PaymentCode", DbType.String, arrFilter[2]);
          instance.AddInParameter(storedProcCommand, "@f_PriceTotal", DbType.Decimal, arrFilter[3]);
          instance.AddInParameter(storedProcCommand, "@v_OrderNumber", DbType.String, arrFilter[4]);
          instance.AddInParameter(storedProcCommand, "@v_TransactionVisaId", DbType.String, arrFilter[5]);
          instance.AddInParameter(storedProcCommand, "@v_VisaResponse", DbType.String, arrFilter[6]);
          instance.AddInParameter(storedProcCommand, "@i_InsertUserId", DbType.Int32, arrFilter[7]);
          instance.AddInParameter(storedProcCommand, "@d_InsertDate", DbType.DateTime, arrFilter[8]);
          instance.AddInParameter(storedProcCommand, "@i_UpdateUserId", DbType.Int32, arrFilter[9]);
          instance.AddInParameter(storedProcCommand, "@d_UpdateDate", DbType.DateTime, arrFilter[10]);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_PaymentVisaLogId"));
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

    public int UpdatePaymentLog(ArrayList arrFilter)
    {
      try
      {
        int num = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_PaymentVisaLogUpdate]"))
        {
          instance.AddInParameter(storedProcCommand, "@i_PaymentVisaLogId", DbType.Int32, arrFilter[0]);
          instance.AddInParameter(storedProcCommand, "@i_RequirementId", DbType.Int32, arrFilter[1]);
          instance.AddInParameter(storedProcCommand, "@v_PaymentCode", DbType.String, arrFilter[2]);
          instance.AddInParameter(storedProcCommand, "@f_PriceTotal", DbType.Decimal, arrFilter[3]);
          instance.AddInParameter(storedProcCommand, "@v_OrderNumber", DbType.String, arrFilter[4]);
          instance.AddInParameter(storedProcCommand, "@v_TransactionVisaId", DbType.String, arrFilter[5]);
          instance.AddInParameter(storedProcCommand, "@v_VisaResponse", DbType.String, arrFilter[6]);
          instance.AddInParameter(storedProcCommand, "@i_InsertUserId", DbType.Int32, arrFilter[7]);
          instance.AddInParameter(storedProcCommand, "@d_InsertDate", DbType.DateTime, arrFilter[8]);
          instance.AddInParameter(storedProcCommand, "@i_UpdateUserId", DbType.Int32, arrFilter[9]);
          instance.AddInParameter(storedProcCommand, "@d_UpdateDate", DbType.DateTime, arrFilter[10]);
          instance.ExecuteNonQuery(storedProcCommand);
          return num;
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

    public int InsertServiceVisaLog(ArrayList arrFilter)
    {
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ServiceVisaLogInsert]"))
        {
          instance.AddOutParameter(storedProcCommand, "@i_ServiceVisaLogId", DbType.Int32, int.MaxValue);
          instance.AddInParameter(storedProcCommand, "@i_RequirementId", DbType.Int32, arrFilter[1]);
          instance.AddInParameter(storedProcCommand, "@v_PaymentCode", DbType.String, arrFilter[2]);
          instance.AddInParameter(storedProcCommand, "@v_OrderNumber", DbType.String, arrFilter[3]);
          instance.AddInParameter(storedProcCommand, "@v_TransactionVisaId", DbType.String, arrFilter[4]);
          instance.AddInParameter(storedProcCommand, "@v_ActionCode", DbType.String, arrFilter[5]);
          instance.AddInParameter(storedProcCommand, "@v_ServiceResponse", DbType.String, arrFilter[6]);
          instance.AddInParameter(storedProcCommand, "@i_InsertUserId", DbType.Int32, arrFilter[7]);
          instance.AddInParameter(storedProcCommand, "@d_InsertDate", DbType.DateTime, arrFilter[8]);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_ServiceVisaLogId"));
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

    public DataTable GetPaymentLogById(int i_PaymentVisaLogId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable paymentLogById;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_PaymentVisaLogGetbyId]"))
      {
        instance.AddInParameter(storedProcCommand, "@i_PaymentVisaLogId", DbType.Int32, (object) i_PaymentVisaLogId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          paymentLogById = new DataTable();
          paymentLogById.Load(reader);
        }
      }
      return paymentLogById;
    }

    public DataTable GetServiceVisaLogByPaymentCode(string s_PaymentCode)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable logByPaymentCode;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ServiceVisaLogGetByPaymentCode]"))
      {
        instance.AddInParameter(storedProcCommand, "@v_PaymentCode", DbType.String, (object) s_PaymentCode);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          logByPaymentCode = new DataTable();
          logByPaymentCode.Load(reader);
        }
      }
      return logByPaymentCode;
    }

    public string GetPlatebyRequirementPlateId(int iRequirementPlateId)
    {
      Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GetPlatebyRequirementPlateId]"))
      {
        instance.AddInParameter(storedProcCommand, "@i_RequirementPlateId", DbType.Int32, (object) iRequirementPlateId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable.Rows[0]["v_PlateNew"].ToString();
    }

    public DataTable ConciliarRequirementPlatebyId(int iRequirementPlateId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable dataTable;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_ReconcilePayment]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) iRequirementPlateId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
      }
      return dataTable;
    }

    public DataTable GeneratePaymentCodebyRequirementId(int iRequirementId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable codebyRequirementId;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_GeneratePaymentCode]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) iRequirementId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          codebyRequirementId = new DataTable();
          codebyRequirementId.Load(reader);
        }
      }
      return codebyRequirementId;
    }

    public void RegisterConciliacionAudit(string vDetails, int iuserId, string vResult)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_RequirementAudit]"))
      {
        instance.AddInParameter(storedProcCommand, "v_Details", DbType.String, (object) vDetails);
        instance.AddInParameter(storedProcCommand, "i_UserID", DbType.Int32, (object) iuserId);
        instance.AddInParameter(storedProcCommand, "v_Result", DbType.String, (object) vResult);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          new DataTable().Load(reader);
      }
    }

    public DataTable GetServiceByUserRole(string RoleId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      DataTable serviceByUserRole;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_GetUserServiceByRole]"))
      {
        instance.AddInParameter(storedProcCommand, nameof (RoleId), DbType.String, (object) RoleId);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          serviceByUserRole = new DataTable();
          serviceByUserRole.Load(reader);
        }
      }
      return serviceByUserRole;
    }
  }
}
