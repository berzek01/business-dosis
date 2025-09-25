// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Service_FacElec.Validator
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

#nullable disable
namespace SIIV.WebApp.Service_FacElec
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(Namespace = "http://aab", ConfigurationName = "Service_FacElec.Validator")]
  public interface Validator
  {
    [OperationContract(Action = "", ReplyAction = "*")]
    validateResponse validate(validateRequest request);
  }
}
