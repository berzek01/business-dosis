// Decompiled with JetBrains decompiler
// Type: SIIV.Requirement.BL.DataBankManagementBL
// Assembly: SIIV.Requirement.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6CDA977C-9E81-4503-829A-9DEDC12E0F36
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.Requirement.BL.dll

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
