// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.BL.DataBankManagementBL
// Assembly: SIIV.Requirement.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE9A2D44-C606-41EA-8453-9569E774C3D1
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.Requirement.BL.dll

using SIIV.Requirement.DA;

#nullable disable
namespace SIIV.Requirement.BL
{
  public class DataBankManagementBL
  {
    private DataBankManagementDA objdataBankManagementDA = new DataBankManagementDA();

    public void ConciliatDataBank(int pintCode, string pstrPlate, int pintDataBank)
    {
      this.objdataBankManagementDA.ConciliatDataBank(pintCode, pstrPlate, pintDataBank);
    }
  }
}
