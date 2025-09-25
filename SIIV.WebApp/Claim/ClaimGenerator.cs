// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claim.ClaimGenerator
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web;

#nullable disable
namespace SIIV.WebApp.Claim
{
  public class ClaimGenerator
  {
    private static HttpResponse objResponse;

    public ClaimGenerator(HttpResponse pobjResponse) => ClaimGenerator.objResponse = pobjResponse;

    public ClaimGenerator()
    {
    }

    public string GenerateClaim_DataNoFound_PopUp(
      string pstrPlateNumber,
      string pstrTitleNumber,
      string pstrRequesterFirstName,
      string pstrRequesterLastName,
      string pstrRequesterDocumentTypeId,
      string pstrRequesterDocumentNumber,
      string pstrRequesterEmail,
      string pstrRequesterTelephone)
    {
      string str1 = pstrPlateNumber + "|" + pstrTitleNumber;
      string str2 = pstrRequesterFirstName + "|" + pstrRequesterLastName + "|" + pstrRequesterDocumentTypeId + "|" + pstrRequesterDocumentNumber + "|" + pstrRequesterEmail + "|" + pstrRequesterTelephone;
      return "../Claims/ClaimDataNoFound.aspx?rv=" + HttpUtility.UrlEncode(str1) + "&rq=" + HttpUtility.UrlEncode(str2);
    }

    public string GenerateClaim_DataNoAgree_PopUp(
      string pstrPlateNumber,
      string pstrTitleNumber,
      string pstrVehicleModel,
      string pstrVehicleBrand,
      string pstrVehicleSerial,
      string pstrOwnerName,
      string pstrOwnerDocument,
      string strOwnerDocumentType,
      string OwnerDocumentItem,
      string pstrPlateOld,
      string pstrRequesterFirstName,
      string pstrRequesterLastName,
      string pstrRequesterDocumentTypeId,
      string pstrRequesterDocumentNumber,
      string pstrRequesterEmail,
      string pstrRequesterTelephone,
      string pstrCategoryId,
      string pstrCategory,
      string pstrCategoryGroup)
    {
      string str1 = pstrPlateNumber + "|" + pstrTitleNumber;
      string str2 = pstrVehicleModel + "|" + pstrVehicleBrand + "|" + pstrVehicleSerial + "|" + pstrOwnerName + "|" + pstrOwnerDocument + "|" + pstrPlateOld + "|" + strOwnerDocumentType + "|" + OwnerDocumentItem;
      string str3 = pstrRequesterFirstName + "|" + pstrRequesterLastName + "|" + pstrRequesterDocumentTypeId + "|" + pstrRequesterDocumentNumber + "|" + pstrRequesterEmail + "|" + pstrRequesterTelephone;
      string str4 = pstrCategoryId;
      string str5 = pstrCategory;
      return "../Claims/ClaimDataNoAgree.aspx?" + ("rv=" + HttpUtility.UrlEncode(str1) + "&rq=" + HttpUtility.UrlEncode(str3) + "&od=" + HttpUtility.UrlEncode(str2) + "&lv=" + HttpUtility.UrlEncode(str4) + "&l=" + HttpUtility.UrlEncode(str5) + "&lg=" + HttpUtility.UrlEncode(pstrCategoryGroup));
    }

    public string GenerateClaim_BatchNoAgree(
      string pstrDispatchNumber,
      string pstrBatchNumber,
      string pstrmotivoid)
    {
      string empty = string.Empty;
      return "../../Claims/ClaimBatchNoAgree.aspx?bn=" + HttpUtility.UrlEncode(pstrDispatchNumber + "|" + pstrBatchNumber + "|" + pstrmotivoid);
    }

    public string GenerateClaim_BatchNoAgree(
      string pstrRequirementPlateId,
      string pstrOwnerName,
      string pstrPlateNumber,
      string pstrDispatchNumber,
      string pstrBatchNumber,
      string pstrProcessTypeId,
      string pstrProductCurrent,
      string pstrProductNew,
      string pstrRequesterFirstName,
      string pstrRequesterLastName,
      string pstrRequesterDocumentTypeId,
      string pstrRequesterDocumentNumber,
      string pstrRequesterEmail,
      string pstrRequesterTelephone)
    {
      string empty = string.Empty;
      string str1 = pstrRequirementPlateId + "|" + pstrOwnerName + "|" + pstrPlateNumber + "|" + pstrDispatchNumber + "|" + pstrBatchNumber + "|" + pstrProcessTypeId + "|" + pstrProductCurrent + "|" + pstrProductNew;
      string str2 = pstrRequesterFirstName + "|" + pstrRequesterLastName + "|" + pstrRequesterDocumentTypeId + "|" + pstrRequesterDocumentNumber + "|" + pstrRequesterEmail + "|" + pstrRequesterTelephone;
      return "../../Claims/ClaimBatchNoAgree.aspx?bn=" + HttpUtility.UrlEncode(str1) + "&rq=" + HttpUtility.UrlEncode(str2);
    }

    public string GenerateClaim_ProductNoAgree(
      string pstrRequirementPlateId,
      string pstrPlateNumber,
      string pstrProcessTypeId,
      string pstrProductCurrent,
      string pstrProductNew,
      string pstrRequesterFirstName,
      string pstrRequesterLastName,
      string pstrRequesterDocumentTypeId,
      string pstrRequesterDocumentNumber,
      string pstrRequesterEmail,
      string pstrRequesterTelephone,
      int intReadValues)
    {
      string empty = string.Empty;
      string str1 = pstrRequirementPlateId + "|" + pstrPlateNumber + "|" + pstrProcessTypeId + "|" + pstrProductCurrent + "|" + pstrProductNew;
      string str2 = pstrRequesterFirstName + "|" + pstrRequesterLastName + "|" + pstrRequesterDocumentTypeId + "|" + pstrRequesterDocumentNumber + "|" + pstrRequesterEmail + "|" + pstrRequesterTelephone;
      return "../../Claims/ClaimProductNoAgree.aspx?pn=" + HttpUtility.UrlEncode(str1) + "&rq=" + HttpUtility.UrlEncode(str2) + "&ro=" + HttpUtility.UrlEncode(intReadValues.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }
  }
}
