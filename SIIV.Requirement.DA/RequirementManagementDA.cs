// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.DA.RequirementManagementDA
// Assembly: SIIV.Requirement.DA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4532E3C2-7D38-4E0C-8CE9-E0E8FA4FAED3
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.Requirement.DA.dll

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

#nullable disable
namespace SIIV.Requirement.DA
{
  public class RequirementManagementDA
  {
    private string cadenaConexion = string.Empty;

    public RequirementManagementDA() => this.cadenaConexion = SIIV.Common.DA.Constants.Constants.NombreConexion;

    public int RequirementDeliveryFinishUpdate(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary)
    {
      try
      {
        int num = 0;
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementDeliveryFinishUpdate]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pobjRequirement.i_RequirementId);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pobjRequirement.i_Status);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Email", DbType.String, (object) pobjBenificiary.v_Email);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBenificiary.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBenificiary.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBenificiary.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBenificiary.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBenificiary.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBenificiary.v_Address);
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

    public int RequirementDeliveryOrderInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester,
      RequirementProgramation pobRequirementProgramation)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementDeliveryOrderInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementTypeId", DbType.Int32, (object) pobjRequirement.i_RequirementTypeId);
          instance.AddInParameter(storedProcCommand, "f_Quantity", DbType.Double, (object) pobjRequirement.f_Quantity);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjRequirement.v_Observations);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pobjRequirement.i_Status);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Ubigeo", DbType.String, (object) pobjRequirement.v_Ubigeo);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdRequester", DbType.String, (object) pobjRequester.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberRequester", DbType.String, (object) pobjRequester.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameRequester", DbType.String, (object) pobjRequester.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameRequester", DbType.String, (object) pobjRequester.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameRequester", DbType.String, (object) pobjRequester.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressRequester", DbType.String, (object) pobjRequester.v_Address);
          instance.AddInParameter(storedProcCommand, "v_UbigeoRequester", DbType.String, (object) pobjRequester.v_AddressLocation);
          instance.AddInParameter(storedProcCommand, "v_PhoneNumberRequester", DbType.String, (object) pobjRequester.v_PhoneNumber);
          instance.AddInParameter(storedProcCommand, "v_Email", DbType.String, (object) pobjRequester.v_Email);
          instance.AddInParameter(storedProcCommand, "i_PersonTypeRequester", DbType.String, (object) pobjRequester.i_PersonTypeId);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBenificiary.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBenificiary.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBenificiary.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBenificiary.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBenificiary.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBenificiary.v_Address);
          instance.AddInParameter(storedProcCommand, "i_PaymentTypeId", DbType.Int32, (object) pobjPayment.i_PaymentTypeId);
          instance.AddInParameter(storedProcCommand, "f_PriceSale", DbType.Double, (object) pobjPayment.f_PriceSale);
          instance.AddInParameter(storedProcCommand, "f_PriceTax", DbType.Double, (object) pobjPayment.f_PriceTax);
          instance.AddInParameter(storedProcCommand, "f_PriceTotal", DbType.Double, (object) pobjPayment.f_PriceTotal);
          instance.AddInParameter(storedProcCommand, "i_BankId", DbType.Int32, (object) pobjPayment.i_BankId);
          instance.AddInParameter(storedProcCommand, "v_BankOperationNumber", DbType.String, (object) pobjPayment.v_BankOperationNumber);
          instance.AddInParameter(storedProcCommand, "v_BankOperationUser", DbType.String, (object) pobjPayment.v_BankOperationUser);
          instance.AddInParameter(storedProcCommand, "v_BankOperationTerminal", DbType.String, (object) pobjPayment.v_BankOperationTerminal);
          instance.AddInParameter(storedProcCommand, "i_AccountId", DbType.Int32, (object) pobjPayment.i_AccountId);
          instance.AddInParameter(storedProcCommand, "i_StatusPayment", DbType.Int32, (object) pobjPayment.i_Status);
          instance.AddInParameter(storedProcCommand, nameof (RequirementDetail), SqlDbType.Structured, (object) RequirementDetail);
          instance.AddInParameter(storedProcCommand, "v_PlateNew", DbType.String, (object) pobRequirementProgramation.v_PlateNew);
          instance.AddInParameter(storedProcCommand, "i_ZoneReference", DbType.Int32, (object) pobRequirementProgramation.i_ZoneReference);
          instance.AddInParameter(storedProcCommand, "i_DistrictReference", DbType.Int32, (object) pobRequirementProgramation.i_DistrictReference);
          instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId"));
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

    public int RequirementRetailOrderInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[uspRequirementRetailOrderInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementTypeId", DbType.Int32, (object) pobjRequirement.i_RequirementTypeId);
          instance.AddInParameter(storedProcCommand, "f_Quantity", DbType.Double, (object) pobjRequirement.f_Quantity);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjRequirement.v_Observations);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pobjRequirement.i_Status);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Ubigeo", DbType.String, (object) pobjRequirement.v_Ubigeo);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdRequester", DbType.String, (object) pobjRequester.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberRequester", DbType.String, (object) pobjRequester.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameRequester", DbType.String, (object) pobjRequester.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameRequester", DbType.String, (object) pobjRequester.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameRequester", DbType.String, (object) pobjRequester.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressRequester", DbType.String, (object) pobjRequester.v_Address);
          instance.AddInParameter(storedProcCommand, "v_UbigeoRequester", DbType.String, (object) pobjRequester.v_AddressLocation);
          instance.AddInParameter(storedProcCommand, "v_PhoneNumberRequester", DbType.String, (object) pobjRequester.v_PhoneNumber);
          instance.AddInParameter(storedProcCommand, "v_Email", DbType.String, (object) pobjRequester.v_Email);
          instance.AddInParameter(storedProcCommand, "i_PersonTypeRequester", DbType.String, (object) pobjRequester.i_PersonTypeId);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBenificiary.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBenificiary.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBenificiary.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBenificiary.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBenificiary.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBenificiary.v_Address);
          instance.AddInParameter(storedProcCommand, "i_PaymentTypeId", DbType.Int32, (object) pobjPayment.i_PaymentTypeId);
          instance.AddInParameter(storedProcCommand, "f_PriceSale", DbType.Double, (object) pobjPayment.f_PriceSale);
          instance.AddInParameter(storedProcCommand, "f_PriceTax", DbType.Double, (object) pobjPayment.f_PriceTax);
          instance.AddInParameter(storedProcCommand, "f_PriceTotal", DbType.Double, (object) pobjPayment.f_PriceTotal);
          instance.AddInParameter(storedProcCommand, "i_BankId", DbType.Int32, (object) pobjPayment.i_BankId);
          instance.AddInParameter(storedProcCommand, "v_BankOperationNumber", DbType.String, (object) pobjPayment.v_BankOperationNumber);
          instance.AddInParameter(storedProcCommand, "v_BankOperationUser", DbType.String, (object) pobjPayment.v_BankOperationUser);
          instance.AddInParameter(storedProcCommand, "v_BankOperationTerminal", DbType.String, (object) pobjPayment.v_BankOperationTerminal);
          instance.AddInParameter(storedProcCommand, "i_AccountId", DbType.Int32, (object) pobjPayment.i_AccountId);
          instance.AddInParameter(storedProcCommand, "i_StatusPayment", DbType.Int32, (object) pobjPayment.i_Status);
          instance.AddInParameter(storedProcCommand, nameof (RequirementDetail), SqlDbType.Structured, (object) RequirementDetail);
          instance.AddInParameter(storedProcCommand, "i_CashRegId", DbType.Int32, (object) pobjRequirement.i_CashRegId);
          instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId"));
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

    public int RequirementMassiveInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester,
      string pstrIpaddress)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[uspRequirementMassiveInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementTypeId", DbType.Int32, (object) pobjRequirement.i_RequirementTypeId);
          instance.AddInParameter(storedProcCommand, "f_Quantity", DbType.Double, (object) pobjRequirement.f_Quantity);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjRequirement.v_Observations);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pobjRequirement.i_Status);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Ubigeo", DbType.String, (object) pobjRequirement.v_Ubigeo);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdRequester", DbType.String, (object) pobjRequester.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberRequester", DbType.String, (object) pobjRequester.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameRequester", DbType.String, (object) pobjRequester.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameRequester", DbType.String, (object) pobjRequester.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameRequester", DbType.String, (object) pobjRequester.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressRequester", DbType.String, (object) pobjRequester.v_Address);
          instance.AddInParameter(storedProcCommand, "v_UbigeoRequester", DbType.String, (object) pobjRequester.v_AddressLocation);
          instance.AddInParameter(storedProcCommand, "v_PhoneNumberRequester", DbType.String, (object) pobjRequester.v_PhoneNumber);
          instance.AddInParameter(storedProcCommand, "v_Email", DbType.String, (object) pobjRequester.v_Email);
          instance.AddInParameter(storedProcCommand, "i_PersonTypeRequester", DbType.String, (object) pobjRequester.i_PersonTypeId);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBenificiary.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBenificiary.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBenificiary.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBenificiary.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBenificiary.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBenificiary.v_Address);
          instance.AddInParameter(storedProcCommand, "v_EmailBeneficiary", DbType.String, (object) pobjBenificiary.v_Email);
          instance.AddInParameter(storedProcCommand, "i_PaymentTypeId", DbType.Int32, (object) pobjPayment.i_PaymentTypeId);
          instance.AddInParameter(storedProcCommand, "f_PriceSale", DbType.Double, (object) pobjPayment.f_PriceSale);
          instance.AddInParameter(storedProcCommand, "f_PriceTax", DbType.Double, (object) pobjPayment.f_PriceTax);
          instance.AddInParameter(storedProcCommand, "f_PriceTotal", DbType.Double, (object) pobjPayment.f_PriceTotal);
          instance.AddInParameter(storedProcCommand, "i_BankId", DbType.Int32, (object) pobjPayment.i_BankId);
          instance.AddInParameter(storedProcCommand, "d_BankOperationDate", DbType.DateTime, (object) pobjPayment.d_BankOperationDate);
          instance.AddInParameter(storedProcCommand, "v_BankOperationNumber", DbType.String, (object) pobjPayment.v_BankOperationNumber);
          instance.AddInParameter(storedProcCommand, "v_BankOperationUser", DbType.String, (object) pobjPayment.v_BankOperationUser);
          instance.AddInParameter(storedProcCommand, "v_BankOperationTerminal", DbType.String, (object) pobjPayment.v_BankOperationTerminal);
          instance.AddInParameter(storedProcCommand, "i_AccountId", DbType.Int32, (object) pobjPayment.i_AccountId);
          instance.AddInParameter(storedProcCommand, "i_StatusPayment", DbType.Int32, (object) pobjPayment.i_Status);
          instance.AddInParameter(storedProcCommand, nameof (RequirementDetail), SqlDbType.Structured, (object) RequirementDetail);
          instance.AddInParameter(storedProcCommand, "v_Ipaddress", DbType.String, (object) pstrIpaddress);
          instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          return Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId"));
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

    public DataTable RequirementInsert(
      SIIV.BE.Requirement pobjRequirement,
      DataTable pdtVehicleRegistrationDetail,
      DataTable pdtRequirementPlate,
      RequirementContributor pobjBeneficiary)
    {
      DataTable dataTable = new DataTable();
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementInsert]"))
      {
        instance.AddInParameter(storedProcCommand, "f_Quantity", DbType.Double, (object) pobjRequirement.f_Quantity);
        instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjRequirement.v_Observations);
        instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
        instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
        instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBeneficiary.i_DocumentTypeId);
        instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBeneficiary.v_DocumentNumber);
        instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_LastName);
        instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_FirstName);
        instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_CompleteName);
        instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBeneficiary.v_Address);
        instance.AddInParameter(storedProcCommand, "VehicleRegistrations", SqlDbType.Structured, (object) pdtVehicleRegistrationDetail);
        instance.AddInParameter(storedProcCommand, "RequirementPlate", SqlDbType.Structured, (object) pdtRequirementPlate);
        instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, 10);
        using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
        {
          dataTable = new DataTable();
          dataTable.Load(reader);
        }
        Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId").ToString());
      }
      return dataTable;
    }

    public int[] RequirementInsertOne(
      SIIV.BE.Requirement pobjRequirement,
      VehicleRegistrationDetail pobjVehicleRegistrationDetail,
      RequirementPlate pobjRequirementPlate,
      RequirementContributor pobjBeneficiary,
      RequirementContributor pobjRequester,
      Payment pobjPayment,
      RequirementProgramation objRequirementProgramation,
      string pstrIpaddress)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32_1;
        int int32_2;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementInsertOne]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementTypeId", DbType.Double, (object) pobjRequirement.i_RequirementTypeId);
          instance.AddInParameter(storedProcCommand, "f_Quantity", DbType.Double, (object) pobjRequirement.f_Quantity);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjRequirement.v_Observations);
          instance.AddInParameter(storedProcCommand, "i_DeliveryPointId", DbType.Int32, (object) pobjRequirementPlate.i_DeliveryPointId);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pobjRequirement.i_ProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Ubigeo", DbType.String, (object) pobjRequirement.v_Ubigeo);
          instance.AddInParameter(storedProcCommand, "i_registrationtypeid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_RegistrationTypeId);
          instance.AddInParameter(storedProcCommand, "i_registrationofficeid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_RegistryOfficeId);
          instance.AddInParameter(storedProcCommand, "i_registryzoneid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_RegistryZoneId);
          instance.AddInParameter(storedProcCommand, "i_vehiclecategoryid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_VehicleCategoryId);
          instance.AddInParameter(storedProcCommand, "i_vehicletypeuseid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_VehicleTypeUseId);
          instance.AddInParameter(storedProcCommand, "i_vehicleclassid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_VehicleClassId);
          instance.AddInParameter(storedProcCommand, "v_platenew", DbType.String, (object) pobjVehicleRegistrationDetail.v_PlateNew);
          instance.AddInParameter(storedProcCommand, "v_plateold", DbType.String, (object) pobjVehicleRegistrationDetail.v_PlateOld);
          instance.AddInParameter(storedProcCommand, "v_titlenumber", DbType.String, (object) pobjVehicleRegistrationDetail.v_TitleNumber);
          instance.AddInParameter(storedProcCommand, "v_brand", DbType.String, (object) pobjVehicleRegistrationDetail.v_Brand);
          instance.AddInParameter(storedProcCommand, "v_model", DbType.String, (object) pobjVehicleRegistrationDetail.v_Model);
          instance.AddInParameter(storedProcCommand, "v_serialnumber", DbType.String, (object) pobjVehicleRegistrationDetail.v_SerialNumber);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameOwner", DbType.String, (object) pobjVehicleRegistrationDetail.v_CompleteNameOwner);
          instance.AddInParameter(storedProcCommand, "i_SpecialPlateTypeid", DbType.Int32, (object) pobjVehicleRegistrationDetail.i_SpecialPlateTypeId);
          instance.AddInParameter(storedProcCommand, "i_requirementplatetypeid", DbType.Int32, (object) pobjRequirementPlate.i_RequirementPlateTypeId);
          instance.AddInParameter(storedProcCommand, "i_ProductId", DbType.Int32, (object) pobjRequirementPlate.i_ProductId);
          instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.Int32, (object) pobjRequirementPlate.i_VehicleId);
          instance.AddInParameter(storedProcCommand, "v_registrationcode", DbType.String, (object) pobjRequirementPlate.v_RegistrationCode);
          instance.AddInParameter(storedProcCommand, "i_databankid", DbType.Int32, (object) pobjRequirementPlate.i_DataBankId);
          instance.AddInParameter(storedProcCommand, "i_processtypeid", DbType.Int32, (object) pobjRequirementPlate.i_ProcessTypeId);
          instance.AddInParameter(storedProcCommand, "i_contigencytypeid", DbType.Int32, (object) pobjRequirementPlate.i_ContingencyTypeId);
          instance.AddInParameter(storedProcCommand, "b_contigencydelivery", DbType.Boolean, (object) pobjRequirementPlate.b_ContingencyDelivery);
          instance.AddInParameter(storedProcCommand, "i_registrationusetypeoldid", DbType.Int32, (object) pobjRequirementPlate.i_RegistrationUseTypeOldId);
          instance.AddInParameter(storedProcCommand, "d_registrationdispatchdate", DbType.DateTime, (object) pobjRequirementPlate.d_RegistrationDispatchDate);
          instance.AddInParameter(storedProcCommand, "i_platetypeid", DbType.Int32, (object) pobjRequirementPlate.i_PlateTypeId);
          instance.AddInParameter(storedProcCommand, "b_pendingconfirmation", DbType.Boolean, (object) pobjRequirementPlate.b_PendingConfirmation);
          instance.AddInParameter(storedProcCommand, "b_migrated", DbType.Boolean, (object) pobjRequirementPlate.b_Migrated);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBeneficiary.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBeneficiary.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBeneficiary.v_Address);
          instance.AddInParameter(storedProcCommand, "v_EmailBeneficiary", DbType.String, (object) pobjBeneficiary.v_Email);
          instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdRequester", DbType.Int32, (object) pobjRequester.i_DocumentTypeId);
          instance.AddInParameter(storedProcCommand, "v_DocumentNumberRequester", DbType.String, (object) pobjRequester.v_DocumentNumber);
          instance.AddInParameter(storedProcCommand, "v_LastNameRequester", DbType.String, (object) pobjRequester.v_LastName);
          instance.AddInParameter(storedProcCommand, "v_FirstNameRequester", DbType.String, (object) pobjRequester.v_FirstName);
          instance.AddInParameter(storedProcCommand, "v_CompleteNameRequester", DbType.String, (object) pobjRequester.v_CompleteName);
          instance.AddInParameter(storedProcCommand, "v_AddressRequester", DbType.String, (object) pobjRequester.v_Address);
          instance.AddInParameter(storedProcCommand, "v_AddressLocationRequester", DbType.String, (object) pobjRequester.v_AddressLocation);
          instance.AddInParameter(storedProcCommand, "v_EmailRequester", DbType.String, (object) pobjRequester.v_Email);
          instance.AddInParameter(storedProcCommand, "v_PhoneNumberRequester", DbType.String, (object) pobjRequester.v_PhoneNumber);
          instance.AddInParameter(storedProcCommand, "i_PersonTypeRequester", DbType.String, (object) pobjRequester.i_PersonTypeId);
          instance.AddInParameter(storedProcCommand, "d_DatePrintingPoliceReport", DbType.DateTime, (object) pobjRequester.d_DatePrintingPoliceReport);
          instance.AddInParameter(storedProcCommand, "v_PoliceReportOrderNumber", DbType.String, (object) pobjRequester.v_PoliceReportOrderNumber);
          instance.AddInParameter(storedProcCommand, "v_PoliceReportKey", DbType.String, (object) pobjRequester.v_PoliceReportKey);
          instance.AddInParameter(storedProcCommand, "i_TypeApplicant", DbType.Int32, (object) pobjRequester.i_TypeApplicant);
          instance.AddInParameter(storedProcCommand, "i_RegistrationReason", DbType.Int32, (object) pobjRequester.i_RegistrationReason);
          instance.AddInParameter(storedProcCommand, "i_PaymentTypeId", DbType.Int32, (object) pobjPayment.i_PaymentTypeId);
          instance.AddInParameter(storedProcCommand, "f_PriceSale", DbType.Double, (object) pobjPayment.f_PriceSale);
          instance.AddInParameter(storedProcCommand, "f_PriceTax", DbType.Double, (object) pobjPayment.f_PriceTax);
          instance.AddInParameter(storedProcCommand, "f_PriceTotal", DbType.Double, (object) pobjPayment.f_PriceTotal);
          instance.AddInParameter(storedProcCommand, "i_BankId", DbType.Int32, (object) pobjPayment.i_BankId);
          instance.AddInParameter(storedProcCommand, "d_BankOperationDate", DbType.DateTime, (object) pobjPayment.d_BankOperationDate);
          instance.AddInParameter(storedProcCommand, "v_BankOperationNumber", DbType.String, (object) pobjPayment.v_BankOperationNumber);
          instance.AddInParameter(storedProcCommand, "v_BankOperationUser", DbType.String, (object) pobjPayment.v_BankOperationUser);
          instance.AddInParameter(storedProcCommand, "v_BankOperationTerminal", DbType.String, (object) pobjPayment.v_BankOperationTerminal);
          instance.AddInParameter(storedProcCommand, "i_AccountId", DbType.Int32, (object) pobjPayment.i_AccountId);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pobjRequirement.i_Status);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirementPlate", DbType.Int32, (object) pobjRequirementPlate.i_Status);
          instance.AddInParameter(storedProcCommand, "i_StatusPayment", DbType.Int32, (object) pobjPayment.i_Status);
          instance.AddInParameter(storedProcCommand, "v_OwnerCompleteName", DbType.String, (object) pobjRequirementPlate.v_OwnerCompleteName);
          instance.AddInParameter(storedProcCommand, "v_OwnerDocumentType", DbType.String, (object) pobjRequirementPlate.v_OwnerDocumentType);
          instance.AddInParameter(storedProcCommand, "v_OwnerDocumentDescription", DbType.String, (object) pobjRequirementPlate.v_OwnerDocumentDescription);
          instance.AddInParameter(storedProcCommand, "v_OwnerDocumentNumber", DbType.String, (object) pobjRequirementPlate.v_OwnerDocumentNumber);
          instance.AddInParameter(storedProcCommand, "i_ZoneReference", DbType.String, (object) objRequirementProgramation.i_ZoneReference);
          instance.AddInParameter(storedProcCommand, "i_DistrictReference", DbType.String, (object) objRequirementProgramation.i_DistrictReference);
          instance.AddInParameter(storedProcCommand, "v_Ipaddress", DbType.String, (object) pstrIpaddress);
          instance.AddOutParameter(storedProcCommand, "i_RequirementId", DbType.Int32, 10);
          instance.AddOutParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, 10);
          instance.ExecuteNonQuery(storedProcCommand);
          int32_1 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementId").ToString());
          int32_2 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_RequirementPlateId").ToString());
        }
        return new int[2]{ int32_1, int32_2 };
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

    public int RequirementUpdate(
      int pintRequirementId,
      int pintRequirementPlateId,
      int pintProofPaymentTypeId,
      RequirementContributor pobjBeneficiary,
      int pintUpdateUser)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      int int32;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementUpdate]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
        instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pintProofPaymentTypeId);
        instance.AddInParameter(storedProcCommand, "i_DocumentTypeIdBeneficiary", DbType.Int32, (object) pobjBeneficiary.i_DocumentTypeId);
        instance.AddInParameter(storedProcCommand, "v_DocumentNumberBeneficiary", DbType.String, (object) pobjBeneficiary.v_DocumentNumber);
        instance.AddInParameter(storedProcCommand, "v_LastNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_LastName);
        instance.AddInParameter(storedProcCommand, "v_FirstNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_FirstName);
        instance.AddInParameter(storedProcCommand, "v_CompleteNameBeneficiary", DbType.String, (object) pobjBeneficiary.v_CompleteName);
        instance.AddInParameter(storedProcCommand, "v_AddressBeneficiary", DbType.String, (object) pobjBeneficiary.v_Address);
        instance.AddInParameter(storedProcCommand, "i_UpdateUserId", DbType.Int32, (object) pintUpdateUser);
        instance.AddOutParameter(storedProcCommand, "i_Success", DbType.Int32, 10);
        instance.ExecuteNonQuery(storedProcCommand);
        int32 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_Success").ToString());
      }
      return int32;
    }

    public int UpdateProofPaperData(
      int pintRequirementId,
      int pintRequirementPlateId,
      int pintProofPaperTypeId,
      string pstrUbigeo,
      int pintBeneficiaryDocumentType,
      string pstrbeneficiaryDocumentNumber,
      string pstrbeneficiaryCompletename,
      string pstrBeneficiaryAddress,
      int pintDeliveryPoint,
      int pintUpdateUser)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      int int32;
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_UpdateProofPaperData"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
        instance.AddInParameter(storedProcCommand, "i_ProofPaperTypeId", DbType.Int32, (object) pintProofPaperTypeId);
        instance.AddInParameter(storedProcCommand, "v_Ubigeo", DbType.String, (object) pstrUbigeo);
        instance.AddInParameter(storedProcCommand, "i_DocumentTypeId", DbType.Int32, (object) pintBeneficiaryDocumentType);
        instance.AddInParameter(storedProcCommand, "v_DocumentNumber", DbType.String, (object) pstrbeneficiaryDocumentNumber);
        instance.AddInParameter(storedProcCommand, "v_CompleteName", DbType.String, (object) pstrbeneficiaryCompletename);
        instance.AddInParameter(storedProcCommand, "v_Address", DbType.String, (object) pstrBeneficiaryAddress);
        instance.AddInParameter(storedProcCommand, "i_DeliveryPointId", DbType.Int32, (object) pintDeliveryPoint);
        instance.AddInParameter(storedProcCommand, "i_UpdateUserId", DbType.Int32, (object) pintUpdateUser);
        instance.AddOutParameter(storedProcCommand, "i_Success", DbType.Int32, 10);
        instance.ExecuteNonQuery(storedProcCommand);
        int32 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_Success").ToString());
      }
      return int32;
    }

    public void RelatedDocumentInsert(RelatedDocument pobjRelatedDocument)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RelatedDocumentInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "v_Description", DbType.String, (object) pobjRelatedDocument.v_Description);
          instance.AddInParameter(storedProcCommand, "v_Petitioner", DbType.String, (object) pobjRelatedDocument.v_Petitioner);
          instance.AddInParameter(storedProcCommand, "v_Motive", DbType.String, (object) pobjRelatedDocument.v_Motive);
          instance.AddInParameter(storedProcCommand, "i_MaximunQuantity", DbType.Int32, (object) pobjRelatedDocument.i_MaximumQuantity);
          instance.AddInParameter(storedProcCommand, "d_CreationDate", DbType.DateTime, (object) pobjRelatedDocument.d_CreationDate);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjRelatedDocument.i_InsertUserId);
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

    public void SpecialRequirementInsert(SpecialRequirement pobjSpecialRequirement)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SpecialRequirementInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pobjSpecialRequirement.i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, "i_RelatedDocumentId", DbType.Int32, (object) pobjSpecialRequirement.i_RelatedDocumentId);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pobjSpecialRequirement.v_Observations);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjSpecialRequirement.i_InsertUserId);
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

    public void IncreaseRelatedDocumentQuantity(int pintRelatedDocumentId, int pintInsertUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_increaseRelatedDocumentQuantity"))
        {
          instance.AddInParameter(storedProcCommand, "i_RelatedDocumentId", DbType.Int32, (object) pintRelatedDocumentId);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUserId);
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

    public void RequirementContributorInsertOne(RequirementContributor pobjRequirementContributor)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementContributorInsertOne]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pobjRequirementContributor.i_RequirementId);
        instance.AddInParameter(storedProcCommand, "i_ContributorTypeId", DbType.Int32, (object) pobjRequirementContributor.i_ContributorTypeId);
        instance.AddInParameter(storedProcCommand, "i_PersontypeId", DbType.Int32, (object) pobjRequirementContributor.i_PersonTypeId);
        instance.AddInParameter(storedProcCommand, "i_DocumentTypeId", DbType.Int32, (object) pobjRequirementContributor.i_DocumentTypeId);
        instance.AddInParameter(storedProcCommand, "v_DocumentNumber", DbType.String, (object) pobjRequirementContributor.v_DocumentNumber);
        instance.AddInParameter(storedProcCommand, "v_LastName", DbType.String, (object) pobjRequirementContributor.v_LastName);
        instance.AddInParameter(storedProcCommand, "v_FirstName", DbType.String, (object) pobjRequirementContributor.v_FirstName);
        instance.AddInParameter(storedProcCommand, "v_CompleteName", DbType.String, (object) pobjRequirementContributor.v_CompleteName);
        instance.AddInParameter(storedProcCommand, "v_Address", DbType.String, (object) pobjRequirementContributor.v_Address);
        instance.AddInParameter(storedProcCommand, "v_AddressLocation", DbType.String, (object) pobjRequirementContributor.v_AddressLocation);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void RequirementContributorsInsert(
      int pintRequirementId,
      int pintRequirementPlateId,
      DataTable pdtContributors)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementContributorInsert]"))
      {
        instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
        instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
        instance.AddInParameter(storedProcCommand, "Owners", SqlDbType.Structured, (object) pdtContributors);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public int ExceptionalRequirementInsert(ExceptionalRequirement pobjExceptionalRequirement)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        int int32;
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ExceptionalRequirementInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ExceptionalRequirementTypeId", DbType.Int32, (object) pobjExceptionalRequirement.i_ExceptionalRequirementTypeId);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pobjExceptionalRequirement.i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, "i_ReferenceRequirementPlateId", DbType.Int32, (object) pobjExceptionalRequirement.i_ReferenceRequirementPlateId);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pobjExceptionalRequirement.i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "i_ResponsableCompanyId", DbType.Int32, (object) pobjExceptionalRequirement.i_ResponsibleCompanyId);
          instance.AddOutParameter(storedProcCommand, "i_ExceptionalRequirementId", DbType.Int32, 10);
          instance.ExecuteNonQuery(storedProcCommand);
          int32 = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "i_ExceptionalRequirementId").ToString());
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

    public int IncidentRequirementInsert(Incident pobjIncident)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_InsertIncident]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pobjIncident.i_RequirementId);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pobjIncident.i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, "d_IncidentDate", DbType.DateTime, (object) pobjIncident.d_IncidentDate);
          instance.AddInParameter(storedProcCommand, "i_Reason", DbType.Int32, (object) pobjIncident.i_Reason);
          instance.ExecuteNonQuery(storedProcCommand);
        }
        return 1;
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

    public void SpecialRequirementApprove(
      int pintSpecialRequiementId,
      int pintApprovalUserId,
      string pstrObservations)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SpecialRequirementApprove]"))
      {
        instance.AddInParameter(storedProcCommand, "i_SpecialRequirement", DbType.Int32, (object) pintSpecialRequiementId);
        instance.AddInParameter(storedProcCommand, "i_ApprovalUserId", DbType.Int32, (object) pintApprovalUserId);
        instance.AddInParameter(storedProcCommand, "v_Observations", DbType.Int32, (object) pstrObservations);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void ExceptionalRequirementApprove(
      int pintExceptionalRequirementId,
      int pintApprovalUserId,
      int pintApprovalStatus,
      string pstrObservations)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ExceptionalRequirementApprove]"))
      {
        instance.AddInParameter(storedProcCommand, "i_ExceptionalRequirementId", DbType.Int32, (object) pintExceptionalRequirementId);
        instance.AddInParameter(storedProcCommand, "i_ApprovalUserId", DbType.Int32, (object) pintApprovalUserId);
        instance.AddInParameter(storedProcCommand, "i_ApprovalStatus", DbType.Int32, (object) pintApprovalStatus);
        instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pstrObservations);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void ChangeUseInsert(
      int pintUseTypeId,
      int pintCategoryId,
      int pintUseTargetId,
      int pintInsertUser)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_ChangeUseInsert]"))
      {
        instance.AddInParameter(storedProcCommand, "i_UseTypeId", DbType.Int32, (object) pintUseTypeId);
        instance.AddInParameter(storedProcCommand, "i_CategoryId", DbType.Int32, (object) pintCategoryId);
        instance.AddInParameter(storedProcCommand, "i_UseTypeTargetId", DbType.Int32, (object) pintUseTargetId);
        instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUser);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void RegisterPaymentCode(int pintRequirementId, int pintInsertUser)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_RegisterPaymentData]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUser);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR PAGAR EL CODIGO DE PAGO");
      }
    }

    public void RetailAnulateSoli(
      int pintRequirementId,
      string pstrObservation,
      int pintInsertUser)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_RetailAnulateSoli]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "v_Observation", DbType.String, (object) pstrObservation);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUser);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR ANULAR LA SOLICITUD");
      }
    }

    public void BlockUnlockPlates(
      int pintBlockId,
      string pstrPlate,
      string pstrActaNumber,
      DateTime dImpositionDate,
      string pstrResolutionNumber,
      string pstrNameWhoRequestedUnlocking,
      DateTime dLiftingDate,
      string pstrObservation,
      string pstrEntity,
      int pintDepartament,
      int pintInsertUser,
      int pintRequirementPlateId,
      int pintRequirementPlateStatus,
      DateTime dStatusDate,
      int pintType)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_BlockUnlockPlates]"))
        {
          instance.AddInParameter(storedProcCommand, "i_BlockId", DbType.String, (object) pintBlockId);
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) pstrPlate);
          instance.AddInParameter(storedProcCommand, "v_ActaNumber", DbType.String, (object) pstrActaNumber);
          instance.AddInParameter(storedProcCommand, "d_ImpositionDate", DbType.DateTime, (object) dImpositionDate);
          instance.AddInParameter(storedProcCommand, "v_ResolutionNumber", DbType.String, (object) pstrResolutionNumber);
          instance.AddInParameter(storedProcCommand, "v_NameWhoRequestedUnlocking", DbType.String, (object) pstrNameWhoRequestedUnlocking);
          instance.AddInParameter(storedProcCommand, "d_LiftingDate", DbType.DateTime, (object) dLiftingDate);
          instance.AddInParameter(storedProcCommand, "v_Observation", DbType.String, (object) pstrObservation);
          instance.AddInParameter(storedProcCommand, "v_Entity", DbType.String, (object) pstrEntity);
          instance.AddInParameter(storedProcCommand, "i_Departament", DbType.Int32, (object) pintDepartament);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUser);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlateId);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateStatus", DbType.Int32, (object) pintRequirementPlateStatus);
          instance.AddInParameter(storedProcCommand, "d_StatusDate", DbType.DateTime, (object) dStatusDate);
          instance.AddInParameter(storedProcCommand, "i_Type", DbType.Int32, (object) pintType);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REGISTRAR LA PLACA");
      }
    }

    public void QuarterlyPriceExceptionRegister(
      int pintTypeVehicleId,
      int pintTypeTramiteId,
      int pintPlateTypeId,
      int pintProductId,
      double pfloPriceSale,
      double pfloPriceTax,
      double pfloPriceCost,
      DateTime pdStartDate,
      DateTime pdEndDate,
      int pintInsertUser)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_QuarterlyPriceExceptionInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_TypeVehicleId", DbType.Int32, (object) pintTypeVehicleId);
          instance.AddInParameter(storedProcCommand, "i_TypeTramiteId", DbType.Int32, (object) pintTypeTramiteId);
          instance.AddInParameter(storedProcCommand, "i_SpecialPlateTypeId", DbType.Int32, (object) pintPlateTypeId);
          instance.AddInParameter(storedProcCommand, "i_ProductId", DbType.Int32, (object) pintProductId);
          instance.AddInParameter(storedProcCommand, "f_PriceSale", DbType.Double, (object) pfloPriceSale);
          instance.AddInParameter(storedProcCommand, "f_PriceTax", DbType.Double, (object) pfloPriceTax);
          instance.AddInParameter(storedProcCommand, "f_PriceCost", DbType.Double, (object) pfloPriceCost);
          instance.AddInParameter(storedProcCommand, "d_StartDate", DbType.DateTime, (object) pdStartDate);
          instance.AddInParameter(storedProcCommand, "d_FinishDate", DbType.DateTime, (object) pdEndDate);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUser);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REGISTRAR LA PLACA");
      }
    }

    public void RegisteCashRegister(
      int i_BoxId,
      int i_LocationId,
      string v_BoxCode,
      string v_Identification,
      double f_Money,
      int i_BoxStatus,
      int i_Status,
      int i_InsertUserId,
      int i_Type)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_RegisterCashRegister]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_BoxId), DbType.Int32, (object) i_BoxId);
          instance.AddInParameter(storedProcCommand, nameof (i_LocationId), DbType.Int32, (object) i_LocationId);
          instance.AddInParameter(storedProcCommand, nameof (v_BoxCode), DbType.String, (object) v_BoxCode);
          instance.AddInParameter(storedProcCommand, nameof (v_Identification), DbType.String, (object) v_Identification);
          instance.AddInParameter(storedProcCommand, nameof (f_Money), DbType.Double, (object) f_Money);
          instance.AddInParameter(storedProcCommand, nameof (i_BoxStatus), DbType.Int32, (object) i_BoxStatus);
          instance.AddInParameter(storedProcCommand, nameof (i_Status), DbType.Int32, (object) i_Status);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_Type), DbType.Int32, (object) i_Type);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR EN LA OPERACION");
      }
    }

    public void RegisteUserQueryIdenti(
      int i_InsertUserId,
      int i_CashRegId,
      int i_Status,
      int i_Type)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_RegisteUserQueryIdenti]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
          instance.AddInParameter(storedProcCommand, nameof (i_Status), DbType.Int32, (object) i_Status);
          instance.AddInParameter(storedProcCommand, nameof (i_Type), DbType.Int32, (object) i_Type);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR EN LA OPERACION");
      }
    }

    public void CashRegConciliation(
      int i_ConId,
      int i_LocationId,
      string v_OperationNumber,
      string v_Observation,
      int i_CashStatus,
      int i_InsertUserId,
      int i_Type)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_CashRegConciliation]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_ConId), DbType.Int32, (object) i_ConId);
          instance.AddInParameter(storedProcCommand, nameof (i_LocationId), DbType.Int32, (object) i_LocationId);
          instance.AddInParameter(storedProcCommand, nameof (v_OperationNumber), DbType.String, (object) v_OperationNumber);
          instance.AddInParameter(storedProcCommand, nameof (v_Observation), DbType.String, (object) v_Observation);
          instance.AddInParameter(storedProcCommand, nameof (i_CashStatus), DbType.Int32, (object) i_CashStatus);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_Type), DbType.Int32, (object) i_Type);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR EN LA OPERACION CAJA");
      }
    }

    public void CashRegisterMovement(
      int i_CashRegId,
      int i_TypeMovementId,
      int i_MotiveMovementId,
      int i_InsertUserId,
      string v_Observacion,
      double f_Amount)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_CashRegisterMovement]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
          instance.AddInParameter(storedProcCommand, nameof (i_TypeMovementId), DbType.Int32, (object) i_TypeMovementId);
          instance.AddInParameter(storedProcCommand, nameof (i_MotiveMovementId), DbType.Int32, (object) i_MotiveMovementId);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, nameof (v_Observacion), DbType.String, (object) v_Observacion);
          instance.AddInParameter(storedProcCommand, nameof (f_Amount), DbType.Double, (object) f_Amount);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR EN LA OPERACION");
      }
    }

    public void ExceptionDeliveryRegister(
      int pintZoneId,
      int pintTypeId,
      int pintDescriptionId,
      int i_InsertUserId,
      string pstringMotive,
      int pintLocationId,
      DateTime dt_DateInicio,
      DateTime dt_DateFin,
      string strvdays)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ExceptionDeliveryRegister]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ZonaId", DbType.Int32, (object) pintZoneId);
          instance.AddInParameter(storedProcCommand, "i_TypeExcepcionId", DbType.Int32, (object) pintTypeId);
          instance.AddInParameter(storedProcCommand, "i_identificatorDeliveryId", DbType.Int32, (object) pintDescriptionId);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          instance.AddInParameter(storedProcCommand, "v_Motive", DbType.String, (object) pstringMotive);
          instance.AddInParameter(storedProcCommand, "i_LocationId", DbType.Int32, (object) pintLocationId);
          instance.AddInParameter(storedProcCommand, "d_StarDate", DbType.DateTime, (object) dt_DateInicio);
          instance.AddInParameter(storedProcCommand, "d_EndDate", DbType.DateTime, (object) dt_DateFin);
          instance.AddInParameter(storedProcCommand, "v_days", DbType.String, (object) strvdays);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR EN EL REGISTRO DE UNA EXCEPCION DELIVERY");
      }
    }

    public int ExceptionDeliveryDelete(int pintExceptionDeliveryId, int i_SystemUserId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_ExceptionDeliveryDelete]"))
        {
          instance.AddInParameter(storedProcCommand, "i_ExceptionDeliveryId", DbType.Int32, (object) pintExceptionDeliveryId);
          instance.AddInParameter(storedProcCommand, "i_UpdateUserId", DbType.Int32, (object) i_SystemUserId);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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

    public void EBillingStatusProcess(
      int pintRequirementId,
      int pintStatusProcess,
      double pdoublePayTotal)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_EBillingStatusProcess]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_StatusProcess", DbType.Int32, (object) pintStatusProcess);
          instance.AddInParameter(storedProcCommand, "f_PayTotal", DbType.Double, (object) pdoublePayTotal);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REALIZAR EL PROCESO");
      }
    }

    public void EBillingCashProcess(int pintRequirementId, int pintStatus, int pintSystemUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_EBillingCashProcess]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "i_SystemUserId", DbType.Int32, (object) pintSystemUserId);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REALIZAR EL PROCESO");
      }
    }

    public DataTable GetEmailRetailAnulate(int pintRequirementId, int pintInsertUserId)
    {
      try
      {
        DataTable emailRetailAnulate = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_EBillingEmailAnulate]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUserId);
          storedProcCommand.CommandTimeout = 0;
          using (IDataReader reader = instance.ExecuteReader(storedProcCommand))
          {
            emailRetailAnulate = new DataTable();
            emailRetailAnulate.Load(reader);
          }
        }
        return emailRetailAnulate;
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

    public DataTable EBillingExportMasiveUrl(string v_EbillingID)
    {
      try
      {
        DataTable dataTable = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_EBillingUrlExport]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_EbillingID), DbType.String, (object) v_EbillingID);
          storedProcCommand.CommandTimeout = 0;
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

    public void RegisterEBillingData(
      int i_CashRegId,
      int pintRequirementId,
      int pintProofPaymentTypeId,
      int pintStatus,
      int pintInsertUserId)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Payment].[usp_RegisterEBillingData]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_CashRegId), DbType.Int32, (object) i_CashRegId);
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_ProofPaymentTypeId", DbType.Int32, (object) pintProofPaymentTypeId);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) pintInsertUserId);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REALIZAR EL PROCESO");
      }
    }

    public void RequirementEBillingStatus(int pintRequirementId, int pintStatus)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementEBillingStatus]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_Status", DbType.Int32, (object) pintStatus);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR REALIZAR EL PROCESO");
      }
    }

    public void ChangeUseUpdate(
      int pintChangeUseId,
      int pintUseTypeId,
      int pintCategoryId,
      int pintUseTargetId,
      int pintUpdateUser)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Common].[usp_ChangeUseUpdate]"))
      {
        instance.AddInParameter(storedProcCommand, "i_ChangeUseId", DbType.Int32, (object) pintChangeUseId);
        instance.AddInParameter(storedProcCommand, "i_UseTypeId", DbType.Int32, (object) pintUseTypeId);
        instance.AddInParameter(storedProcCommand, "i_CategoryId", DbType.Int32, (object) pintCategoryId);
        instance.AddInParameter(storedProcCommand, "i_UseTypeTargetId", DbType.Int32, (object) pintUseTargetId);
        instance.AddInParameter(storedProcCommand, "i_UpdateUserId", DbType.Int32, (object) pintUpdateUser);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void SunarpDataParalyze(int pintVehicleId)
    {
      SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
      using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SunarpDataParalyze]"))
      {
        instance.AddInParameter(storedProcCommand, "i_VehicleId", DbType.Int32, (object) pintVehicleId);
        instance.ExecuteNonQuery(storedProcCommand);
      }
    }

    public void UpdateExceptionalRequirmentStatus(
      int pintRequirementPlate,
      int pintExceptionalRequirement,
      int pintStatusExcetional,
      int pintStatusRequirement,
      string psrtObservations,
      int pintApprovalUserid)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_UpdateExceptionalRequirmentStatus"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlate);
          instance.AddInParameter(storedProcCommand, "i_ExceptionalRequirementId", DbType.Int32, (object) pintExceptionalRequirement);
          instance.AddInParameter(storedProcCommand, "i_StatusExceptional", DbType.Int32, (object) pintStatusExcetional);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pintStatusRequirement);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) psrtObservations);
          instance.AddInParameter(storedProcCommand, "i_ApprovalUserId", DbType.String, (object) pintApprovalUserid);
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

    public void UpdateSpecialRequirmentStatus(
      int pintRequirementPlate,
      int pintSpecialRequirement,
      int pintStatusSpecial,
      int pintStatusRequirement,
      string pstrObservations,
      int pintApprovalUserid)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("Requirement.usp_UpdateSpecialRequirmentStatus"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) pintRequirementPlate);
          instance.AddInParameter(storedProcCommand, "i_SpecialRequirementId", DbType.Int32, (object) pintSpecialRequirement);
          instance.AddInParameter(storedProcCommand, "i_StatusSpecial", DbType.Int32, (object) pintStatusSpecial);
          instance.AddInParameter(storedProcCommand, "i_StatusRequirement", DbType.Int32, (object) pintStatusRequirement);
          instance.AddInParameter(storedProcCommand, "v_Observations", DbType.String, (object) pstrObservations);
          instance.AddInParameter(storedProcCommand, "i_ApprovalUserId", DbType.String, (object) pintApprovalUserid);
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

    public int RequirementProgramationUpdate(
      RequirementProgramation ObjRequirementProgramation,
      out int i_StatusProgramationOut)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementProgramationUpdate]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.Int32, (object) ObjRequirementProgramation.i_RequirementPlateId);
          instance.AddInParameter(storedProcCommand, "i_BlockTimeReference", DbType.String, (object) ObjRequirementProgramation.i_BlockTimeReference);
          instance.AddInParameter(storedProcCommand, "d_RegistrationDate", DbType.DateTime, (object) ObjRequirementProgramation.d_RegistrationDate);
          instance.AddInParameter(storedProcCommand, "i_UpdateUserId", DbType.Int32, (object) ObjRequirementProgramation.i_UpdateUserId);
          instance.AddOutParameter(storedProcCommand, "@i_StatusProgramationOut", DbType.Int32, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          i_StatusProgramationOut = Convert.ToInt32(instance.GetParameterValue(storedProcCommand, "@i_StatusProgramationOut"));
        }
        return i_StatusProgramationOut;
      }
      catch (SqlException ex)
      {
        throw new Exception(ex.Message);
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR GRABAR LA PROGRAMACIÓN.");
      }
    }

    public string EBillingUrl(
      int pintRequirementId,
      int pintRequirementPlateId,
      out string v_Url,
      out string v_Estado)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementEBillingUrl]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementId", DbType.Int32, (object) pintRequirementId);
          instance.AddInParameter(storedProcCommand, "i_RequirementPlateId", DbType.String, (object) pintRequirementPlateId);
          instance.AddOutParameter(storedProcCommand, "@v_UrlOut", DbType.String, int.MaxValue);
          instance.AddOutParameter(storedProcCommand, "@v_EstadoOut", DbType.String, int.MaxValue);
          instance.ExecuteNonQuery(storedProcCommand);
          v_Url = Convert.ToString(instance.GetParameterValue(storedProcCommand, "@v_UrlOut"));
          v_Estado = Convert.ToString(instance.GetParameterValue(storedProcCommand, "@v_EstadoOut"));
        }
        return v_Url;
      }
      catch (SqlException ex)
      {
        throw new Exception(ex.Message);
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR GRABAR LA PROGRAMACIÓN.");
      }
    }

    public void AsignationProgramationInsert(
      RequirementAsignationProgramation ObjRequirementAsignationProgramation)
    {
      try
      {
        SqlDatabase instance = (SqlDatabase) EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_AsignationProgramationInsert]"))
        {
          instance.AddInParameter(storedProcCommand, "i_RequirementProgramationId", DbType.Int32, (object) ObjRequirementAsignationProgramation.i_RequirementProgramationId);
          instance.AddInParameter(storedProcCommand, "i_CourierReference", DbType.String, (object) ObjRequirementAsignationProgramation.i_CourierReference);
          instance.AddInParameter(storedProcCommand, "d_AsignationDate", DbType.DateTime, (object) ObjRequirementAsignationProgramation.d_AsignationDate);
          instance.AddInParameter(storedProcCommand, "v_ObservationA", DbType.String, (object) ObjRequirementAsignationProgramation.v_ObservationA);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) ObjRequirementAsignationProgramation.i_InsertUserId);
          instance.ExecuteNonQuery(storedProcCommand);
        }
      }
      catch (SqlException ex)
      {
        throw new HandledException(ex);
      }
    }

    public int InsertDeliverySystemParameter(
      int i_GroupId,
      int i_ParameterId,
      string v_Value,
      string v_Description,
      string v_OldValue,
      int i_InsertUserId)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_InsertDeliverySystemParameter]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_GroupId), DbType.Int32, (object) i_GroupId);
          instance.AddInParameter(storedProcCommand, nameof (i_ParameterId), DbType.Int32, (object) i_ParameterId);
          instance.AddInParameter(storedProcCommand, nameof (v_Value), DbType.String, (object) v_Value);
          instance.AddInParameter(storedProcCommand, nameof (v_Description), DbType.String, (object) v_Description);
          instance.AddInParameter(storedProcCommand, nameof (v_OldValue), DbType.String, (object) v_OldValue);
          instance.AddInParameter(storedProcCommand, nameof (i_InsertUserId), DbType.Int32, (object) i_InsertUserId);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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

    public int UpdateDeleteDeliverySystemParameter(
      int i_GroupId,
      int i_ParameterId,
      string v_Description,
      string v_Value,
      string v_OldValue,
      int i_UpdateUserId,
      int i_Reference,
      string v_Option)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateDeleteDeliverySystemParameter2]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_GroupId), DbType.Int32, (object) i_GroupId);
          instance.AddInParameter(storedProcCommand, nameof (i_ParameterId), DbType.Int32, (object) i_ParameterId);
          instance.AddInParameter(storedProcCommand, nameof (v_Description), DbType.String, (object) v_Description);
          instance.AddInParameter(storedProcCommand, nameof (v_Value), DbType.String, (object) v_Value);
          instance.AddInParameter(storedProcCommand, nameof (v_OldValue), DbType.String, (object) v_OldValue);
          instance.AddInParameter(storedProcCommand, nameof (i_UpdateUserId), DbType.Int32, (object) i_UpdateUserId);
          instance.AddInParameter(storedProcCommand, nameof (i_Reference), DbType.Int32, (object) i_Reference);
          instance.AddInParameter(storedProcCommand, nameof (v_Option), DbType.String, (object) v_Option);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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

    public int UpdateDeleteDeliveryRequirement(
      int i_statusDistrict,
      int i_requirementPlate,
      int i_District,
      string v_Adrress,
      string v_Email,
      string v_Telephone,
      int i_insertUser,
      int i_RequirementProgramation,
      string v_PlateNew)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_UpdateDeliveryRequirement]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (i_statusDistrict), DbType.Int32, (object) i_statusDistrict);
          instance.AddInParameter(storedProcCommand, nameof (i_requirementPlate), DbType.Int32, (object) i_requirementPlate);
          instance.AddInParameter(storedProcCommand, nameof (i_District), DbType.Int32, (object) i_District);
          instance.AddInParameter(storedProcCommand, nameof (v_Adrress), DbType.String, (object) v_Adrress);
          instance.AddInParameter(storedProcCommand, nameof (v_Email), DbType.String, (object) v_Email);
          instance.AddInParameter(storedProcCommand, nameof (v_Telephone), DbType.String, (object) v_Telephone);
          instance.AddInParameter(storedProcCommand, nameof (i_insertUser), DbType.Int32, (object) i_insertUser);
          instance.AddInParameter(storedProcCommand, nameof (i_RequirementProgramation), DbType.Int32, (object) i_RequirementProgramation);
          instance.AddInParameter(storedProcCommand, nameof (v_PlateNew), DbType.String, (object) v_PlateNew);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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

    public DataTable SearchMRE(
      string v_Plate,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_flag,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      try
      {
        DataTable dataTable = (DataTable) null;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_SearchMRE]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (v_Plate), DbType.String, (object) v_Plate);
          instance.AddInParameter(storedProcCommand, "i_status", DbType.Int32, (object) i_Status);
          instance.AddInParameter(storedProcCommand, "d_DateIni", DbType.DateTime, (object) d_StartDate);
          instance.AddInParameter(storedProcCommand, "d_DateFin", DbType.DateTime, (object) d_EndDate);
          instance.AddInParameter(storedProcCommand, "i_Flag", DbType.Int32, (object) i_flag);
          instance.AddInParameter(storedProcCommand, nameof (startRowIndex), DbType.Int32, (object) startRowIndex);
          instance.AddInParameter(storedProcCommand, nameof (maxRows), DbType.Int32, (object) maxRows);
          instance.AddOutParameter(storedProcCommand, "totalRows", DbType.Int32, int.MaxValue);
          storedProcCommand.CommandTimeout = 0;
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

    public int CreateUpdatePlateMRE(string v_plate, int i_SystemUserId, string v_Option)
    {
      try
      {
        int updatePlateMre = 0;
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_RequirementInsertMRE]"))
        {
          instance.AddInParameter(storedProcCommand, "v_Plate", DbType.String, (object) v_plate);
          instance.AddInParameter(storedProcCommand, "i_InsertUserId", DbType.Int32, (object) i_SystemUserId);
          instance.AddInParameter(storedProcCommand, nameof (v_Option), DbType.String, (object) v_Option);
          updatePlateMre = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
        }
        return updatePlateMre;
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

    public int EnabledDisabledProgramation(
      DateTime dt_Date,
      int i_BlockSheduleId,
      int i_ZoneId,
      string v_Option)
    {
      int num = 0;
      try
      {
        Database instance = EnterpriseLibraryContainer.Current.GetInstance<Database>(this.cadenaConexion);
        using (DbCommand storedProcCommand = instance.GetStoredProcCommand("[Requirement].[usp_EnabledDisabledBlockScheduleZone]"))
        {
          instance.AddInParameter(storedProcCommand, nameof (dt_Date), DbType.DateTime, (object) dt_Date);
          instance.AddInParameter(storedProcCommand, nameof (i_BlockSheduleId), DbType.Int32, (object) i_BlockSheduleId);
          instance.AddInParameter(storedProcCommand, nameof (i_ZoneId), DbType.String, (object) i_ZoneId);
          instance.AddInParameter(storedProcCommand, nameof (v_Option), DbType.String, (object) v_Option);
          num = Convert.ToInt32(instance.ExecuteNonQuery(storedProcCommand));
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
  }
}
