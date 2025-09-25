// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.BL.RequirementQueriesBL
// Assembly: SIIV.Requirement.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6CDA977C-9E81-4503-829A-9DEDC12E0F36
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.Requirement.BL.dll

using SIIV.Requirement.DA;
using System;
using System.Collections;
using System.Data;

#nullable disable
namespace SIIV.Requirement.BL
{
  public class RequirementQueriesBL
  {
    private RequirementQueriesDA objRequirementQueriesDA = new RequirementQueriesDA();

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
      return this.objRequirementQueriesDA.UniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, pintCategoryId, pintProcessTypeId, pintStatus, pstrSerial, pstrPaymentCode, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out pinttotalRows);
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
      return this.objRequirementQueriesDA.EBillingUniversalQueryRead(pintProfType, pintStartdate, pintFinishdate, pstrProfDocument, pstrClient, pstrCliDocument, pintRequirement, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out pinttotalRows);
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
      return this.objRequirementQueriesDA.EBillingQueryExport(pstringProfType, pintStartdate, pintFinishdate, strClient, strDesde, strHasta, strSerieDocument, startRowIndex, maxRows, out pinttotalRows);
    }

    public DataTable BlockPlatesUniversalQueryRead(
      int pintBlockId,
      string pstrPlate,
      int pintStatus,
      int pintUserId,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      return this.objRequirementQueriesDA.BlockPlatesUniversalQueryRead(pintBlockId, pstrPlate, pintStatus, pintUserId, startRowIndex, maxRows, out pinttotalRows);
    }

    public DataTable PermissionBlockPlates(int pintUserId)
    {
      return this.objRequirementQueriesDA.PermissionBlockPlates(pintUserId);
    }

    public DataTable GestPlatesRequirementStatus(string pstrPlate)
    {
      return this.objRequirementQueriesDA.GestPlatesRequirementStatus(pstrPlate);
    }

    public DataTable CashRegisterUniversalQueryRead(
      int pintWarehouseId,
      string pstrDescription,
      int pintLocationId,
      int pintBoxStatus,
      int pintStatus,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      return this.objRequirementQueriesDA.CashRegisterUniversalQueryRead(pintWarehouseId, pstrDescription, pintLocationId, pintBoxStatus, pintStatus, startRowIndex, maxRows, out pinttotalRows);
    }

    public DataTable CashRegConciliationQuery(
      int pintStartdate,
      int pintFinishdate,
      int i_CashRegId,
      string pstrDescription,
      int i_LocationId,
      int i_CashRegSStatus,
      int i_Status,
      int startRowIndex,
      int maxRows,
      out int pinttotalRows)
    {
      return this.objRequirementQueriesDA.CashRegConciliationQuery(pintStartdate, pintFinishdate, i_CashRegId, pstrDescription, i_LocationId, i_CashRegSStatus, i_Status, startRowIndex, maxRows, out pinttotalRows);
    }

    public DataTable GetDescriptionExceptionsByType(int pinTypeExceptionsId, int pinZoneId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetDescriptionExceptionsByType(pinTypeExceptionsId, pinZoneId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetRequiredStageByIdProcess(int pintProcessTypeId)
    {
      return this.objRequirementQueriesDA.GetRequiredStageByIdProcess(pintProcessTypeId);
    }

    public int ValidateExistRequirementByPlateTitle(string pstrPlateNumber, string pstrTitle)
    {
      return this.objRequirementQueriesDA.ValidateExistRequirementByPlateTitle(pstrPlateNumber, pstrTitle);
    }

    public DataTable SunarpDataRead(string pstrPlateNumber, string pstrTitle, bool irol = false)
    {
      return this.objRequirementQueriesDA.SunarpDataRead(pstrPlateNumber, pstrTitle, irol);
    }

    public DataTable SunarpDataReadbyId(int pintVehicleId, bool irol = false)
    {
      return this.objRequirementQueriesDA.SunarpDataReadbyId(pintVehicleId, irol);
    }

    public DataTable SunarpDataRead3rd(string pstrPlateNumber, bool irol = false)
    {
      return this.objRequirementQueriesDA.SunarpDataRead3rd(pstrPlateNumber, irol);
    }

    public DataTable SunarpDataReadChangeUse(string pstrPlateNumber, bool irol = false)
    {
      return this.objRequirementQueriesDA.SunarpDataReadChangeUse(pstrPlateNumber, irol);
    }

    public DataTable SunarpDataReadChangeUseRectification(string pstrPlateNumber, int pintVehicleId)
    {
      return this.objRequirementQueriesDA.SunarpDataReadChangeUseRectification(pstrPlateNumber, pintVehicleId);
    }

    public int ValidateExistSunarpByPlateTitle(string pstrPlateNumber, string pstrTitle)
    {
      return this.objRequirementQueriesDA.ValidateExistSunarpByPlateTitle(pstrPlateNumber, pstrTitle);
    }

    public int ValidateExistSunarpByPlateVinSerie(string pstrPlateNumber, string pstrVinSerie)
    {
      return this.objRequirementQueriesDA.ValidateExistSunarpByPlateVinSerie(pstrPlateNumber, pstrVinSerie);
    }

    public DataTable GetOwnersByIdSunarp(int pintVehicleId)
    {
      return this.objRequirementQueriesDA.GetOwnersByIdSunarp(pintVehicleId);
    }

    public DataTable ValidateDeliveryZone(int pintDeliveryPointId)
    {
      return this.objRequirementQueriesDA.ValidateExistDeliveryPoint(pintDeliveryPointId);
    }

    public string DeliveryDireccion(int pintDeliveryPointId)
    {
      return this.objRequirementQueriesDA.DeliveryDireccion(pintDeliveryPointId);
    }

    public DataTable GetProofPaymenType() => this.objRequirementQueriesDA.GetProofPaymenType();

    public DataTable GetActionProceesEBilling(int iRequirementID)
    {
      return this.objRequirementQueriesDA.GetActionProceesEBilling(iRequirementID);
    }

    public DataTable GetActionProceesCashRegUser(int i_InsertUserId)
    {
      return this.objRequirementQueriesDA.GetActionProceesCashRegUser(i_InsertUserId);
    }

    public DataTable GetCashRegisterList(
      int i_CashRegId,
      int i_LocationId,
      string v_BoxCode,
      string v_BoxIdentification,
      int i_InsertUserId,
      int i_Type)
    {
      return this.objRequirementQueriesDA.GetCashRegisterList(i_CashRegId, i_LocationId, v_BoxCode, v_BoxIdentification, i_InsertUserId, i_Type);
    }

    public DataTable GetPersonType() => this.objRequirementQueriesDA.GetPersonType();

    public DataTable GetDocumentType() => this.objRequirementQueriesDA.GetDocumentType();

    public DataTable GetProofPaymenTypeSpecial()
    {
      return this.objRequirementQueriesDA.GetProofPaymenTypeSpecial();
    }

    public DataTable GetLocation(
      string pstrLocationid,
      string pstrDescription,
      string pstrCompanyId)
    {
      return this.objRequirementQueriesDA.GetLocation(pstrLocationid, pstrDescription, pstrCompanyId);
    }

    public DataTable GetLocationRequirement()
    {
      return this.objRequirementQueriesDA.GetLocationRequirement();
    }

    public DataTable GetLocationRequirement(int i_SystemUserId)
    {
      return this.objRequirementQueriesDA.GetLocationRequirement(i_SystemUserId);
    }

    public DataTable GetBank() => this.objRequirementQueriesDA.GetBank();

    public DataTable GetChangeUse(int pintUseTypeId, int pintCategoryId)
    {
      return this.objRequirementQueriesDA.GetChangeUse(pintUseTypeId, pintCategoryId);
    }

    public int Verify3rdPlate(string pstrPlateNumber)
    {
      return this.objRequirementQueriesDA.Verify3rdPlate(pstrPlateNumber);
    }

    public int VerifyProcess3rdPlate(string pstrPlateNumber)
    {
      return this.objRequirementQueriesDA.VerifyProcess3rdPlate(pstrPlateNumber);
    }

    public DataTable ValidateProductPrice3rd(string pstrPlateNumber, bool irol = false)
    {
      return this.objRequirementQueriesDA.ValidateProductPrice3rd(pstrPlateNumber, irol);
    }

    public string GetCorrelativeTitle() => this.objRequirementQueriesDA.GetCorrelativeTitle();

    public DataTable GetProductbyTypeUse(int pintUseTypeId)
    {
      return this.objRequirementQueriesDA.GetProductbyTypeUse(pintUseTypeId);
    }

    public DataTable GetPriceServiceDelivery(int i_ProductId, int pintCorrespondenceType)
    {
      return this.objRequirementQueriesDA.GetPriceServiceDelivery(i_ProductId, pintCorrespondenceType);
    }

    public DataTable ChangeUseRead(
      string pstrUseType,
      string pstrCategory,
      string pstrUseTargetType)
    {
      return this.objRequirementQueriesDA.ChangeUseRead(pstrUseType, pstrCategory, pstrUseTargetType);
    }

    public DataTable GetUseType() => this.objRequirementQueriesDA.GetUseType();

    public DataTable GetCategory() => this.objRequirementQueriesDA.GetCategory();

    public DataTable GenerateCUR(int pintRequirementId)
    {
      return this.objRequirementQueriesDA.GenerateCUR(pintRequirementId);
    }

    public DataTable GenerateCURByIds(int pintRequirementId, int pintRequirementPlateId)
    {
      return this.objRequirementQueriesDA.GenerateCURByIds(pintRequirementId, pintRequirementPlateId);
    }

    public DataTable GenerateDeliveryCUR(int pintRequirementId)
    {
      return this.objRequirementQueriesDA.GenerateDeliveryCUR(pintRequirementId);
    }

    public DataTable GenerateCashRegister(int i_CashRegId, int i_InsertUserId, int i_Type)
    {
      return this.objRequirementQueriesDA.GenerateCashRegister(i_CashRegId, i_InsertUserId, i_Type);
    }

    public DataTable GetSpecialPlateClass(int pintVehicleRegistration, int pintSpecialPlate)
    {
      return this.objRequirementQueriesDA.GetSpecialPlateClass(pintVehicleRegistration, pintSpecialPlate);
    }

    public DataTable GetSpecialPlate(int pintVehicleRegistration)
    {
      return this.objRequirementQueriesDA.GetSpecialPlate(pintVehicleRegistration);
    }

    public DataTable GetSpecialRequirementType(
      int pintVehicleRegistration,
      int pintSpecialPlateType,
      int pintVehicleClass)
    {
      return this.objRequirementQueriesDA.GetSpecialRequirementType(pintVehicleRegistration, pintSpecialPlateType, pintVehicleClass);
    }

    public DataTable ValidateExistMRE(string pstrPlateNumber, int pintProcessTypeId)
    {
      return this.objRequirementQueriesDA.ValidateExistMRE(pstrPlateNumber, pintProcessTypeId);
    }

    public DataTable GetProductByRegistrationClass(
      int pintVehicleRegistration,
      int pintVehicleClass)
    {
      return this.objRequirementQueriesDA.GetProductByRegistrationClass(pintVehicleRegistration, pintVehicleClass);
    }

    public DataTable RelatedDocumentRead(
      int i_RoleConfigId,
      int startRowIndex,
      int maxRows,
      out int pintTotalRows)
    {
      return this.objRequirementQueriesDA.RelatedDocumentRead(i_RoleConfigId, startRowIndex, maxRows, out pintTotalRows);
    }

    public int DeleteOffice(int i_RelatedDocumentId, int i_SystemUserId)
    {
      return this.objRequirementQueriesDA.DeleteOffice(i_RelatedDocumentId, i_SystemUserId);
    }

    public string CodeOfficeGenerate() => this.objRequirementQueriesDA.CodeOfficeGenerate();

    public DataTable GetRelatedDocument(int i_RoleConfigId)
    {
      return this.objRequirementQueriesDA.GetRelatedDocument(i_RoleConfigId);
    }

    public DataTable GetRelatedDocumentbyId(int id)
    {
      return this.objRequirementQueriesDA.GetRelatedDocumentbyId(id);
    }

    public DataTable GetProcess() => this.objRequirementQueriesDA.GetProcess();

    public DataTable GetApprovedRequirement(
      int intType,
      int intStartDate,
      int intFinishDate,
      int intAppoved,
      int intProcess,
      string strInsertUser,
      string strOficio)
    {
      return this.objRequirementQueriesDA.GetApprovedRequirement(intType, intStartDate, intFinishDate, intAppoved, intProcess, strInsertUser, strOficio);
    }

    public DataTable GetGroup(string strGroup) => this.objRequirementQueriesDA.GetGroup(strGroup);

    public DataTable GetRequirementPlateStatus()
    {
      return this.objRequirementQueriesDA.GetRequirementPlateStatus();
    }

    public DataTable GetEbillingProofPaymentType()
    {
      return this.objRequirementQueriesDA.GetEbillingProofPaymentType();
    }

    public DataTable GetRequirementDatabyPlate(string pstrPlate)
    {
      return this.objRequirementQueriesDA.GetRequirementDatabyPlate(pstrPlate);
    }

    public DataTable GetRequirementDatabyRetail(int i_RequirementId)
    {
      return this.objRequirementQueriesDA.GetRequirementDatabyRetail(i_RequirementId);
    }

    public DataTable GetRequirementDatabyPaymentCode(string v_PaymentCode)
    {
      return this.objRequirementQueriesDA.GetRequirementDatabyPaymentCode(v_PaymentCode);
    }

    public DataTable GetPaymentDatabyPaymentCode(string v_PaymentCode)
    {
      return this.objRequirementQueriesDA.GetPaymentDatabyPaymentCode(v_PaymentCode);
    }

    public DataTable GetRequirementPlateDatabyId(int pintRequirement)
    {
      return this.objRequirementQueriesDA.GetRequirementPlateDatabyId(pintRequirement);
    }

    public DataTable GetSunarpDatabyId(int pintVehicleId, int pintRequirementPlate)
    {
      return this.objRequirementQueriesDA.GetSunarpDatabyId(pintVehicleId, pintRequirementPlate);
    }

    public DataTable GetPaymentDatabyRequirement(int pintRequirement, int pintRequirementPlate)
    {
      return this.objRequirementQueriesDA.GetPaymentDatabyRequirement(pintRequirement, pintRequirementPlate);
    }

    public DataTable GetProofPaperDatabyRequirement(int pintRequirementPlate)
    {
      return this.objRequirementQueriesDA.GetProofPaperDatabyRequirement(pintRequirementPlate);
    }

    public DataTable GetDistrict(int i_ParameterId, int iVehicleClassId)
    {
      return this.objRequirementQueriesDA.GetDistrict(i_ParameterId, iVehicleClassId);
    }

    public DataTable GetDistrictByDeliveryPoint(
      int pintDeliveryPoint,
      int i_VehicleClasification,
      int i_Cobertura = 0)
    {
      return this.objRequirementQueriesDA.GetDistrictByDeliveryPoint(pintDeliveryPoint, i_VehicleClasification, i_Cobertura);
    }

    public int GetProductCorrespondence(int pintProductid, int pintCorrespondenceType)
    {
      return this.objRequirementQueriesDA.GetProductCorrespondence(pintProductid, pintCorrespondenceType);
    }

    public DataTable GetAgreggateProducts(
      int pintCorrespondenceTypeId,
      int pintVehicleClasificationId)
    {
      return this.objRequirementQueriesDA.GetAgreggateProducts(pintCorrespondenceTypeId, pintVehicleClasificationId);
    }

    public string GetRequisitebyRequirement(int pintRequirementId, int pintProcessId)
    {
      return this.objRequirementQueriesDA.GetRequisitebyRequirement(pintRequirementId, pintProcessId);
    }

    public int GetSystemUserPublic(int pintSystemUserId, int pintApplicationId)
    {
      return this.objRequirementQueriesDA.GetSystemUserPublic(pintSystemUserId, pintApplicationId);
    }

    public string GetSystemUserExtendedAction(int pintSystemUserId, int pintApplicationId)
    {
      return this.objRequirementQueriesDA.GetSystemUserExtendedAction(pintSystemUserId, pintApplicationId);
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
      this.objRequirementQueriesDA.GetRequirementStatus(pstrPlaetNumber, pintRequirementId, out opstrStatusDescription, out opstrObservations, out opstrSerialNumber, out opstrBrand, out opstrModel, out opstrOwnerCompleteName, out opstrPlatePrevious, out opstrPlateNew, out opstrDuplicate, out opstrDescription, out opstrStatus, out opstrDeliveryPoint, out opstrStartDate, out opstrInsertDate);
    }

    public string GetPaymentCode(int pintRequirementPlate)
    {
      return this.objRequirementQueriesDA.GetPaymentCode(pintRequirementPlate);
    }

    public DataTable GetRequirementContributor(int pintContributorType, int pintRequirement)
    {
      return this.objRequirementQueriesDA.GetRequirementContributor(pintContributorType, pintRequirement);
    }

    public int GetRequirementIdByRequirementPlateId(int pintRequirementPlateId)
    {
      return this.objRequirementQueriesDA.GetRequirementIdByRequirementPlateId(pintRequirementPlateId);
    }

    public int GetRequirementPlateIdByPlateNumber(string pstrPlateNumberId)
    {
      return this.objRequirementQueriesDA.GetRequirementPlateIdByPlateNumber(pstrPlateNumberId);
    }

    public DataTable GetRequirementProgramation(string v_Plate)
    {
      try
      {
        return this.objRequirementQueriesDA.GetRequirementProgramation(v_Plate);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetDeliveryCouriers(int iLocation)
    {
      try
      {
        return this.objRequirementQueriesDA.GetDeliveryCouriers(iLocation);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetRequirementSchedule(string v_Plate)
    {
      try
      {
        return this.objRequirementQueriesDA.GetRequirementSchedule(v_Plate);
      }
      catch (Exception ex)
      {
        throw ex;
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
        this.objRequirementQueriesDA.RequirementGetPrice(pintProductId, out f_PriceCost, out f_PriceTax, out f_PriceSale);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable DeliveryListGetByIdZone(int i_ZoneReference, int i_RequirementPlateId)
    {
      try
      {
        return this.objRequirementQueriesDA.DeliveryListGetByIdZone(i_ZoneReference, i_RequirementPlateId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable DeliveryListGetByIdZoneOne(int i_RequirementPlateId)
    {
      try
      {
        return this.objRequirementQueriesDA.DeliveryListGetByIdZoneOne(i_RequirementPlateId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable DeliverySendEmail(int i_RequirementPlateId)
    {
      try
      {
        return this.objRequirementQueriesDA.DeliverySendEmail(i_RequirementPlateId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int RequirementProgramationUpdate(
      int i_RequirementPlateId,
      int i_Motive,
      string v_ObservationA,
      int i_SystemUserId)
    {
      return this.objRequirementQueriesDA.RequirementProgramationUpdate(i_RequirementPlateId, i_Motive, v_ObservationA, i_SystemUserId);
    }

    public DataTable SearchDeliveryPlatePaymentReport(
      int i_SystemUserId,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      return this.objRequirementQueriesDA.SearchDeliveryPlatePaymentReport(i_SystemUserId, d_StartDate, d_EndDate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
    }

    public DataTable SearchStatusDelivery(
      int i_SystemUserId,
      int i_RequirementPlateId,
      string v_Plate,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_Flag,
      int pintStartRowIndex,
      int pintMaxRows,
      out int pintTotalRows)
    {
      return this.objRequirementQueriesDA.SearchStatusDelivery(i_SystemUserId, i_RequirementPlateId, v_Plate, i_Status, d_StartDate, d_EndDate, i_Flag, pintStartRowIndex, pintMaxRows, out pintTotalRows);
    }

    public DataTable SearchStatusDeliveryReport()
    {
      return this.objRequirementQueriesDA.SearchStatusDeliveryReport();
    }

    public DataTable GetRequirementProgrmationDelivery(
      int i_RequirementPlateId,
      int pintSystemUserId)
    {
      return this.objRequirementQueriesDA.GetRequirementProgrmationDelivery(i_RequirementPlateId, pintSystemUserId);
    }

    public DataTable SearchStatusDeliveryDetail(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.SearchStatusDeliveryDetail(i_RequirementPlateId);
    }

    public DataTable GetGroupsDelivery(int i_GroupId, int i_ParameterId, string v_option)
    {
      return this.objRequirementQueriesDA.GetGroupsDelivery(i_GroupId, i_ParameterId, v_option);
    }

    public DataTable GetVReferenceDelivery(int i_GroupId, int i_ParameterId)
    {
      return this.objRequirementQueriesDA.GetVReferenceDelivery(i_GroupId, i_ParameterId);
    }

    public DataTable ProcessSunarpToProcessType(string v_PlateMotive)
    {
      try
      {
        return this.objRequirementQueriesDA.ProcessSunarpToProcessType(v_PlateMotive);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string GetDescriptionProduct(int pintProductid)
    {
      return this.objRequirementQueriesDA.GetDescriptionProduct(pintProductid);
    }

    public int GetCantDeliveryServices(int i_SystemUserId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetCantDeliveryServices(i_SystemUserId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetBlockShedule(int i_BlockSheduleId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetBlockShedule(i_BlockSheduleId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetBlockSheduleZone(string v_ReferenceId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetBlockSheduleZone(v_ReferenceId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable SearchBlockSheduleZone(DateTime dt_Date, int i_BlockSheduleId, int i_ZoneId)
    {
      try
      {
        return this.objRequirementQueriesDA.SearchBlockSheduleZone(dt_Date, i_BlockSheduleId, i_ZoneId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable ZoneByLocationGet(string pintLocationId)
    {
      try
      {
        return this.objRequirementQueriesDA.ZoneByLocationGet(pintLocationId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable SearchExceptionsByLocation(
      int pintLocationId,
      int intStartRowIndex,
      int intMaxRows,
      out int intTotalRows)
    {
      try
      {
        return this.objRequirementQueriesDA.SearchExceptionsByLocation(pintLocationId, intStartRowIndex, intMaxRows, out intTotalRows);
      }
      catch (Exception ex)
      {
        throw ex;
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
        return this.objRequirementQueriesDA.GetCantDeliveryRequirement(v_Plate, iZonaId, iDistrictId, ischedule, vDate);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable RequirementGetByPaymentCode(string v_PaymentCode, int i_RequirementId = 0)
    {
      try
      {
        return this.objRequirementQueriesDA.RequirementGetByPaymentCode(v_PaymentCode, i_RequirementId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateRequirementBoundVisa(int i_RequirementId)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateRequirementBoundVisa(i_RequirementId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateRequirementBoundCash(int i_RequirementId)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateRequirementBoundCash(i_RequirementId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateDeletePaymentRequirement(string v_PaymentCode, int i_SystemUserId)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateDeletePaymentRequirement(v_PaymentCode, i_SystemUserId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateETicketVisa(string v_PaymentCode, string v_Eticket, string v_NumOrden)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateETicketVisa(v_PaymentCode, v_Eticket, v_NumOrden);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int AccreditPayment(string Cod, int Type, string IdTrans)
    {
      try
      {
        return this.objRequirementQueriesDA.AccreditPayment(Cod, Type, IdTrans);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int EBillingPayment()
    {
      try
      {
        return this.objRequirementQueriesDA.EBillingPayment();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetEBillingIssued()
    {
      try
      {
        return this.objRequirementQueriesDA.GetEBillingIssued();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetEBillingInternal()
    {
      try
      {
        return this.objRequirementQueriesDA.GetEBillingInternal();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable ETicketVisaGet(string v_Eticket)
    {
      try
      {
        return this.objRequirementQueriesDA.ETicketVisaGet(v_Eticket);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ElectronicBillGet(int i_requirementPlateId)
    {
      try
      {
        return this.objRequirementQueriesDA.ElectronicBillGet(i_requirementPlateId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetLogServiceVisaByStatusPayment()
    {
      return this.objRequirementQueriesDA.GetLogServiceVisaByStatusPayment();
    }

    public int ProcessAccreditPaymentVisa()
    {
      return this.objRequirementQueriesDA.ProcessAccreditPaymentVisa();
    }

    public int ProcessAccreditPaymentBanks()
    {
      return this.objRequirementQueriesDA.ProcessAccreditPaymentBanks();
    }

    public int CancelPayment(string Cod, int Type)
    {
      try
      {
        return this.objRequirementQueriesDA.CancelPayment(Cod, Type);
      }
      catch (Exception ex)
      {
        throw ex;
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
      return this.objRequirementQueriesDA.RetailGetByProduct(i_ProductId, i_ProductTypeId, i_ProductUseId, i_CategoryId, v_Description, startRowIndex, maxRows, out totalRows);
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
      return this.objRequirementQueriesDA.DeliveryUniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pintRequirementId, pstrOwnerName, pintStatus, pstrPaymentCode, pintUserId, startRowIndex, maxRows, out pinttotalRows);
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
      return this.objRequirementQueriesDA.GetManufacturedPlates(i_SystemUserId, i_DeliveryPointId, i_ProcessTypeId, pintStartdate, pintFinishdate, v_platenumber, v_OwnerCompleteName, pintStartRowIndex, pintMaxRows, out pintTotalRows);
    }

    public DataTable GetDataManufacturedByPlate(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.GetDataManufacturedByPlate(i_RequirementPlateId);
    }

    public int RequirementCallInsert(ArrayList arrFilter)
    {
      return this.objRequirementQueriesDA.RequirementCallInsert(arrFilter);
    }

    public DataTable GetDetailbyProductDelivery(int i_ProductId)
    {
      return this.objRequirementQueriesDA.GetDetailbyProductDelivery(i_ProductId);
    }

    public int StockMovemenDeliverytInsertTransfer(string CodOrden)
    {
      return this.objRequirementQueriesDA.StockMovemenDeliverytInsertTransfer(CodOrden);
    }

    public int UpdateRequirementCancel(int i_RequirementId)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateRequirementCancel(i_RequirementId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int ValidateExistRequirementDelivery(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.ValidateExistRequirementDelivery(i_RequirementPlateId);
    }

    public DataTable GetRequirementTrazability()
    {
      return this.objRequirementQueriesDA.GetRequirementTrazability();
    }

    public DataTable GetPreviousStatusRequirement(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.GetPreviousStatusRequirement(i_RequirementPlateId);
    }

    public DataTable GetDataTrazabilityId(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.GetDataTrazabilityId(i_RequirementPlateId);
    }

    public DataTable GetDataTrazabilityMasiveId(int i_RequirementId)
    {
      return this.objRequirementQueriesDA.GetDataTrazabilityMasiveId(i_RequirementId);
    }

    public int UpdateConfirmationSendEmail(
      int i_EmailNotificationId,
      bool b_ResultSend,
      string v_Observation)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateConfirmationSendEmail(i_EmailNotificationId, b_ResultSend, v_Observation);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateConfirmationAlertVehicleSendEmail(
      int i_AlertVehicleEmailId,
      int i_AlertVehicleId,
      int i_ResultSend,
      string v_Observation)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateConfirmationAlertVehicleSendEmail(i_AlertVehicleEmailId, i_AlertVehicleId, i_ResultSend, v_Observation);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateConfirmTransferDeliverySendEmail()
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateConfirmTransferDeliverySendEmail();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateConfirmationReadEmail(int i_TypeNotificationId, int i_NotificationId)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdateConfirmationReadEmail(i_TypeNotificationId, i_NotificationId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetFabricatedPlateAlert()
    {
      try
      {
        return this.objRequirementQueriesDA.GetFabricatedPlateAlert();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetAlertVehicleEmail()
    {
      try
      {
        return this.objRequirementQueriesDA.GetAlertVehicleEmail();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetGestorRequirement(int i_GestorRequirementId, int i_TypeId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetGestorRequirement(i_GestorRequirementId, i_TypeId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int ProcessGestorRequirement(int i_GestorRequirementId)
    {
      return this.objRequirementQueriesDA.ProcessGestorRequirement(i_GestorRequirementId);
    }

    public int GenerateProcessProductive(int i_RequirementId)
    {
      return this.objRequirementQueriesDA.GenerateProcessProductive(i_RequirementId);
    }

    public int UpdatePhoneNumber(
      int i_RequirementPlateId,
      int i_SystemUserId,
      string v_PhoneNumberNew)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdatePhoneNumber(i_RequirementPlateId, i_SystemUserId, v_PhoneNumberNew);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetConciliacionPlate(string v_PlateNew, int i_ActionId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetConciliacionPlate(v_PlateNew, i_ActionId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetDeliveryProduct()
    {
      try
      {
        return this.objRequirementQueriesDA.GetDeliveryProduct();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string GetDeliveryProductOfDistrict(int i_DistrictId, int i_VehicleClasification)
    {
      try
      {
        return this.objRequirementQueriesDA.GetDeliveryProductOfDistrict(i_DistrictId, i_VehicleClasification);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable SearchDeliveryData(int i_RequirementPlateId)
    {
      return this.objRequirementQueriesDA.SearchDeliveryData(i_RequirementPlateId);
    }

    public int InsertServiceVisaLog(ArrayList arrFilter)
    {
      try
      {
        return this.objRequirementQueriesDA.InsertServiceVisaLog(arrFilter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int InsertPaymentLog(ArrayList arrFilter)
    {
      try
      {
        return this.objRequirementQueriesDA.InsertPaymentLog(arrFilter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdatePaymentLog(ArrayList arrFilter)
    {
      try
      {
        return this.objRequirementQueriesDA.UpdatePaymentLog(arrFilter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetPaymentLogById(int i_PaymentVisaLogId)
    {
      try
      {
        return this.objRequirementQueriesDA.GetPaymentLogById(i_PaymentVisaLogId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetServiceVisaLogByPaymentCode(string s_PaymentCode)
    {
      try
      {
        return this.objRequirementQueriesDA.GetServiceVisaLogByPaymentCode(s_PaymentCode);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string GetPlatebyRequirementPlateId(int iRequirementPlateId)
    {
      return this.objRequirementQueriesDA.GetPlatebyRequirementPlateId(iRequirementPlateId);
    }

    public DataTable ConciliarRequirementPlatebyId(int iRequirementPlateId)
    {
      return this.objRequirementQueriesDA.ConciliarRequirementPlatebyId(iRequirementPlateId);
    }

    public DataTable GeneratePaymentCodebyRequirementId(int iRequirementId)
    {
      return this.objRequirementQueriesDA.GeneratePaymentCodebyRequirementId(iRequirementId);
    }

    public void RegisterConciliacionAudit(string vDetails, int iuserId, string vResult)
    {
      this.objRequirementQueriesDA.RegisterConciliacionAudit(vDetails, iuserId, vResult);
    }
  }
}
