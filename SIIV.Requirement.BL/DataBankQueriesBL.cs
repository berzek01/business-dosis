// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.BL.DataBankQueriesBL
// Assembly: SIIV.Requirement.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6CDA977C-9E81-4503-829A-9DEDC12E0F36
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.Requirement.BL.dll

using SIIV.Requirement.DA;
using System.Data;

#nullable disable
namespace SIIV.Requirement.BL
{
  public class DataBankQueriesBL
  {
    private DataBankQueriesDA objDataBankQueriesDA = new DataBankQueriesDA();

    public DataTable PaymentAcreditation(
      int pintBankId,
      string pstrPaymentDate,
      string pstrTerminal,
      string pstrUserCode,
      string pstrProduct,
      int pintPaymentType)
    {
      return this.objDataBankQueriesDA.PaymentAcreditation(pintBankId, pstrPaymentDate, pstrTerminal, pstrUserCode, pstrProduct, pintPaymentType);
    }

    public DataTable PaymentAcreditationbyOperation(
      int pintBankId,
      string pstrPaymentDate,
      string pstrOperation,
      string psrtUserCode,
      string pstrProduct)
    {
      return this.objDataBankQueriesDA.PaymentAcreditationbyOperation(pintBankId, pstrPaymentDate, pstrOperation, psrtUserCode, pstrProduct);
    }

    public DataTable getDataBankbyId(int pintBankId)
    {
      return this.objDataBankQueriesDA.getDataBankbyId(pintBankId);
    }

    public int getDataBankConciliate(int pintDataBankid)
    {
      return this.objDataBankQueriesDA.getDataBanKConciliate(pintDataBankid);
    }
  }
}
