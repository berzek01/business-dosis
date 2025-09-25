// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.GenerateTicketVisa.WSEticketSoap
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

#nullable disable
namespace SIIV.WebApp.GenerateTicketVisa
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(Namespace = "https://www.multimerchantvisanet.com/solicitudtransaccion", ConfigurationName = "GenerateTicketVisa.WSEticketSoap")]
  public interface WSEticketSoap
  {
    [OperationContract(Action = "https://www.multimerchantvisanet.com/solicitudtransaccion/GeneraEticket", ReplyAction = "*")]
    GeneraEticketResponse GeneraEticket(GeneraEticketRequest request);
  }
}
