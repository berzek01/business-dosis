// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.BL.RequirementManagementBL
// Assembly: SIIV.Requirement.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE9A2D44-C606-41EA-8453-9569E774C3D1
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.Requirement.BL.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Requirement.DA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

#nullable disable
namespace SIIV.Requirement.BL
{
  public class RequirementManagementBL
  {
    private RequirementManagementDA objRequirementManagementDA = new RequirementManagementDA();

    public int RequirementDeliveryFinishUpdate(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary)
    {
      return this.objRequirementManagementDA.RequirementDeliveryFinishUpdate(pobjRequirement, pobjBenificiary);
    }

    public int RequirementDeliveryOrderInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester,
      RequirementProgramation pobRequirementProgramation)
    {
      return this.objRequirementManagementDA.RequirementDeliveryOrderInsert(pobjRequirement, pobjBenificiary, RequirementDetail, pobjPayment, pobjRequester, pobRequirementProgramation);
    }

    public int RequirementRetailOrderInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester)
    {
      return this.objRequirementManagementDA.RequirementRetailOrderInsert(pobjRequirement, pobjBenificiary, RequirementDetail, pobjPayment, pobjRequester);
    }

    public int RequirementMassiveInsert(
      SIIV.BE.Requirement pobjRequirement,
      RequirementContributor pobjBenificiary,
      DataTable RequirementDetail,
      Payment pobjPayment,
      RequirementContributor pobjRequester,
      string pstrIpaddress = "")
    {
      return this.objRequirementManagementDA.RequirementMassiveInsert(pobjRequirement, pobjBenificiary, RequirementDetail, pobjPayment, pobjRequester, pstrIpaddress);
    }

    public DataTable RequirementInsert(
      SIIV.BE.Requirement pobjRequirement,
      DataTable pdtVehicleRegistrationDetail,
      DataTable pdtRequirementPlate,
      RequirementContributor pobjBeneficiary)
    {
      return this.objRequirementManagementDA.RequirementInsert(pobjRequirement, pdtVehicleRegistrationDetail, pdtRequirementPlate, pobjBeneficiary);
    }

    public void RelatedDocumentInsert(RelatedDocument pobjRelatedDocument)
    {
      this.objRequirementManagementDA.RelatedDocumentInsert(pobjRelatedDocument);
    }

    public void SpecialRequirementInsert(SpecialRequirement pobjSpecialRequirement)
    {
      this.objRequirementManagementDA.SpecialRequirementInsert(pobjSpecialRequirement);
    }

    public void RequirementContributorInsertOne(RequirementContributor pobjRequirementContributor)
    {
      this.objRequirementManagementDA.RequirementContributorInsertOne(pobjRequirementContributor);
    }

    public int ExceptionalRequirementInsert(ExceptionalRequirement pobjExceptionalRequirement)
    {
      return this.objRequirementManagementDA.ExceptionalRequirementInsert(pobjExceptionalRequirement);
    }

    public int IncidentRequirementInsert(Incident pobjIncident)
    {
      return this.objRequirementManagementDA.IncidentRequirementInsert(pobjIncident);
    }

    public void SpecialRequirementApprove(
      int pintSpecialRequiementId,
      int pintApprovalUserId,
      string pstrObservations)
    {
      this.objRequirementManagementDA.SpecialRequirementApprove(pintSpecialRequiementId, pintApprovalUserId, pstrObservations);
    }

    public int[] RequirementInsertOne(
      SIIV.BE.Requirement pobjRequirement,
      VehicleRegistrationDetail pobjVehicleRegistrationDetail,
      RequirementPlate pobjRequirementPlate,
      RequirementContributor pobjBeneficiary,
      RequirementContributor pobjRequester,
      Payment pobjPayment,
      RequirementProgramation objRequirementProgramation,
      string pstrIpaddress = "")
    {
      return this.objRequirementManagementDA.RequirementInsertOne(pobjRequirement, pobjVehicleRegistrationDetail, pobjRequirementPlate, pobjBeneficiary, pobjRequester, pobjPayment, objRequirementProgramation, pstrIpaddress);
    }

    public void ExceptionalRequirementApprove(
      int pintExceptionalRequirementId,
      int pintApprovalUserId,
      int pintApprovalStatus,
      string pstrObservations)
    {
      this.objRequirementManagementDA.ExceptionalRequirementApprove(pintExceptionalRequirementId, pintApprovalUserId, pintApprovalStatus, pstrObservations);
    }

    public void RequirementContributorsInsert(
      int pintRequirementId,
      int pintRequirementPlateId,
      DataTable pdtContributors)
    {
      this.objRequirementManagementDA.RequirementContributorsInsert(pintRequirementId, pintRequirementPlateId, pdtContributors);
    }

    public void ChangeUseInsert(
      int pintUseTypeId,
      int pintCategoryId,
      int pintUseTargetId,
      int pintInsertUser)
    {
      this.objRequirementManagementDA.ChangeUseInsert(pintUseTypeId, pintCategoryId, pintUseTargetId, pintInsertUser);
    }

    public bool RegisterPaymentCode(int pintRequirementId, int pintInsertUser)
    {
      try
      {
        this.objRequirementManagementDA.RegisterPaymentCode(pintRequirementId, pintInsertUser);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool BlockUnlockPlates(
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
        this.objRequirementManagementDA.BlockUnlockPlates(pintBlockId, pstrPlate, pstrActaNumber, dImpositionDate, pstrResolutionNumber, pstrNameWhoRequestedUnlocking, dLiftingDate, pstrObservation, pstrEntity, pintDepartament, pintInsertUser, pintRequirementPlateId, pintRequirementPlateStatus, dStatusDate, pintType);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool QuarterlyPriceExceptionRegister(
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
        this.objRequirementManagementDA.QuarterlyPriceExceptionRegister(pintTypeVehicleId, pintTypeTramiteId, pintPlateTypeId, pintProductId, pfloPriceSale, pfloPriceTax, pfloPriceCost, pdStartDate, pdEndDate, pintInsertUser);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool RetailAnulateSoli(
      int pintRequirementId,
      string pstrObservation,
      int pintInsertUser)
    {
      try
      {
        this.objRequirementManagementDA.RetailAnulateSoli(pintRequirementId, pstrObservation, pintInsertUser);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool RegisteCashRegister(
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
        this.objRequirementManagementDA.RegisteCashRegister(i_BoxId, i_LocationId, v_BoxCode, v_Identification, f_Money, i_BoxStatus, i_Status, i_InsertUserId, i_Type);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool RegisteUserQueryIdenti(
      int i_InsertUserId,
      int i_CashRegId,
      int i_Status,
      int i_Type)
    {
      try
      {
        this.objRequirementManagementDA.RegisteUserQueryIdenti(i_InsertUserId, i_CashRegId, i_Status, i_Type);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool CashRegConciliation(
      int i_ConId,
      int i_LocationId,
      string v_OperationNumber,
      string v_Observation,
      int i_Cashtatus,
      int i_InsertUserId,
      int i_Type)
    {
      try
      {
        this.objRequirementManagementDA.CashRegConciliation(i_ConId, i_LocationId, v_OperationNumber, v_Observation, i_Cashtatus, i_InsertUserId, i_Type);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool CashRegisterMovement(
      int i_CashRegId,
      int i_TypeMovementId,
      int i_MotiveMovementId,
      int i_InsertUserId,
      string v_Observacion,
      double f_Amount)
    {
      try
      {
        this.objRequirementManagementDA.CashRegisterMovement(i_CashRegId, i_TypeMovementId, i_MotiveMovementId, i_InsertUserId, v_Observacion, f_Amount);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool ExceptionDeliveryRegister(
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
        this.objRequirementManagementDA.ExceptionDeliveryRegister(pintZoneId, pintTypeId, pintDescriptionId, i_InsertUserId, pstringMotive, pintLocationId, dt_DateInicio, dt_DateFin, strvdays);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public int ExceptionDeliveryDelete(int pintExceptionDeliveryId, int i_SystemUserId)
    {
      try
      {
        return this.objRequirementManagementDA.ExceptionDeliveryDelete(pintExceptionDeliveryId, i_SystemUserId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool EBillingStatusProcess(
      int pintRequirementId,
      int pintStatusProcess,
      double pdoublePayTotal)
    {
      try
      {
        this.objRequirementManagementDA.EBillingStatusProcess(pintRequirementId, pintStatusProcess, pdoublePayTotal);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool EBillingCashProcess(int pintRequirementId, int pintStatus, int pintSystemUserId = 0)
    {
      try
      {
        this.objRequirementManagementDA.EBillingCashProcess(pintRequirementId, pintStatus, pintSystemUserId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public string EBillingUrl(
      int pintRequirementId,
      int pintRequirementPlateId,
      out string v_Url,
      out string v_Estado)
    {
      try
      {
        return this.objRequirementManagementDA.EBillingUrl(pintRequirementId, pintRequirementPlateId, out v_Url, out v_Estado);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable GetEmailRetailAnulate(int pintRequirementId, int pintInsertUserId)
    {
      return this.objRequirementManagementDA.GetEmailRetailAnulate(pintRequirementId, pintInsertUserId);
    }

    public DataTable EBillingExportMasiveUrl(string v_EbillingID)
    {
      return this.objRequirementManagementDA.EBillingExportMasiveUrl(v_EbillingID);
    }

    public bool RegisterEBillingData(
      int i_CashRegId,
      int pintRequirementId,
      int pintProofPaymentTypeId,
      int pintStatus,
      int pintInsertUserId)
    {
      try
      {
        this.objRequirementManagementDA.RegisterEBillingData(i_CashRegId, pintRequirementId, pintProofPaymentTypeId, pintStatus, pintInsertUserId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool RequirementEBillingStatus(int pintRequirementId, int pintStatus)
    {
      try
      {
        this.objRequirementManagementDA.RequirementEBillingStatus(pintRequirementId, pintStatus);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public void ChangeUseUpdate(
      int pintChangeUseId,
      int pintUseTypeId,
      int pintCategoryId,
      int pintUseTargetId,
      int pintUpdateUser)
    {
      this.objRequirementManagementDA.ChangeUseUpdate(pintChangeUseId, pintUseTypeId, pintCategoryId, pintUseTargetId, pintUpdateUser);
    }

    public int RequirementUpdate(
      int pintRequirementId,
      int pintRequirementPlateId,
      int pintProofPaymentTypeId,
      RequirementContributor pobjBeneficiary,
      int pintUpdateUser)
    {
      return this.objRequirementManagementDA.RequirementUpdate(pintRequirementId, pintRequirementPlateId, pintProofPaymentTypeId, pobjBeneficiary, pintUpdateUser);
    }

    public void IncreaseRelatedDocumentQuantity(int pintRelatedDocumentId, int pintInsertUserId)
    {
      this.objRequirementManagementDA.IncreaseRelatedDocumentQuantity(pintRelatedDocumentId, pintInsertUserId);
    }

    public void SunarpDataParalyze(int pintVehicleId)
    {
      this.objRequirementManagementDA.SunarpDataParalyze(pintVehicleId);
    }

    public void UpdateExceptionalRequirmentStatus(
      int pintRequirementPlate,
      int pintExceptionalRequirement,
      int pintStatusExcetional,
      int pintStatusRequirement,
      string psrtObservations,
      int pintApprovalUserid)
    {
      this.objRequirementManagementDA.UpdateExceptionalRequirmentStatus(pintRequirementPlate, pintExceptionalRequirement, pintStatusExcetional, pintStatusRequirement, psrtObservations, pintApprovalUserid);
    }

    public void UpdateSpecialRequirmentStatus(
      int pintRequirementPlate,
      int pintSpecialRequirement,
      int pintStatusSpecial,
      int pintStatusRequirement,
      string psrtObservations,
      int pintApprovalUserid)
    {
      this.objRequirementManagementDA.UpdateSpecialRequirmentStatus(pintRequirementPlate, pintSpecialRequirement, pintStatusSpecial, pintStatusRequirement, psrtObservations, pintApprovalUserid);
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
      return this.objRequirementManagementDA.UpdateProofPaperData(pintRequirementId, pintRequirementPlateId, pintProofPaperTypeId, pstrUbigeo, pintBeneficiaryDocumentType, pstrbeneficiaryDocumentNumber, pstrbeneficiaryCompletename, pstrBeneficiaryAddress, pintDeliveryPoint, pintUpdateUser);
    }

    public int RequirementProgramationUpdate(
      RequirementProgramation ObjRequirementProgramation,
      out int i_StatusProgramationOut)
    {
      try
      {
        return this.objRequirementManagementDA.RequirementProgramationUpdate(ObjRequirementProgramation, out i_StatusProgramationOut);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool AsignationProgramationInsert(
      List<RequirementAsignationProgramation> dtListDeliveryPlate)
    {
      try
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          foreach (RequirementAsignationProgramation ObjRequirementAsignationProgramation in dtListDeliveryPlate)
            new RequirementManagementBL().objRequirementManagementDA.AsignationProgramationInsert(ObjRequirementAsignationProgramation);
          transactionScope.Complete();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public int InsertDeliverySystemParameter(
      int i_GroupId,
      int i_ParameterId,
      string v_Value,
      string v_Description,
      string v_OldValue,
      int i_InsertUserId)
    {
      try
      {
        return this.objRequirementManagementDA.InsertDeliverySystemParameter(i_GroupId, i_ParameterId, v_Value, v_Description, v_OldValue, i_InsertUserId);
      }
      catch (Exception ex)
      {
        throw ex;
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
      try
      {
        return this.objRequirementManagementDA.UpdateDeleteDeliverySystemParameter(i_GroupId, i_ParameterId, v_Description, v_Value, v_OldValue, i_UpdateUserId, i_Reference, v_Option);
      }
      catch (Exception ex)
      {
        throw ex;
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
      try
      {
        return this.objRequirementManagementDA.UpdateDeleteDeliveryRequirement(i_statusDistrict, i_requirementPlate, i_District, v_Adrress, v_Email, v_Telephone, i_insertUser, i_RequirementProgramation, v_PlateNew);
      }
      catch (Exception ex)
      {
        throw ex;
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
      return this.objRequirementManagementDA.SearchMRE(v_Plate, i_Status, d_StartDate, d_EndDate, i_flag, startRowIndex, maxRows, out pinttotalRows);
    }

    public int CreateUpdatePlateMRE(string v_plate, int i_SystemUserId, string v_Option)
    {
      try
      {
        int updatePlateMre = 0;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          updatePlateMre = this.objRequirementManagementDA.CreateUpdatePlateMRE(v_plate, i_SystemUserId, v_Option);
          transactionScope.Complete();
        }
        return updatePlateMre;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int EnabledDisabledProgramation(
      DateTime dt_Date,
      int i_BlockSheduleId,
      int i_ZoneId,
      string v_Option)
    {
      try
      {
        return this.objRequirementManagementDA.EnabledDisabledProgramation(dt_Date, i_BlockSheduleId, i_ZoneId, v_Option);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
